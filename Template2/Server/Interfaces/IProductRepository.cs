using Template2.Shared.DTOs;

namespace Template2.Server.Interfaces;

public interface IProductRepository
{
    Task<ServiceResponse<List<ProductDto>>> GetAllAsync(string category, string? genre, string? age);
}