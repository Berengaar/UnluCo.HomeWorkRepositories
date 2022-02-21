using EmirhanAvci.WebApi.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.DataParticles.Abstract
{
    public interface ITokenParticleGeneratorService
    {
        List<Token> GetAll();
    }
}
