using EmirhanAvci.WebApi.Week1.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Week1.FixedDataOperations.DataListOperations
{
    public static class CoinDataListGenerator
    {
        public static List<Coin> coinsList = new List<Coin>()
        {
            new Coin{Id=1,CategoryId=1,NetworkId=1,CoinName="ChainLink",CoinPriceAvg=27.39,CoinMaxSupply=1000000000,CoinTotalSupply=1000000000,CoinCap=11633538747,CoinListDate=new DateTime(2017,9,24),VisibilityStatus=true},
            new Coin{Id=2,CategoryId=4,NetworkId=2,CoinName="Audius",CoinPriceAvg=1.43,CoinMaxSupply=null,CoinTotalSupply=1072692308,CoinCap=736331316,CoinListDate=new DateTime(2021,1,15),VisibilityStatus=true},
            new Coin{Id=3,CategoryId=2,NetworkId=3,CoinName="Cardano",CoinPriceAvg=1.27,CoinMaxSupply=45000000000,CoinTotalSupply=33968614581,CoinCap=42493007975,CoinListDate=new DateTime(2017,10,3),VisibilityStatus=true},
            new Coin{Id=4,CategoryId=3,NetworkId=4,CoinName="Ankr",CoinPriceAvg=27.39,CoinMaxSupply=10000000000,CoinTotalSupply=10000000000,CoinCap=756435878,CoinListDate=new DateTime(2019,3,8),VisibilityStatus=true},
        };

        public static List<Token> tokensList = new List<Token>()
        {
            new Token{Id=1,CategoryId=1,NetworkId=1,TokenName="Revv",TokenPriceAvg=0.1242,TokenMaxSupply=3000000000,TokenTotalSupply=3000000000,TokenCap=34568749,TokenListDate=new DateTime(2022,1,1)},
            new Token{Id=2,CategoryId=5,NetworkId=2,TokenName="S.S. Lazio Fan Token",TokenPriceAvg=4.35,TokenMaxSupply=40000000,TokenTotalSupply=40000000,TokenCap=37447455,TokenListDate=new DateTime(2022,1,1)}
        };
    }
}
