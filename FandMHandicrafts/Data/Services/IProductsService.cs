﻿

using FandMHandicrafts.Models;

namespace FandMHandicrafts.Data.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(int id);

        Task AddAsync(Product product);

        Task<Product> UpdateAsync(int id, Product newProduct);

        Task DeleteAsync(int id);
    }
}
