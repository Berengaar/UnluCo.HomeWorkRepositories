using EmirhanAvci.WebApi.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.DataParticles.Abstract
{
    public interface ICoinParticleGeneratorService
    {
        List<Coin> GetAll();
        IEnumerable<Coin> GetAllForUnauthorizedUsers();
        Coin GetById(int id);
        
        void Add(Coin coin);
        void Update(int id, Coin coin);
        void UpdateName(int id, Coin coin);
        void DeleteCompletely(int id,Coin coin);
        void Delete(int id,Coin coin);
    }
}
