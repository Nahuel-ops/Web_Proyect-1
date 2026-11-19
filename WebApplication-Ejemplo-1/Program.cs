using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApplication_Ejemplo_1.Data;
using WebApplication_Ejemplo_1.Factories;
using WebApplication_Ejemplo_1.Interfaces;
using WebApplication_Ejemplo_1.Services;

namespace WebApplication_Ejemplo_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Habilitar CORS para que Blazor / Razor pueda consumir la API
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("PermitirBlazor", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Cadena de conexión del appsettings.json
            var db = builder.Configuration.GetConnectionString("ConexionDB");

            // Registrar el contexto de la base de datos en el contenedor de servicios
            builder.Services.AddDbContext<BaseDeDatos>(options => options.UseSqlServer(db));

            builder.Services.AddControllers();

            // Inyección de dependencias
            builder.Services.AddScoped<ICancha, CanchaServices>();
            builder.Services.AddScoped<ICliente, ClienteServices>();
            builder.Services.AddScoped<IReserva, ReservaServices>();
            builder.Services.AddScoped<IAuthInterface, AuthServices>();

            // Configuración de Autenticación JWT
            var jwtKey = builder.Configuration["Jwt:Key"]!;
            var jwtIssuer = builder.Configuration["Jwt:Issuer"];
            var jwtAudience = builder.Configuration["Jwt:Audience"];

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

            builder.Services.AddEndpointsApiExplorer();

            // Configuración de Swagger con soporte para Token JWT (Botón Authorize)
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication-Ejemplo-1", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Ingresa tu token JWT aquí (sin escribir 'Bearer')."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // >>> AQUÍ VA EL BLOQUE DE LAS DOS OPCIONES DE SEMBRADO <<<
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<BaseDeDatos>();

                // OPCIÓN 1: Fábrica estática (CanchaFactories)
                /*
                if (!context.canchas.Any())
                {
                    context.canchas.AddRange(CanchaFactories.CrearCanchas());
                    context.SaveChanges();
                }
                */

                // OPCIÓN 2: Fábrica personalizada con Bogus (CanchasFactories)
                if (!context.canchas.Any())
                {
                    CanchasFactories.Seed(context);
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Activamos archivos estáticos
            app.UseStaticFiles();

            app.UseCors("PermitirBlazor");

            // ¡Orden obligatorio: primero Authentication y luego Authorization!
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
