using System.Text.Json.Serialization;

namespace LexNews_Charlie.Models.SpotModels
{

   

    public class TodaysSpotData
    {
        [JsonPropertyName("spotData")]
        public List <SpotPriceHour> TodaysSpotPrices { get; set; }
    }


    public class SpotPriceHour
    {
        [JsonPropertyName("spotData")]
        public List<AreaSpotData> SpotData { get; set; }
    }
    public class AreaSpotData
    {
        [JsonPropertyName("dateAndTime")]
        public DateTime DateAndTime { get; set; }
         
        [JsonPropertyName("areaName") ]
        public string AreaName { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; }
    }
}
