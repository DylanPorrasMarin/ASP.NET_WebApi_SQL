namespace C__WEB_API_REST_SQL.Models
{
    public class Categoria
    {

        //se crea la clase para mappear con la base de datos 
        public int IDCategoria { get; set; }

        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public bool Estado { get; set; }



    }
}
