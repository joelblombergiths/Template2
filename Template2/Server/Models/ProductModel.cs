using Azure;
using Azure.Data.Tables;

namespace Template2.Server.Models;

public class ProductModel : ITableEntity
{
    public string Name { get; set; }
    public string Genre { get; set; }
    public double Price { get; set; }
    public string Age { get; set; }
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}