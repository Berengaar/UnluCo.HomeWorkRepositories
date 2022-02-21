using EmirhanAvci.WebApi.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.BusinessParticles.Abstract
{
    public interface ITokenService
    {
        List<Token> GetAll();
    }
}
