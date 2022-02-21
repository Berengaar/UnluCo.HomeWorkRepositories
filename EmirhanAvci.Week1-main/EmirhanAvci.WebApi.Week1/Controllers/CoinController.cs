using EmirhanAvci.WebApi.Week1.FixedDataOperations.DataListOperations;
using EmirhanAvci.WebApi.Week1.Models.Concrete;
using EmirhanAvci.WebApi.Week1.Models.Messages.SuccessMessages;
using EmirhanAvci.WebApi.Week1.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Week1.Controllers
{
    [Route("api/coin")]
    [ApiController]
    public class CoinController : ControllerBase
    {
        [HttpGet("GetAllForAdmins")]
        public IActionResult Get()
        {
            return Ok(CoinDataListGenerator.coinsList);
        }

        #region VisibilityStatus
        /*
            Veri tabanlarında silme işlemleri gerçekleşmez ancak kullanıcıların görüntülemesini istemediğimiz 
            dataları gizleyebiliriz. Bu yüzden VisibilityStatus adında bir property tanımlar ve default true
            veririz.
         */
        #endregion
        [HttpGet("GetAllForUsers")]
        public IActionResult GetForUnauthorizedUsers()
        {
            return Ok(CoinDataListGenerator.coinsList.Where(w => w.VisibilityStatus == true));
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            if (id > 0)
            {
                var selectedCoin = CoinDataListGenerator.coinsList.FirstOrDefault(f => f.Id == id);
                if (selectedCoin != null)
                {
                    return Ok(selectedCoin);
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Coin coin)
        {
            CoinValidation coinValidation = new CoinValidation();
            if (coinValidation.AddControl(coin))
            {
                try
                {
                    CoinDataListGenerator.coinsList.Add(coin);
                    return Ok();            //Convert to Created
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
            else
            {
                //Return Anonymous Object
                return Created("Index", new { message = CoinSuccessMessage.CoinAddedMessage });
            }
        }


        [HttpPut("Update/{id}")]
        public IActionResult Update(string id, [FromBody] Coin coin)
        {
            CoinValidation coinValidation = new CoinValidation();
            var numericControlObject =coinValidation.UpdateControl(id, coin);
            //Item1 = Id Number
            //Item2 = ControlValue
            if (numericControlObject.Item1!=0 && numericControlObject.Item2!=-1)
            {
                var updatedCoin = CoinDataListGenerator.coinsList.FirstOrDefault(f => f.Id == numericControlObject.Item1);
                updatedCoin.CoinName = coin.CoinName != default ? updatedCoin.CoinName : coin.CoinName;
                updatedCoin.CoinCap = coin.CoinCap != default ? updatedCoin.CoinCap : coin.CoinCap;
                updatedCoin.CoinListDate = coin.CoinListDate != default ? updatedCoin.CoinListDate : coin.CoinListDate;
                updatedCoin.CoinMaxSupply = coin.CoinMaxSupply != default ? updatedCoin.CoinMaxSupply : coin.CoinMaxSupply;
                updatedCoin.CoinTotalSupply = coin.CoinTotalSupply != default ? updatedCoin.CoinTotalSupply : coin.CoinTotalSupply;
                updatedCoin.CoinPriceAvg = coin.CoinPriceAvg != default ? updatedCoin.CoinPriceAvg : coin.CoinPriceAvg;
                updatedCoin.NetworkId = coin.NetworkId != default ? updatedCoin.NetworkId : coin.NetworkId;
                updatedCoin.CategoryId = coin.CategoryId != default ? updatedCoin.CategoryId : coin.CategoryId;
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch("UpdateName/{id}")]
        public IActionResult UpdateName(string id, [FromBody] Coin coin)
        {
            CoinValidation coinValidation = new CoinValidation();
            var numericControlObject = coinValidation.UpdateControl(id, coin);
            if (numericControlObject.Item1 != 0 && numericControlObject.Item2 != -1)
            {
                var updatedCoin = CoinDataListGenerator.coinsList.FirstOrDefault(f => f.Id == numericControlObject.Item1);
                updatedCoin.CoinName = coin.CoinName != default ? coin.CoinName : updatedCoin.CoinName;
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteCompletely/{id}")]
        public IActionResult DeleteCompletely(string id,[FromBody] Coin coin)
        {
            CoinValidation coinValidation = new CoinValidation();
            var numericControlObject = coinValidation.UpdateControl(id, coin);
            if (numericControlObject.Item1 != 0 && numericControlObject.Item2 != -1)
            {
                var deletedCoin = CoinDataListGenerator.coinsList.FirstOrDefault(f => f.Id == numericControlObject.Item1);
                CoinDataListGenerator.coinsList.Remove(deletedCoin);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        } 

        [HttpPatch]
        public IActionResult Delete(string id, [FromBody] Coin coin)
        {
            CoinValidation coinValidation = new CoinValidation();
            var numericControlObject = coinValidation.UpdateControl(id, coin);
            if (numericControlObject.Item1 != 0 && numericControlObject.Item2 != -1)
            {
                var deletedCoin = CoinDataListGenerator.coinsList.FirstOrDefault(f => f.Id == numericControlObject.Item1);
                deletedCoin.VisibilityStatus = false;
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("GetAllByCategory")]
        public IActionResult GetSortByCategory()
        {
            return Ok(CoinDataListGenerator.coinsList.OrderBy(o => o.CategoryId).ToList());
        }

        [HttpGet("GetAllByNetwork")]
        public IActionResult GetSoryByNetwork()
        {
            return Ok(CoinDataListGenerator.coinsList.OrderBy(o => o.NetworkId).ToList());
        }
    }
}
