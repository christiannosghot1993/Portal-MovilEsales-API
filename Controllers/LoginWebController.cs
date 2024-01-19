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
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace Portal_MovilEsales_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginWebController : ControllerBase
    {
        //public readonly EsalesLatamContext _dbcontext;

        public IConfiguration _configuration;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;

        public readonly EsalesLatamContext _dbcontext;

        public LoginWebController(IConfiguration configuration, 
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,
            EsalesLatamContext dbcontext)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _dbcontext = dbcontext;
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion([FromBody] Object optData)
        {
            try
            {
                ////para validar los permisos según el usuario
                //var identity = HttpContext.User.Identity as ClaimsIdentity;
                //var rToken = Jwt.validarToken(identity);
                //if (!rToken.success) return rToken;
                //String usuario = rToken.result;
                ////fin de validación de los permisos según el usuario

                var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());

                string email = data.email.ToString();
                string password = data.password.ToString();
                string codigopais = string.Empty;
                string codigoperfil = string.Empty;

                var iuser = await _userManager.FindByEmailAsync(email);

                var credenciales = await _signInManager.UserManager.CheckPasswordAsync(iuser, password);

                if (credenciales == false)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        success = false,
                        resultcode = "500",
                        message = "Credenciales incorrectas",
                        result = ""
                    });
                }

                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),//Se estaba usando DateTime.UtcNow.ToString()), lo que causaba que la hora se adelante 5 horas más a la actual y ocacionaba que el token con caducidad de 60 minutos se detecte como caducado
                    new Claim("id", iuser.Id),
                    new Claim("usuario", iuser.UserName)
                };

                var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: singIn
                );


                var usuarionuevo = _dbcontext.AspNetUsers.FirstOrDefault(x => x.Email == email);

                if (false)
                {
                    codigopais = usuarionuevo.PlantaId.ToString();
                    codigoperfil = usuarionuevo.PerfilEsales.ToString();
                }

                return StatusCode(StatusCodes.Status200OK, new
                {
                    success = true,
                    resultcode = "200",
                    message = "credenciales validas",
                    codigoperfil = codigoperfil,
                    codigopais = codigopais,
                    result = new JwtSecurityTokenHandler().WriteToken(token)
                });
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
