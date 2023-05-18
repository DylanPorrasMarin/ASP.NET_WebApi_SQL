using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

                                   //JwT = Json Web Token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        // Configuración de validación del token
        ValidateIssuer = true,  // Validar el emisor del token
        ValidateAudience = true,  // Validar el destinatario del token
        ValidateLifetime = true,  // Validar la vigencia del token
        ValidateIssuerSigningKey = true,  // Validar la clave de firma del token

        // Emisor y destinatario válidos para el token
        ValidIssuer = builder.Configuration["Jwt:Issuer"],  // Emisor válido obtenido de la configuración
        ValidAudience = builder.Configuration["Jwt:Issuer"],  // Destinatario válido obtenido de la configuración

        // Clave de firma del token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))  // Clave de firma obtenida de la configuración
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//AGREGADOS ESTOS 2
app.UseAuthentication();
app.UseAuthorization();


app.UseAuthorization();

app.MapControllers();

app.Run();
