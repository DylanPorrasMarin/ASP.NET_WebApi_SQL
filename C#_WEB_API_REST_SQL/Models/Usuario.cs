using Microsoft.AspNetCore.Identity;

namespace C__WEB_API_REST_SQL.Models
{
    public class Usuario

    {
        //CLASE PARA SIMULAR BASE DE DATOS
        public string idUsuario { get; set; }
        public string usuario { get; set;}

        public string password { get; set; }

        public string role { get; set; }

        public static List<Usuario> DB() {

            var list = new List<Usuario>()
            {
                new Usuario
                {
                idUsuario = "1",
                usuario = "Mateo",
                password = "123",
                role = "empleado"
                },

                new Usuario
                {
                idUsuario = "2",
                usuario = "Marcos",
                password = "123",
                role = "empleado"
                },

                new Usuario
                {
                idUsuario = "3",
                usuario = "Lucas",
                password = "123",
                role = "Asesor"
                },
                new Usuario
                {
                idUsuario = "4",
                usuario = "Juan",
                password = "123",
                role = "Administrador"
                }
            };

            return list;
        }
    }
}
