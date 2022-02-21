using EmirhanAvci.WebApi.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.BusinessParticles.Abstract
{
    #region NamingDescription
    /*
        Main folder name is BusinessParticles.Normally we can name it  
     */
    #endregion

    public interface ICoinService
    {
        List<Coin> GetAll();
        //With Linq
        IEnumerable<Coin> GetAllForUnauthorizedUsers();
        Coin GetById(int id);
        bool Add(Coin coin);
        bool Update(string strId, Coin coin);
        bool UpdateName(string strId, Coin coin);
        bool DeleteCompletely(string strId,Coin coin);
        bool Delete(string strId, Coin coin);
    }
}
