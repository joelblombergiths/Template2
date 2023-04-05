using Azure;
using Template2.Server.Interfaces;
using Template2.Server.Models;
using Template2.Shared.DTOs;

namespace Template2.Server.Data;

public class ProductRepository : IProductRepository
{
    private readonly AzureTableContext _context;

    public ProductRepository(AzureTableContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<ProductDto>>> GetAllAsync(string category, string? genre, string? age)
    {
        try
        {
            AsyncPageable<ProductModel>? res;

            if (genre is null && age is null)
            {
                res = _context.Products
                    .QueryAsync<ProductModel>(e =>
                        e.PartitionKey.Equals(category, StringComparison.InvariantCultureIgnoreCase));
            }
            else if (genre is not null && age is not null)
            {
                res = _context.Products
                    .QueryAsync<ProductModel>(e =>
                        e.PartitionKey.Equals(category, StringComparison.InvariantCultureIgnoreCase) &&
                        e.Genre.Equals(genre, StringComparison.InvariantCultureIgnoreCase) &&
                        e.Age.Equals(age, StringComparison.InvariantCultureIgnoreCase));
            }
            else
            {
                if (genre is not null)
                {
                    res = _context.Products
                        .QueryAsync<ProductModel>(e =>
                            e.PartitionKey.Equals(category, StringComparison.InvariantCultureIgnoreCase) &&
                            e.Genre.Equals(genre, StringComparison.InvariantCultureIgnoreCase));
                }
                else
                {
                    res = _context.Products
                        .QueryAsync<ProductModel>(e =>
                            e.PartitionKey.Equals(category, StringComparison.InvariantCultureIgnoreCase) &&
                            e.Age.Equals(age, StringComparison.InvariantCultureIgnoreCase));
                }
            }

            List<ProductDto> products = new();
            await foreach (ProductModel product in res)
            {
                products.Add(new()
                {
                    Id = product.RowKey,
                    Name = product.Name,
                    Genre = product.Genre,
                    Price = product.Price
                });
            }

            return new()
            {
                IsSuccessful = products.Any(),
                Data = products,
                Message = string.Empty
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                IsSuccessful = false,
                Message = ex.Message
            };
        }
    }
}
