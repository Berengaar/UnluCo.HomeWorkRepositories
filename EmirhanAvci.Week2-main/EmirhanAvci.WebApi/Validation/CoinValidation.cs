using EmirhanAvci.WebApi.Helpers.Extensions;
using EmirhanAvci.WebApi.Models.Concrete;
using EmirhanAvci.WebApi.Validation.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Validation
{
    public class CoinValidation: ICoinValidationService
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
        public Tuple<int, int> IdIsValid(string strId, [FromBody] Coin coin)
        {
            //IsNumber() => String Extension
            if (strId.IsNumber())
            {
                var numeric = Convert.ToInt32(strId);
                return Tuple.Create(numeric, 0);
            }
            else
            {
                return Tuple.Create(0, -1);
            }
        }

    }
}
