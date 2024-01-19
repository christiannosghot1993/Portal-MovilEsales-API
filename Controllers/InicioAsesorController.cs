using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portal_MovilEsales_API.Models;
using System.Security.Claims;

namespace Portal_MovilEsales_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InicioAsesorController : ControllerBase
    {

        public IConfiguration _configuration;


        public readonly EsalesLatamContext _dbcontext;

        public InicioAsesorController(IConfiguration configuration, 
            EsalesLatamContext dbcontext)
        {
            _configuration = configuration;
            _dbcontext = dbcontext;
        }

        //Se debe especificar el esquema Bearer en el AuthenticationSchemes para que la autorización funcione
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public dynamic RetonaDatosInicio([FromBody] Object optData)
        {
            //para validar los permisos según el usuario
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = Jwt.validarToken(identity);
            if (!rToken.success) return rToken;
            String usuario = rToken.result;
            //fin de validación de los permisos según el usuario

            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());

            string email = data.email.ToString();
            string password = data.password.ToString();
            string codigopais = string.Empty;
            string codigoperfil = string.Empty;

            return StatusCode(StatusCodes.Status200OK, new
            {
                success = true,
                resultcode = "200",
                message = "credenciales validas",
                codigoperfil = codigoperfil,
                codigopais = codigopais,
                result = ""
            });
        }
    }
}
