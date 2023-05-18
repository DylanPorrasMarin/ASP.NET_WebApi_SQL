using C__WEB_API_REST_SQL.Models;
using C__WEB_API_REST_SQL.Recursos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace C__WEB_API_REST_SQL.Controllers
{
    [ApiController]
    [Route("producto")]
    public class ProductoController: ControllerBase
    {

        [HttpGet]
        [Route("listar")]
        public dynamic ListarProductos( ) 
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@Estado","1") //Se usa la clase parametro para pasar lo los valores de la tabla

            };
           DataTable tCategoria =  DBDatos.Listar("Categoria_Listar", parametros);
           DataTable tProducto = DBDatos.Listar("Producto_Listar");

           string jsonCategoria = JsonConvert.SerializeObject(tCategoria);//Hace que la informacion de la tabla la convierta en un json string 
           string jsonProducto = JsonConvert.SerializeObject(tProducto);

            return new
            {
                success = true,
                message = "Exito",
                result = new
                {
                    categoria = JsonConvert.DeserializeObject<List<Categoria>>(jsonCategoria), //Convierte el json string en objecto
                    producto = JsonConvert.DeserializeObject<List<Producto>>(jsonProducto)
                }
            };
        }
    }
}
