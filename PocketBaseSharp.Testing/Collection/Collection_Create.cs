using PocketBaseSharp;

namespace PocketBaseSharp.Testing;

public class Collection_Create
{
    private readonly PocketBase  _pb = new PocketBase("http://127.0.0.1:8090");

    [Fact]
    public async Task Create_WithValidData_ShouldReturnRecord()
    {
        const string payload = $"{{\"name\": \"Jane Doe\"}}";
        var data = await _pb.Collection("test_collection").Create(payload);
        
        Assert.NotNull(data);
    }

    [Fact]
    public async Task Create_WithInvalidData_ShouldThrow()
    {
        const string payload = $"{{\"invalid_param\": \"Jane Doe\"}}";
        var data = _pb.Collection("test_collection");
        
        await Assert.ThrowsAsync<HttpRequestException>(() => data.Create(payload));
    }
}