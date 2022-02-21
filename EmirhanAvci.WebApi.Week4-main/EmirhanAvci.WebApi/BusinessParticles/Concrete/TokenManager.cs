using EmirhanAvci.WebApi.BusinessParticles.Abstract;
using EmirhanAvci.WebApi.DataParticles.Abstract;
using EmirhanAvci.WebApi.Models.Concrete;
using EmirhanAvci.WebApi.Validation.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.BusinessParticles.Concrete
{
    public class TokenManager : ITokenService
    {
        ITokenValidationService _tokenValidationService;
        ITokenParticleGeneratorService _tokenParticleGeneratorService;
        public TokenManager(ITokenParticleGeneratorService tokenParticleGeneratorService)
        {
            _tokenParticleGeneratorService = tokenParticleGeneratorService;
        }

        public bool Add(Token token)
        {
            if (_tokenValidationService.AddControl(token))
            {
                _tokenParticleGeneratorService.Add(token);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(string strId, Token token)
        {
            var controlObject = _tokenValidationService.IdIsValid(strId, token);
            if (controlObject.Item1 != 0 && controlObject.Item2 != -1)
            {
                int id = Convert.ToInt32(strId);
                _tokenParticleGeneratorService.Delete(id, token);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteCompletely(string strId, Token token)
        {
            var controlObject = _tokenValidationService.IdIsValid(strId, token);
            if (controlObject.Item1 != 0 && controlObject.Item2 != -1)
            {
                int id = Convert.ToInt32(strId);
                _tokenParticleGeneratorService.DeleteCompletely(id, token);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Token> GetAll()
        {
            return _tokenParticleGeneratorService.GetAll();
        }

        public IEnumerable<Token> GetAllForUnauthorizedUsers()
        {
            return _tokenParticleGeneratorService.GetAllForUnauthorizedUsers();
        }

        public Token GetById(int id)
        {
            return _tokenParticleGeneratorService.GetById(id);
        }

        public bool Update(string strId, Token token)
        {
            var controlObject = _tokenValidationService.IdIsValid(strId, token);
            if (controlObject.Item1 != 0 && controlObject.Item2 != -1)
            {
                int id = Convert.ToInt32(strId);
                _tokenParticleGeneratorService.Update(id, token);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateName(string strId, Token token)
        {
            var controlObject = _tokenValidationService.IdIsValid(strId, token);
            if (controlObject.Item1 != 0 && controlObject.Item2 != -1)
            {
                int id = Convert.ToInt32(strId);
                _tokenParticleGeneratorService.UpdateName(id, token);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
