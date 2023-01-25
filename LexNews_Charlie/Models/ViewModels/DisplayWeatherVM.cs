namespace LexNews_Charlie.Models.ViewModels
{
    public class DisplayWeatherVM
    {
        public string City { get; set; }

        public string Country { get; set; }

        public string Description { get; set; }

        public string Humidity { get; set; }

        public string TempFeelsLike { get; set; }

        public string Temp { get; set; }      

        public string WeatherIcon { get; set; }


        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Main
        {
            public double temp { get; set; }
            public double feels_like { get; set; }  
            public int humidity { get; set; }
        }

        public class Sys
        { 
            public string country { get; set; }
            
        }
    }
}
