using EmirhanAvci.WebApi.Models.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Validation.Abstract
{
    public interface ITokenValidationService
    {
        bool AddControl([FromBody] Token token);
        Tuple<int, int> IdIsValid(string strId, [FromBody] Token token);
    }
}
