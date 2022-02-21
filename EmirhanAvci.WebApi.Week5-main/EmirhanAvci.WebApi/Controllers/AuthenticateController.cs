using EmirhanAvci.WebApi.Authentication;
using EmirhanAvci.WebApi.Authentication.Models;
using EmirhanAvci.WebApi.Authentication.ResponseMessages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> _user;
        private readonly RoleManager<IdentityRole> _role;
        private readonly IConfiguration _configuration;

        #region Summary
        /*
         * IConfiguration : This interface used for reading appsettings.json's sections and json objects.
         * UserManager : This class used for manage user operations and informations.
         * RoleManager : This class used for manage role operations and informations.
         * We are using Task<IActionResult> actions. Because multiple users can try login or register at the same time
         * Claims used for keep information about the user and authorize with roles.
         * Guid used for to create unique values(We are using for to help to create unique token)
         * Audience : This is the field where we determine who/which origins/sites will use the token value to be created.
         * Issuer : This is the field where we will express who distributes the token value to be created.
         * Expires : Lifetime
         * signingCredentials : Algorithm 
         */
        #endregion
        public AuthenticateController(UserManager<User> user, RoleManager<IdentityRole> role, IConfiguration configuration)
        {
            _user = user;
            _role = role;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _user.FindByNameAsync(model.Username);
            if (user != null && await _user.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _user.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _user.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = Message.UserExists[0], Message = Message.UserExists[1] });
            }

            else
            {
                User user = new User()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };
                var result = await _user.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = Message.UserValidation[0], Message = Message.UserValidation[1] });
                }
                else
                {
                    return Ok(new Response { Status = Message.UserCreated[0], Message = Message.UserCreated[1] });
                }
            }
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _user.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = Message.UserExists[0], Message = Message.UserExists[1] });
            }
            else
            {
                User user = new User()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };
                var result = await _user.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = Message.UserValidation[0], Message = Message.UserValidation[1] });
                }

                if (!await _role.RoleExistsAsync(Role.Admin))
                {
                    await _role.CreateAsync(new IdentityRole(Role.Admin));
                }
                if (!await _role.RoleExistsAsync(Role.User))
                {
                    await _role.CreateAsync(new IdentityRole(Role.User));
                }

                if (await _role.RoleExistsAsync(Role.Admin))
                {
                    await _user.AddToRoleAsync(user, Role.Admin);
                }
                return Ok(new Response { Status = Message.UserCreated[0], Message = Message.UserCreated[1] });
            }
           
        }
    }
}
