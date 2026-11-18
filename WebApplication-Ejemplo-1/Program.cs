using Microsoft.EntityFrameworkCore;
using WebApplication_Ejemplo_1.Data;
using WebApplication_Ejemplo_1.Interfaces;
using WebApplication_Ejemplo_1.Services;
using WebApplication_Ejemplo_1.Factories;

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

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            //activamos 
            app.UseStaticFiles();

            app.UseCors("PermitirBlazor");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
