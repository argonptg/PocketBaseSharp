using PocketBaseSharp;

namespace PocketBaseSharp.Testing;

public class Collection_Update
{
    private readonly PocketBase  _pb = new PocketBase("http://127.0.0.1:8090");

    [Fact]
    public async Task Update_WithValidPayload_ShouldReturnRecord()
    {
        const string payload = $"{{\"name\": \"Foo Bar\"}}";
        const string recordId = "testingrecordid";
        var data = await _pb.Collection("test_collection").Update(recordId, payload);
        
        Assert.NotNull(data);
    }

    [Fact]
    public async Task Update_WithInvalidPayload_ShouldReturnRecord()
    {
        const string payload = $"{{\"invalid_param\": \"Foo Bar\"}}";
        const string recordId = "testingrecordid";
        var data = _pb.Collection("test_collection").Update(recordId, payload);
        
        Assert.NotNull(data);
    }
    
    [Fact]
    public async Task Update_WithInvalidRecordId_ShouldThrow()
    {
        const string payload = $"{{\"name\": \"Foo Bar\"}}";
        const string recordId = "invalidrecordid";
        var data = _pb.Collection("test_collection");
        
        await Assert.ThrowsAsync<HttpRequestException>(() => data.Update(recordId, payload));
    }
}