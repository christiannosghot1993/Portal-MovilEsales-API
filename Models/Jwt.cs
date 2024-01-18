using System.Security.Claims;

namespace Portal_MovilEsales_API.Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public static dynamic validarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Verificar si estás enviando un token válido",
                        result = ""
                    };
                }

                var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;

                EsalesLatamContext objContext = new EsalesLatamContext();

                var usuarioLogeado = objContext.AspNetUsers.FirstOrDefault(x => x.Id == id);

                return new
                {
                    success = true,
                    message = "exito",
                    result = usuarioLogeado
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Catch: " + ex.Message,
                    result = ""
                };
            }
        }
    }
}
