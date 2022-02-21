using EmirhanAvci.WebApi.DataParticles.Abstract;
using EmirhanAvci.WebApi.Models;
using EmirhanAvci.WebApi.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.DataParticles.Concrete
{
    public class TokenParticleGenerator : ITokenParticleGeneratorService
    {

        private readonly Context _db;
        public TokenParticleGenerator(Context db)
        {
            _db = db;
        }

        public void Add(Token token)
        {
            _db.Tokens.Add(token);
            _db.SaveChanges();
        }

        public void Delete(int id, Token token)
        {
            var deletedToken= _db.Tokens.SingleOrDefault(s => s.Id == id);
            deletedToken.VisibilityStatus = false;
            _db.SaveChanges();
        }

        public void DeleteCompletely(int id, Token token)
        {
            _db.Tokens.Remove(_db.Tokens.SingleOrDefault(s => s.Id == id));
        }

        public List<Token> GetAll()
        {
            return _db.Tokens.ToList();
        }

        public IEnumerable<Token> GetAllForUnauthorizedUsers()
        {
            return _db.Tokens.Where(w => w.VisibilityStatus == true).ToArray();
        }

        public Token GetById(int id)
        {
            return _db.Tokens.SingleOrDefault(s => s.Id == id);
        }

        public void Update(int id, Token token)
        {
            var updatedToken = _db.Tokens.SingleOrDefault(f => f.Id == id);
            updatedToken.TokenName = token.TokenName != default ? updatedToken.TokenName : token.TokenName;
            updatedToken.TokenCap = token.TokenCap != default ? updatedToken.TokenCap : token.TokenCap;
            updatedToken.TokenListDate = token.TokenListDate != default ? updatedToken.TokenListDate : token.TokenListDate;
            updatedToken.TokenMaxSupply = token.TokenMaxSupply != default ? updatedToken.TokenMaxSupply : token.TokenMaxSupply;
            updatedToken.TokenTotalSupply = token.TokenTotalSupply != default ? updatedToken.TokenTotalSupply : token.TokenTotalSupply;
            updatedToken.TokenPriceAvg = token.TokenPriceAvg != default ? updatedToken.TokenPriceAvg : token.TokenPriceAvg;
            updatedToken.NetworkId = token.NetworkId != default ? updatedToken.NetworkId : token.NetworkId;
            updatedToken.CategoryId = token.CategoryId != default ? updatedToken.CategoryId : token.CategoryId;
            _db.SaveChanges();
        }

        public void UpdateName(int id, Token token)
        {
            var updatedToken = _db.Tokens.SingleOrDefault(f => f.Id == id);
            updatedToken.TokenName = token.TokenName;
            _db.SaveChanges();
        }
    }
}
