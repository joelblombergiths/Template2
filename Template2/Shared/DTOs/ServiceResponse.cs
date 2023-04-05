namespace Template2.Shared.DTOs;

public class ServiceResponse<T>
{
    public bool IsSuccessful { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
}