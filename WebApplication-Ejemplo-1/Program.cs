
using Microsoft.EntityFrameworkCore;
using WebApplication_Ejemplo_1.Data;

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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
