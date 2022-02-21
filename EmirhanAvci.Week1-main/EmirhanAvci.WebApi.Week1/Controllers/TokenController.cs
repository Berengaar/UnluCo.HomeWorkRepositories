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
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpGet("GetAllForAdmins")]
        public IActionResult Get()
        {
            return Ok(CoinDataListGenerator.tokensList);
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
            return Ok(CoinDataListGenerator.tokensList.Where(w => w.VisibilityStatus == true));
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            if (id > 0)
            {
                var selectedToken = CoinDataListGenerator.tokensList.FirstOrDefault(f => f.Id == id);
                if (selectedToken != null)
                {
                    return Ok(selectedToken);
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
        public IActionResult Add([FromBody] Token token)
        {
            TokenValidation tokenValidation = new TokenValidation();
            if (tokenValidation.AddControl(token))
            {
                try
                {
                    CoinDataListGenerator.tokensList.Add(token);
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
                return Created("Index", new { message = TokenSuccessMessage.TokenAddedMessage });
            }
        }


        [HttpPut("Update/{id}")]
        public IActionResult Update(string id, [FromBody] Token token)
        {
            TokenValidation tokenValidation = new TokenValidation();
            var numericControlObject = tokenValidation.UpdateControl(id, token);
            //Item1 = Id Number
            //Item2 = ControlValue
            if (numericControlObject.Item1 != 0 && numericControlObject.Item2 != -1)
            {
                var updatedToken = CoinDataListGenerator.tokensList.FirstOrDefault(f => f.Id == numericControlObject.Item1);
                updatedToken.TokenName = token.TokenName != default ? updatedToken.TokenName : token.TokenName;
                updatedToken.TokenCap = token.TokenCap != default ? updatedToken.TokenCap : token.TokenCap;
                updatedToken.TokenListDate = token.TokenListDate != default ? updatedToken.TokenListDate : token.TokenListDate;
                updatedToken.TokenMaxSupply = token.TokenMaxSupply != default ? updatedToken.TokenMaxSupply : token.TokenMaxSupply;
                updatedToken.TokenTotalSupply = token.TokenTotalSupply != default ? updatedToken.TokenTotalSupply : token.TokenTotalSupply;
                updatedToken.TokenPriceAvg = token.TokenPriceAvg != default ? updatedToken.TokenPriceAvg : token.TokenPriceAvg;
                updatedToken.NetworkId = token.NetworkId != default ? updatedToken.NetworkId : token.NetworkId;
                updatedToken.CategoryId = token.CategoryId != default ? updatedToken.CategoryId : token.CategoryId;
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch("UpdateName/{id}")]
        public IActionResult UpdateName(string id, [FromBody] Token token)
        {
            TokenValidation tokenValidation = new TokenValidation();
            var numericControlObject = tokenValidation.UpdateControl(id, token);
            if (numericControlObject.Item1 != 0 && numericControlObject.Item2 != -1)
            {
                var updatedToken = CoinDataListGenerator.tokensList.FirstOrDefault(f => f.Id == numericControlObject.Item1);
                updatedToken.TokenName = token.TokenName != default ? token.TokenName : updatedToken.TokenName;
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteCompletely/{id}")]
        public IActionResult DeleteCompletely(string id, [FromBody] Token token)
        {
            TokenValidation tokenValidation = new TokenValidation();
            var numericControlObject = tokenValidation.UpdateControl(id, token);
            if (numericControlObject.Item1 != 0 && numericControlObject.Item2 != -1)
            {
                var deletedToken = CoinDataListGenerator.tokensList.FirstOrDefault(f => f.Id == numericControlObject.Item1);
                CoinDataListGenerator.tokensList.Remove(deletedToken);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        public IActionResult Delete(string id, [FromBody] Token token)
        {
            TokenValidation tokenValidation = new TokenValidation();
            var numericControlObject = tokenValidation.UpdateControl(id, token);
            if (numericControlObject.Item1 != 0 && numericControlObject.Item2 != -1)
            {
                var deletedToken = CoinDataListGenerator.tokensList.FirstOrDefault(f => f.Id == numericControlObject.Item1);
                deletedToken.VisibilityStatus = false;
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
            return Ok(CoinDataListGenerator.tokensList.OrderBy(o => o.CategoryId).ToList());
        }

        [HttpGet("GetAllByNetwork")]
        public IActionResult GetSoryByNetwork()
        {
            return Ok(CoinDataListGenerator.tokensList.OrderBy(o => o.NetworkId).ToList());
        }
    }
}
