using System.Security.Claims;

namespace C__WEB_API_REST_SQL.Models
{
    public class Jwt
    {
        //HACEMOS ESTA CLASE PARA QUE SEA MAS FACIL ACCEDER A LAS PROPIEDADES DE JWT
        public string key { get; set; }

        public string Issuer { get; set;}

        public string Audience { get; set; }

        public string Subject { get; set;}

        //METODO GENERAL PARA VALIDAR EL TOKEN

        //Se recibe como parametro el ClaimsIdentity ya que es lo que vamos a recibir del usuario
        public static dynamic ValidarToken(ClaimsIdentity identity)
        {
            try
            {
             

                if (!identity.Claims.Any())
                {
                    return new
                    {
                        success = false,
                        message = "Verificar si estas enviando un token valido",
                        result = ""
                    };
                }

                var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;

                Usuario usuario = Usuario.DB().FirstOrDefault(x => x.idUsuario == id);

                return new
                {
                    success = true,
                    message = "exito",
                    result = usuario
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
