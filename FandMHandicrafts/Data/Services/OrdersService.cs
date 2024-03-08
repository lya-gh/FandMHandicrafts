﻿using FandMHandicrafts.Models;
using Microsoft.EntityFrameworkCore;

namespace FandMHandicrafts.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext _context;

        public OrdersService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId,string userRole)
        {
            var orders = await _context.Orders.Include(n=>n.OrderItems).ThenInclude(n=>n.Product).ToListAsync();
            if (userRole != "Admin")
            { 
                orders = orders.Where(n=>n.UserId == userId).ToList();
            }
            return orders;     
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    ProductQuantity = item.ProductQuantity,
                    ProductId = item.Product.Id,
                    OrderId = order.Id,
                    ProductPrice = item.Product.ProductPrice
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();  
        }
    }
}