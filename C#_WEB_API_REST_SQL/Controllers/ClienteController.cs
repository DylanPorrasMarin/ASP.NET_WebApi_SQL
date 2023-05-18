using C__WEB_API_REST_SQL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace C__WEB_API_REST_SQL.Controllers

    
{

    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase  //PARA QUE SEA UN CONTROLADOR
    {
        [HttpGet]
        [Route("listar")]
        public dynamic listarClientes()//DYNAMIC Se utiliza cuando el tipo exacto de un objeto no se conoce en tiempo de compilación y se necesita flexibilidad para trabajar con diferentes tipos de datos.

        {
            //AQUI SE OBTINE EL CLIENTE DE LA DB

            List<Cliente> clientes = new List<Cliente> {

                new Cliente
                {
                id="1",
                correo = "google@gmail.com",
                edad ="19",
                nombre = "Mateo"

                },

                new Cliente
                {
                id="2",
                correo = "google@gmail.com",
                edad ="20",
                nombre = "Dylan"

                }

            };
            return clientes;
        }

        [HttpGet]
        [Route("listarxid")]

        public dynamic listarClienteId(int codigo)//DYNAMIC Se utiliza cuando el tipo exacto de un objeto no se conoce en tiempo de compilación y se necesita flexibilidad para trabajar con diferentes tipos de datos.

            {
            //AQUI SE OBTINE EL CLIENTE DE LA DB
                return new
                {
                    id = codigo,
                    correo = "correo@gmail.com",
                    edad = "20",
                    nombre = "UsuarioPorId"

                };

            }


         [HttpPost]
         [Route("Guardar")]

         public dynamic guardarCliente(Cliente cliente)
            {
              cliente.id = "3";
               return new
                  {
                    success = true,
                    message = "Cliente registrado",
                    result = cliente

                  };


            }


        [HttpPost]
        [Route("eliminar")]
        [Authorize] //PARA EVITAR EL GASTO DE RECURSOS YA QUE SI O SI DEBE DE ENVIARSE UN TOKEN VALIDO
        public dynamic eliminarCliente(Cliente cliente)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var rToken = Jwt.ValidarToken(identity);

            if (!rToken.success) return rToken;

            Usuario usuario = rToken.result;

            if (usuario.role != "Administrador")
            {
                return new
                {
                    success = false,
                    message = "No tienes permisos para eliminar clientes",
                    result = ""
                };
            }

            return new
            {
                success = true,
                message = "cliente eliminado",
                result = cliente
            };
        }
    }
 }
