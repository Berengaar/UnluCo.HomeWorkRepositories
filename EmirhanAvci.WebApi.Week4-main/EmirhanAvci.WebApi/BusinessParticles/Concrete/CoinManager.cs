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
    public class CoinManager : ICoinService
    {
        ICoinParticleGeneratorService _coinParticleGeneratorService;
        ICoinValidationService _coinValidationService;

        public CoinManager(ICoinParticleGeneratorService coinParticleGeneratorService, ICoinValidationService coinValidationService)
        {
            _coinParticleGeneratorService = coinParticleGeneratorService;
            _coinValidationService = coinValidationService;
        }

        public bool Add(Coin coin)
        {
            if (_coinValidationService.AddControl(coin))
            {
                _coinParticleGeneratorService.Add(coin);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(string strId, Coin coin)
        {
            var controlObject = _coinValidationService.IdIsValid(strId, coin);
            if (controlObject.Item1 != 0 && controlObject.Item2 != -1)
            {
                int id = Convert.ToInt32(strId);
                _coinParticleGeneratorService.Delete(id, coin);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteCompletely(string strId,Coin coin)
        {
            var controlObject = _coinValidationService.IdIsValid(strId, coin);
            if (controlObject.Item1!=0 && controlObject.Item2!=-1)
            {
                int id = Convert.ToInt32(strId);
                _coinParticleGeneratorService.DeleteCompletely(id, coin);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Coin> GetAll()
        {
            return _coinParticleGeneratorService.GetAll();
        }

        public IEnumerable<Coin> GetAllForUnauthorizedUsers()
        {
            return _coinParticleGeneratorService.GetAllForUnauthorizedUsers();
        }

        public Coin GetById(int id)
        {
            return _coinParticleGeneratorService.GetById(id);
        }
        public bool Update(string strId, Coin coin)
        {
            var controlObject = _coinValidationService.IdIsValid(strId, coin);
            if (controlObject.Item1!=0 && controlObject.Item2!=-1)
            {
                int id = Convert.ToInt32(strId);
                _coinParticleGeneratorService.Update(id, coin);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateName(string strId, Coin coin)
        {
            var controlObject = _coinValidationService.IdIsValid(strId, coin);
            if (controlObject.Item1 != 0 && controlObject.Item2 != -1)
            {
                int id = Convert.ToInt32(strId);
                _coinParticleGeneratorService.UpdateName(id, coin);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
