using System.Text.Json.Serialization;

namespace Transactions.API.Data.Models
{
    public class MerchantDescription
    {
        public MerchantDescription() { }

        public MerchantDescription(string description, string merchant = "unknown")
        {
            Description = description;
            Merchant = merchant;
        }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("merchant")]
        public string Merchant { get; set; }
    }
}