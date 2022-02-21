using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Models.Concrete
{
    public class Coin
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int NetworkId { get; set; }
        [MinLength(2)]
        public string CoinName { get; set; }
        public double CoinPriceAvg { get; set; }
        public decimal? CoinMaxSupply { get; set; }
        public decimal CoinTotalSupply { get; set; }
        public decimal CoinCap { get; set; }
        public DateTime CoinListDate { get; set; }
        public bool VisibilityStatus { get; set; }

    }
}
