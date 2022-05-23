using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestApi.Configuration;
using TestApi.Utilities;
using TestDB.DTOs;
using TestLogic;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SegurityController : ControllerBase
    {
        private readonly IOptions<Urls> _config;
        private readonly IConfiguration _configuration;
        public Service _service = new Service();
        public TokenJWT tokenJWT = new TokenJWT();

        public SegurityController(IOptions<Urls> config, IConfiguration configuration)
        {
            _config = config;
            _configuration = configuration;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> getUser(string user, string password)
        {
            var result = "";
            List<UserDto> users = new List<UserDto>();
            try
            {

                result = _service.getUsers(_config.Value.UrlApi).Result;
                users = JsonConvert.DeserializeObject<List<UserDto>>(result);

                var usuarioExiste = users.Where(c => c.UserName == user).FirstOrDefault();

                if(usuarioExiste == null)
                {
                    return NotFound("Usuario no encontrado");
                }

                if(usuarioExiste.Password == password)
                {
                    var token = tokenJWT.GenerateToken(usuarioExiste, _configuration);
                    return Ok(new { token });
                }
                else
                {
                    return NotFound("Credenciales invalidas");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        
    }
}
