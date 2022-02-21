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
        IEnumerable<Token> GetAllForUnauthorizedUsers();
        Token GetById(int id);

        void Add(Token token);
        void Update(int id, Token token);
        void UpdateName(int id, Token token);
        void DeleteCompletely(int id, Token token);
        void Delete(int id, Token token);
    }
}
