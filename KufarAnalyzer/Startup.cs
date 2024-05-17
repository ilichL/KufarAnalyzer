using KufarAnalyzer.Data;
using KufarAnalyzer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using KufarAnalyzer.DataAccess;
using KufarAnalyzer.DataAccess.Abstractions;
using KufarAnalyzer.DataAccess.Implementations;
using KufarAnalyzer.Ifrastructure.Abstractions;
using KufarAnalyzer.Ifrastructure.Implementations;

namespace KufarAnalyzer
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup()
        {
            var ConfigurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddInMemoryCollection();

            Configuration = ConfigurationBuilder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<KufarContext>(opt => opt.UseSqlServer(connectionString));

            services.AddHttpClient();
            services.AddScoped<IKufarFlatRepository, KufarFlatRepository>();
            services.AddScoped<IKufarFlatOneDayRepository, KufarFlatOneDayRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IKufarFlatService, KufarFlatService>();


            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
