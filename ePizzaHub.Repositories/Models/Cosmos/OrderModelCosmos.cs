using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using ePizzaHub.Repositories.Models.cosmos;

namespace ePizzaHub.Repositories.Models.Cosmos
{
    public class OrderModelCosmos
    {
        public OrderModelCosmos()
        {
            Items = new List<ItemModelCosmos>();
        }
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("PaymentId")]
        public string PaymentId { get; set; }

        [JsonPropertyName("UserId")]
        public int UserId { get; set; }

        [JsonPropertyName("Currency")]
        public string Currency { get; set; }

        [JsonPropertyName("Total")]
        public decimal Total { get; set; }

        [JsonPropertyName("Tax")]
        public decimal Tax { get; set; }

        [JsonPropertyName("GrandTotal")]
        public decimal GrandTotal { get; set; }

        [JsonPropertyName("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [JsonPropertyName("Items")]
        public ICollection<ItemModelCosmos> Items { get; set; }

        [JsonPropertyName("Locality")]
        public string Locality { get; internal set; }
    }
}
