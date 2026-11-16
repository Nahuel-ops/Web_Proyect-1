
using Microsoft.EntityFrameworkCore;
using WebApplication_Ejemplo_1.Data;
using WebApplication_Ejemplo_1.Interfaces;
using WebApplication_Ejemplo_1.Services;
//
using WebApplication_Ejemplo_1.Factories;



namespace WebApplication_Ejemplo_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //esta linea es para obtener la cadena de conexion del appsettings.json
            var db = builder.Configuration.GetConnectionString("ConexionDB");

            //esta linea es para registrar el contexto de la base de datos en el contenedor de servicios
            builder.Services.AddDbContext<BaseDeDatos>(options => options.UseSqlServer(db));



            builder.Services.AddControllers();

            //agregamos cambio en fecha 11 de noviembre.
            builder.Services.AddScoped<ICancha, CanchaServices>();

            // este es de cliente:
            builder.Services.AddScoped<ICliente, ClienteServices>();

            // este es de reservas:
            builder.Services.AddScoped<IReserva, ReservaServices>();



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Sembrar datos iniciales (Factories) al arrancar si las tablas están vacías
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<BaseDeDatos>();

                if (!context.canchas.Any())
                {
                    context.canchas.AddRange(CanchaFactories.CrearCanchas());
                    context.SaveChanges();
                }
                /*
                if (!context.clientes.Any())
                {
                    context.clientes.AddRange(ClienteFactories.CrearClientes());
                    context.SaveChanges();
                }*/
            }



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
