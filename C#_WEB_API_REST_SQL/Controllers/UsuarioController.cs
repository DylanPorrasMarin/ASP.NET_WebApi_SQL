using C__WEB_API_REST_SQL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace C__WEB_API_REST_SQL.Controllers
{
    [ApiController]
    [Route("Usuario")] //RUTA GENERAL DEL API
    public class UsuarioController : ControllerBase
    {
        public IConfiguration _configuration; //DECLARA LA CLASE PARA OBTENER LAS CONFIGURACIONES QUE SE NECESITAN

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration; //SE INICIALIZA CON LOS PAREMTROS

        }

        [HttpPost]
        [Route("login")] //SUB RUTA DEL API EJEMPLO usuario/login
        public dynamic IniciarSesion([FromBody] object optData) 
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString()); //DYNAMIC PARA QUE SEA CUALQUIER DATO QUE SE RECIBA

            string user = data.usuario.ToString();
            string password = data.password.ToString();
            
            Usuario usuario  = Usuario.DB().Where(x => x.usuario == user && x.password == password).FirstOrDefault(); //Se verifica con el where si el usuario recibido en la funcions de iniciar sesion es igual al metodo de BD DE LA CLASE USUARIO

            if (usuario == null) 
            {
                return new
                {
                    success = false,
                    message = "Credenciales Incorrectas",
                    result = ""
                };
            }

            var jwt =_configuration.GetSection("Jwt").Get<Jwt>(); //El getSection agarra los valores del appSettigsJson pero al crear la case Jwt podemos convertir la varible jwt en clase con EL get<Jwt>()

            //SE ESPECIFICA TODO LO QUE VA ALMACENAR NUESTRO TOKEN
            var claims = new[]
            {
                new Claim (JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim (JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim ("id", usuario.idUsuario),
                new Claim ("usuario", usuario.usuario)

            };

            //ENCRIPTANDO LA KEY
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(4),  //CUANTO TIEMPO PARA QUE VENZA EL TOKEN DEL USUARIO
                signingCredentials: singIn

                );

            return new {
                success = true,
                message = "Exito",
                result = new JwtSecurityTokenHandler().WriteToken(token)
           
            };
        }
    }
}
