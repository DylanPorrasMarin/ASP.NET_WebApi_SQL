namespace C__WEB_API_REST_SQL.Models
{
    public class Jwt
    {
        //HACEMOS ESTA CLASE PARA QUE SEA MAS FACIL ACCEDER A LAS PROPIEDADES DE JWT
        public string key { get; set; }

        public string Issuer{ get; set;}

        public string Audience { get; set; }

        public string Subject { get; set;}

 
    }
}
