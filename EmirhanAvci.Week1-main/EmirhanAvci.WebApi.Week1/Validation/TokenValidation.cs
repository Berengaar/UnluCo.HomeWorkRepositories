using EmirhanAvci.WebApi.Week1.Models.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Week1.Validation
{
    public class TokenValidation
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
        public Tuple<int, int> UpdateControl(string id, [FromBody] Token token)
        {
            //IsNumber()?
            if (Regex.IsMatch(id, @"^\d+$"))
            {
                var numeric = Convert.ToInt32(id);
                if (numeric > 0)
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
