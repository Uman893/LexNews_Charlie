namespace LexNews_Charlie.Models.ViewModels
{
    public class DisplayScoresVM
    {

        public int id { get; set; }
        public string sport_key { get; set; }
        public string sport_title { get; set; }
        public DateTime commence_time { get; set; }
        public bool completed { get; set; }
        public string home_team { get; set; }
        public string away_team { get; set; }

        public int scores { get; set; }

        public DateTime last_update { get; set; }
       
 
    }
}
