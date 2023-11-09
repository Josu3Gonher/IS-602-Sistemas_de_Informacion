using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApi_Foro.Entities;

namespace WebApi_Foro
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Add DBContext
            services.AddDbContext<PacientesDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DbPacientesConnection"));
            });

            services.AddDbContext<CitasDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DbCitasConnection"));
            });

            // Add CORS
            services.AddCors(opciones =>
            {
                opciones.AddPolicy("CorsRule", rule =>
                {
                    rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
                });
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsRule");

            app.UseAuthorization();

            app.UseEndpoints(enpoints =>
            {
                enpoints.MapControllers();
            });
        }
    }
}
