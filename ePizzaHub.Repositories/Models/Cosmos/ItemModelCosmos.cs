using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ePizzaHub.Repositories.Models.cosmos
{
    public class ItemModelCosmos
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("ItemId")]
        public int ItemId { get;  set; }

        [JsonPropertyName("UnitPrice")]
        public decimal UnitPrice { get;  set; }

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("Total")]
        public decimal Total { get; set; }

        [JsonPropertyName("ImageUrl")]
        public string ImageUrl { get; set; }
    }
}
