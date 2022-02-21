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
        //With Linq
        IEnumerable<Token> GetAllForUnauthorizedUsers();
        Token GetById(int id);
        bool Add(Token token);
        bool Update(string strId, Token token);
        bool UpdateName(string strId, Token token);
        bool DeleteCompletely(string strId, Token token);
        bool Delete(string strId, Token token);
    }
}
