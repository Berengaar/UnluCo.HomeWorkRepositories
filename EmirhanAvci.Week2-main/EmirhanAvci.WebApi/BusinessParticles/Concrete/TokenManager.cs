using EmirhanAvci.WebApi.BusinessParticles.Abstract;
using EmirhanAvci.WebApi.DataParticles.Abstract;
using EmirhanAvci.WebApi.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.BusinessParticles.Concrete
{
    public class TokenManager : ITokenService
    {

        ITokenParticleGeneratorService _tokenParticleGeneratorService;
        public TokenManager(ITokenParticleGeneratorService tokenParticleGeneratorService)
        {
            _tokenParticleGeneratorService = tokenParticleGeneratorService;
        }
        public List<Token> GetAll()
        {
            return _tokenParticleGeneratorService.GetAll();
        }
    }
}
