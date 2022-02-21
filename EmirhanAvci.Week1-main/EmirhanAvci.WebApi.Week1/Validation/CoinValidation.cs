using EmirhanAvci.WebApi.Week1.Models.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Week1.Validation
{
    public class CoinValidation
    {
        public bool AddControl([FromBody] Coin coin)
        {
            if (coin != null && !string.IsNullOrEmpty(coin.CoinName) && coin.CoinName.Length >= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region TupleClass
        /*
         Yeni bir class oluşturmadan birden fazla tipteki elemanı aynı veri yapısı içerisinde tutabiliyoruz.
         Anonymous type return ettiğimizde property'leri dönemeyiz
         Bu yüzden bu çözümü kullandım.
         */
        #endregion
        public Tuple<int, int> UpdateControl(string id, [FromBody]Coin coin)
        {
            //IsNumber()?
            if (Regex.IsMatch(id, @"^\d+$"))
            {
                var numeric = Convert.ToInt32(id);
                if (numeric>0)
                {
                    return Tuple.Create(numeric, 0);
                }
                else
                {
                    return Tuple.Create(numeric, -1);
                }
            }
            else
            {
                return Tuple.Create(0, -1);
            }
        }

    }
}
