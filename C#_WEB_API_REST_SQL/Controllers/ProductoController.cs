﻿using C__WEB_API_REST_SQL.Models;
using C__WEB_API_REST_SQL.Recursos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace C__WEB_API_REST_SQL.Controllers
{
    [ApiController]
    [Route("producto")]
    public class ProductoController : ControllerBase
    {

        [HttpGet]
        [Route("listar")]
        public dynamic ListarProductos()
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@Estado","1") //Se usa la clase parametro para pasar lo los valores de la tabla

            };
            DataTable tCategoria = DBDatos.Listar("Categoria_Listar", parametros);
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

        [HttpPost]
        [Route("agregar")]

        public dynamic AgregarProducto(Producto producto)

        {
            //CON ES PEDAZO DE CODIGO HACEMOS QUE EL PROCEDIMIENTO ALMACENADO DE LA BD AGREGRAR_PRODUCTO, SE USE MANDANDOLE POR PARAMETROS LOS ATRIBUTOS QUE PIDE EL MISMO PROCEDIMIENTO Y TABLA PARA AGREGAR UN NUEVO PRODUCTO
            List<Parametro> parametros = new List<Parametro>
        {
            new Parametro("@IDCategoria", producto.IDCategoria),
            new Parametro("@Nombre",  producto.Nombre),
            new Parametro("@Precio", producto.Precio )
        };


            //se utliza el metodo ejecutar de la clase DBDatos, recibe como primer parametro el procedimiento almacenado de sql server "Prodcuto_Agregar y los parametros del producto a agregar en la tabla
            //Se utiliza bool por que el metodo de la clase DBDatos es un metodo que retorna un booleano
            dynamic result = DBDatos.Ejecutar("Producto_Agregar", parametros);

            return new
            {
                success = result.exito,
                message = result.mensaje,  //HACIENDO USO DE Ternario
                result = ""
            };
        }

    }
    
} 
  
