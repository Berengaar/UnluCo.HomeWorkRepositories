using EmirhanAvci.WebApi.Authentication;
using EmirhanAvci.WebApi.BusinessParticles.Abstract;
using EmirhanAvci.WebApi.Models.Concrete;
using EmirhanAvci.WebApi.Models.Messages.SuccessMessages;
using EmirhanAvci.WebApi.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CoinController : ControllerBase
    {
        private readonly ICoinService _coinService;

        public CoinController(ICoinService coinService)
        {
            _coinService = coinService;
        }

        [Authorize(Roles =Role.Admin)]
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
        [HttpGet("{id}")]
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
        [Authorize(Roles =Role.Admin)]
        [HttpPost]
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


        [Authorize(Roles =Role.Admin)]
        [HttpPut("{strId}")]
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

        [Authorize(Roles =Role.Admin)]
        [HttpPatch("{strId}")]
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

        [Authorize(Roles =Role.Admin)]
        [HttpDelete("{strId}")]
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
        //[HttpGet("GetAllByCategory")]
        //public IActionResult GetSortByCategory()
        //{
        //    return Ok(CoinDataListGenerator.coinsList.OrderBy(o => o.CategoryId).ToList());
        //}

        //[HttpGet("GetAllByNetwork")]
        //public IActionResult GetSortByNetwork()
        //{
        //    return Ok(CoinDataListGenerator.coinsList.OrderBy(o => o.NetworkId).ToList());
        //}
    }
}
