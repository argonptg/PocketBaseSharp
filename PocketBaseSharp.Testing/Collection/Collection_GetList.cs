using PocketBaseSharp;

namespace PocketBaseSharp.Testing;

public class Collection_GetList
{
    private readonly PocketBase _pb = new PocketBase("http://127.0.0.1:8090");
    
    [Fact]
    public async Task GetList_WithValidParams_ShouldReturnRecord()
    {
        const string collectionName = "test_collection";
        var data = await _pb.Collection(collectionName).GetList(1, 50);
        
        Assert.NotNull(data);
    }

    [Fact]
    public async Task GetList_WithInvalidParams_ShouldThrow()
    {
        const string collectionName = "invalid_name";
        var collection = _pb.Collection(collectionName);

        await Assert.ThrowsAsync<HttpRequestException>(() => collection.GetList(1, 50));
    }
    
    
}
