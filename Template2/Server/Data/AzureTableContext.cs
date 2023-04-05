using Azure.Data.Tables;

namespace Template2.Server.Data;

public class AzureTableContext
{
    public TableClient Products { get; }

    public AzureTableContext(string conectionString)
    {
        Products = new(new(conectionString),"tableone");
    }
}