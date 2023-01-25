using LexNews_Charlie.Models.Entities;

namespace LexNews_Charlie.Models.ViewModels
{
    public class HistoricalDataVM
    {
        public DateTime Date { get; set; }
        public List<SpotPriceEntity> DateAreaPrices { get; set; }

        //public HistoricalDataVM()
        //{
        //    DateAreaPrices = new List<SpotPriceEntity>();
        //}

    }

   
 
}
