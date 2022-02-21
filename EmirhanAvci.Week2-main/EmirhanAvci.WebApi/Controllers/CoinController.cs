using EmirhanAvci.WebApi.BusinessParticles.Abstract;
using EmirhanAvci.WebApi.FixedDataOperations.DataListOperations;
using EmirhanAvci.WebApi.Models.Concrete;
using EmirhanAvci.WebApi.Models.Messages.SuccessMessages;
using EmirhanAvci.WebApi.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Controllers
{
    [Route("api/coin")]
    [ApiController]
    public class CoinController : ControllerBase
    {
        ICoinService _coinService;

        public CoinController(ICoinService coinService)
        {
            _coinService = coinService;
        }

        [HttpGet("GetAllForAdmins")]
        public IActionResult GetAll()
        {
            return Ok(_coinService.GetAll());
        }

        #region VisibilityStatus
        /*
            Veri tabanlarında silme işlemleri gerçekleşmez ancak kullanıcıların görüntülemesini istemediğimiz 
            dataları gizleyebiliriz. Bu yüzden VisibilityStatus adında bir property tanımlar ve default true
            veririz.
         */
        #endregion
        [HttpGet("GetAllForUsers")]
        public IActionResult GetAllForUnauthorizedUsers()
        {
            return Ok(_coinService.GetAllForUnauthorizedUsers());
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            if (id > 0)
            {
                var selectedCoin = _coinService.GetById(id);
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
            if (_coinService.Add(coin))
            {
                return Created("Index", new { message = CoinSuccessMessage.CoinAddedMessage });
            }
            //Return Anonymous Object
            else
            {
                return BadRequest();
            }
        }


        [HttpPut("Update/{strId}")]
        public IActionResult Update(string strId, [FromBody] Coin coin)
        {
            if (_coinService.Update(strId,coin))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch("UpdateName/{strId}")]
        public IActionResult UpdateName(string strId, [FromBody] Coin coin)
        {
            if (_coinService.UpdateName(strId,coin))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteCompletely/{strId}")]
        public IActionResult DeleteCompletely(string strId, [FromBody] Coin coin)
        {
            if (_coinService.DeleteCompletely(strId,coin))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        
        [HttpPatch("DeleteForUser/{strId}")]
        public IActionResult Delete(string strId, [FromBody] Coin coin)
        {
            if (_coinService.Delete(strId, coin))
            {
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
        public IActionResult GetSortByNetwork()
        {
            return Ok(CoinDataListGenerator.coinsList.OrderBy(o => o.NetworkId).ToList());
        }
    }
}
