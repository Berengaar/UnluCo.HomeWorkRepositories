using EmirhanAvci.WebApi.DataParticles.Abstract;
using EmirhanAvci.WebApi.Models;
using EmirhanAvci.WebApi.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.DataParticles.Concrete
{
    public class CoinParticleGenerator : ICoinParticleGeneratorService
    {
        private readonly Context _db;

        public CoinParticleGenerator(Context db)
        {
            _db=db;
        }

        public void Add(Coin coin)
        {
            _db.Coins.Add(coin);
            _db.SaveChanges();
        }

        public void Delete(int id, Coin coin)
        {
            var deletedCoin=_db.Coins.SingleOrDefault(s => s.Id == id);
            deletedCoin.VisibilityStatus = false;
            _db.SaveChanges();
        }

        public void DeleteCompletely(int id,Coin coin)
        {
            _db.Coins.Remove(_db.Coins.SingleOrDefault(s => s.Id == id));
        }

        public List<Coin> GetAll()
        {
            return _db.Coins.ToList();
        }

        public IEnumerable<Coin> GetAllForUnauthorizedUsers()
        {
            return _db.Coins.Where(w=>w.VisibilityStatus==true).ToArray();
        }

        public Coin GetById(int id)
        {
            return _db.Coins.SingleOrDefault(f => f.Id == id);
        }

        public void Update(int id,Coin coin)
        {
            var updatedCoin = _db.Coins.FirstOrDefault(f => f.Id == id);
            updatedCoin.CoinName = coin.CoinName != default ? updatedCoin.CoinName : coin.CoinName;
            updatedCoin.CoinCap = coin.CoinCap != default ? updatedCoin.CoinCap : coin.CoinCap;
            updatedCoin.CoinListDate = coin.CoinListDate != default ? updatedCoin.CoinListDate : coin.CoinListDate;
            updatedCoin.CoinMaxSupply = coin.CoinMaxSupply != default ? updatedCoin.CoinMaxSupply : coin.CoinMaxSupply;
            updatedCoin.CoinTotalSupply = coin.CoinTotalSupply != default ? updatedCoin.CoinTotalSupply : coin.CoinTotalSupply;
            updatedCoin.CoinPriceAvg = coin.CoinPriceAvg != default ? updatedCoin.CoinPriceAvg : coin.CoinPriceAvg;
            updatedCoin.NetworkId = coin.NetworkId != default ? updatedCoin.NetworkId : coin.NetworkId;
            updatedCoin.CategoryId = coin.CategoryId != default ? updatedCoin.CategoryId : coin.CategoryId;
            _db.SaveChanges();
        }

        public void UpdateName(int id, Coin coin)
        {
            var updatedCoin = _db.Coins.SingleOrDefault(s => s.Id == id);
            updatedCoin.CoinName = coin.CoinName;
            _db.SaveChanges();
        }
    }
}
