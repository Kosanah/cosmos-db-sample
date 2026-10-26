using Microsoft.Azure.Cosmos;

// Cosmos DB sample: container query with a partition key, parameterized.
public class OrdersRepository
{
    private readonly Container _container;
    public OrdersRepository(CosmosClient client) => _container = client.GetContainer("app", "orders");
    public async Task<List<OrderDoc>> PaidAsync(string customerId)
    {
        var q = _container.GetItemQueryIterator<OrderDoc>(new QueryDefinition("SELECT * FROM c WHERE c.paid = true"), requestOptions: new QueryRequestOptions { PartitionKey = new PartitionKey(customerId)});
        var out_ = new List<OrderDoc>();
        while (q.HasMoreResults) out_.AddRange(await q.ReadNextAsync());
        return out_;
}
}
public class OrderDoc { public string id { get; set;} = ""; public bool paid { get; set;}}
