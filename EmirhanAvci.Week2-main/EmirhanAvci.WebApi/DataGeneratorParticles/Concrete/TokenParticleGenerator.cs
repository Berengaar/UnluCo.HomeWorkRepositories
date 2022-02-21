using EmirhanAvci.WebApi.DataParticles.Abstract;
using EmirhanAvci.WebApi.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.DataParticles.Concrete
{
    public class TokenParticleGenerator : ITokenParticleGeneratorService
    {

        List<Token> _tokensList;
        public TokenParticleGenerator()
        {
            _tokensList = new List<Token>()
            {
                new Token{Id=1,CategoryId=1,NetworkId=1,TokenName="Revv",TokenPriceAvg=0.1242,TokenMaxSupply=3000000000,TokenTotalSupply=3000000000,TokenCap=34568749,TokenListDate=new DateTime(2022,1,1)},
                new Token{Id=2,CategoryId=5,NetworkId=2,TokenName="S.S. Lazio Fan Token",TokenPriceAvg=4.35,TokenMaxSupply=40000000,TokenTotalSupply=40000000,TokenCap=37447455,TokenListDate=new DateTime(2022,1,1)}
            };
        }
        public List<Token> GetAll()
        {
            return _tokensList;
        }
    }
}
