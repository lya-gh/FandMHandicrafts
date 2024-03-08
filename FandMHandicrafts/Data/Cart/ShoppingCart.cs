using FandMHandicrafts.Models;
using Microsoft.EntityFrameworkCore;

namespace FandMHandicrafts.Data.Cart
{
    public class ShoppingCart
    {
        public ApplicationDbContext _context { get; set; }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ApplicationDbContext>();

            string cartId = session.GetString("CartId")?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddItemToCart(Product product)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Product.Id == product.Id
                && n.ShoppingCartId == ShoppingCartId);


            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    ProductQuantity = 1
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }else
            {
                shoppingCartItem.ProductQuantity++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Product product)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Product.Id == product.Id
                && n.ShoppingCartId == ShoppingCartId);


            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.ProductQuantity > 1)
                {
                    shoppingCartItem.ProductQuantity--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }


        public List<ShoppingCartItem> GetShoppingCartItems() 
        {
            return ShoppingCartItems ?? (ShoppingCartItems= _context.ShoppingCartItems.Where
                (n=>n.ShoppingCartId==ShoppingCartId).Include(n=>n.Product).ToList());
        }

        public double GetShoppingCartTotal() 
        {
            var total = _context.ShoppingCartItems.Where
                (n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Product.ProductPrice * n.ProductQuantity).Sum();
            return total;
        }

        public async Task ClearShoppingCartAsync()
        { 
            var items = await _context.ShoppingCartItems.Where
                (n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Product).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
