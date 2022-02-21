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
    public class TokenValidation : ITokenValidationService
    {
        public bool AddControl([FromBody] Token token)
        {
            if (token != null && !string.IsNullOrEmpty(token.TokenName) && token.TokenName.Length >= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Tuple<int, int> IdIsValid(string strId, [FromBody] Token token)
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
