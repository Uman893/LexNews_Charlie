using NuGet.Protocol.Core.Types;

namespace LexNews_Charlie.Models
{
    public class Currency
    {
        public string from { get; set; }

        public string to { get; set; }

        public decimal q { get; set; }

        public decimal? exchangerate { get; set; }


    }



}
