using EmirhanAvci.WebApi.Models.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Validation.Abstract
{
    public interface ICoinValidationService
    {
        bool AddControl([FromBody] Coin coin);
        Tuple<int, int> IdIsValid(string strId, [FromBody] Coin coin);
    }
}
