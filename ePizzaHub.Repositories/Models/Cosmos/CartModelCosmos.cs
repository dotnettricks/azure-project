using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Models.cosmos
{
    public class CartModelCosmos
    {
        public CartModelCosmos()
        {
            Items = new List<ItemModelCosmos>();
        }
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("UserId")]
        public int UserId { get; set; }

        [JsonPropertyName("Total")]
        public decimal Total { get; set; }

        [JsonPropertyName("Tax")]
        public decimal Tax { get; set; }

        [JsonPropertyName("GrandTotal")]
        public decimal GrandTotal { get; set; }
        [JsonPropertyName("id")]

        public DateTime CreatedDate { get; set; }

        [JsonPropertyName("Items")]
        public IList<ItemModelCosmos> Items { get; set; }
    }
}
