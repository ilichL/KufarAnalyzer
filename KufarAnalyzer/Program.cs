using Serilog;

namespace KufarAnalyzer
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
        .UseSerilog((ctx, lc) =>
        {
            lc.MinimumLevel.Information().WriteTo.Console();

        }).ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });


        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}
