using System.Text;
using System.Web; // For HttpUtility
using Newtonsoft.Json;
using PocketBaseSharp.Types;

namespace PocketBaseSharp.Collections;

public class Collection(HttpClient httpClient, string url, string collectionName)
{
    private readonly string _baseUri = $"{url}/api/collections/{collectionName}/";

    public async Task<Record?> GetList(
        int page, 
        int perPage, 
        (string? filter, string? sort, string? expand)? opt = null // Nullable tuple
    )
    {
        try
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["page"] = page.ToString();
            query["per_page"] = perPage.ToString();

            if (opt is not null) // Check if tuple is set
            {
                if (!string.IsNullOrEmpty(opt.Value.sort)) query["sort"] = opt.Value.sort;
                if (!string.IsNullOrEmpty(opt.Value.expand)) query["expand"] = opt.Value.expand;
                if (!string.IsNullOrEmpty(opt.Value.filter)) query["filter"] = opt.Value.filter;
            }

            var uri = $"{_baseUri}/records?{query}";
            
            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var jsonContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Record>(jsonContent);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Item?> GetOne(string recordId, string? expand = null)
    {
        try
        {
            var uri = $"{_baseUri}/records/{recordId}";
            
            // if expand isn't null then add it to the uri
            if (!string.IsNullOrEmpty(expand)) uri += $"?{expand}";
            
            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var jsonContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Item>(jsonContent);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Item?> Create(string body, string? expand = null)
    {
        try
        {
            var uri = $"{_baseUri}records";

            // yada yada yada you get the deal
            if (!string.IsNullOrEmpty(expand)) uri += $"?{expand}";
            
            // set http headers
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            
            var response = await httpClient
                .PostAsync(uri, new StringContent(body, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var jsonContent = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<Item>(jsonContent);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<Item?> Update(string recordId, string body, string? expand = null)
    {
        try
        {
            var uri = $"{_baseUri}records/{recordId}";

            // yada yada yada you get the deal
            if (!string.IsNullOrEmpty(expand)) uri += $"?{expand}";
            
            // set http headers
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            
            var response = await httpClient
                .PatchAsync(uri, new StringContent(body, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var jsonContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonContent);
            
            return JsonConvert.DeserializeObject<Item>(jsonContent);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    
}