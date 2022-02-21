using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Models.Concrete
{
    public class Token
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int NetworkId { get; set; }
        [MinLength(2)]
        public string TokenName { get; set; }
        public double TokenPriceAvg { get; set; }
        public decimal? TokenMaxSupply { get; set; }
        public decimal TokenTotalSupply { get; set; }
        public decimal TokenCap { get; set; }
        public DateTime TokenListDate { get; set; }
        public bool VisibilityStatus { get; set; } = true;

    }
}
