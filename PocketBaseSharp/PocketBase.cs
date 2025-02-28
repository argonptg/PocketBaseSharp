using PocketBaseSharp.Collections;

namespace PocketBaseSharp;

public class PocketBase(string serverUrl)
{
    private readonly HttpClient _httpClient = new();

    public Collection Collection(string collectionName)
    {
        return new Collection(_httpClient, serverUrl, collectionName);
    }
}