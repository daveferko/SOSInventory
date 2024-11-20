using System.Text.Json.Serialization;

namespace SOSInventory.Dto
{
    public class ItemQuantityResponse
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("quantityOnHand")]
        public int QuantityOnHand { get; set; }
    }
}
