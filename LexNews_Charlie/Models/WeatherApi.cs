using System.Text.Json.Serialization;

namespace LexNews_Charlie.Models
{
    public class WeatherApi
    {
        public string City { get; set; }
        public Weather[] weather { get; set; }
        public Main main { get; set; }

        public Clouds clouds { get; set; }

        public Sys sys { get; set; }

        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }

        public class Main
        {
            public float temp { get; set; }
            public float feels_like { get; set; }
            public int humidity { get; set; }
        }

        public class Clouds
        {
            public int all { get; set; }
        }

        public class Sys
        {
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }
    }






}
