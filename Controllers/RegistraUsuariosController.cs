using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Portal_MovilEsales_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;

namespace Portal_MovilEsales_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistraUsuariosController : ControllerBase
    {
        public IConfiguration _configuration;

        private readonly UserManager<IdentityUser> _userManager;

        public readonly EsalesLatamContext _dbcontext;

        public RegistraUsuariosController(IConfiguration configuration, UserManager<IdentityUser> userManager, EsalesLatamContext dbcontext)
        {
            _configuration = configuration;
            _userManager = userManager;
            _dbcontext = dbcontext;
        }

        [HttpPost]
        public async Task<IActionResult> Registro([FromBody] Object optData)
        {
            try 
            {
                var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());

                string user = data.email.ToString();
                string email = data.email.ToString();
                string password = data.password.ToString();
                string perfil = data.perfil.ToString();
                string planta = data.planta.ToString();

                IdentityUser iuser = new()
                {
                    Email = email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = user
                };

                var result = await _userManager.CreateAsync(iuser, password);

                if (result.Succeeded)
                {
                    var usuarionuevo = _dbcontext.AspNetUsers.FirstOrDefault(x => x.Email == email);

                    usuarionuevo.PlantaId = int.Parse(planta);
                    usuarionuevo.PerfilEsales = int.Parse(perfil);
                    usuarionuevo.IntentosIngreso = 0;
                    usuarionuevo.Estado = "Activo";

                    _dbcontext.SaveChanges();

                    return StatusCode(StatusCodes.Status201Created,
                        new
                        {
                            success = true,
                            resultcode = "200",
                            message = "Usuario creado"
                        });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new
                        {
                            success = false,
                            resultcode = "500",
                            message = "Usuario no creado"
                        });
                }
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            new
                            {
                                success = false,
                                resultcode = "500",
                                message = "Error en datos"                                
                            });
            }

        }
    }
}
