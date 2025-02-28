using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PocketBaseSharp.Types
{
    public class Record
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public required List<Item> Items { get; set; } // Initialize the list
    }

    public class Item
    {
        public string CollectionId { get; set; } = string.Empty;  // Initialize as empty string
        public string CollectionName { get; set; } = string.Empty;  // Initialize as empty string
        public string Id { get; set; } = string.Empty;  // Initialize as empty string
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        
        [JsonExtensionData]
        public Dictionary<string, JToken> AdditionalData { get; set; } = new Dictionary<string, JToken>();  // Initialize as an empty dictionary
    }
}