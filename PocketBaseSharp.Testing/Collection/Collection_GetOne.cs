using PocketBaseSharp;

namespace PocketBaseSharp.Testing;

public class Collection_GetOne
{
    private readonly PocketBase  _pb = new PocketBase("http://127.0.0.1:8090");

    [Fact]
    public async Task GetOne_WithValidParams_ShouldReturnRecord()
    {
        const string collectionName = "test_collection";
        const string recordId = "testingrecordid";
        var data = await _pb.Collection(collectionName).GetOne(recordId);
        
        Assert.NotNull(data);
    }

    [Fact]
    public async Task GetOne_WithInvalidParams_ShouldThrow()
    {
        const string collectionName = "test_collection";
        const string recordId = "invalidrecordid";
        var data = _pb.Collection(collectionName);
        
        await Assert.ThrowsAsync<HttpRequestException>(() => data.GetOne(recordId));
    }
}