using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace ScndMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            //webBuilder.ConfigureKestrel(serverOptions =>
        //            //{
        //            //    serverOptions.Listen(IPAddress.Any, 80, listenOptions =>
        //            //    {
        //            //        listenOptions.UseHttps("certs/meuCert.pfx", "3826427540028922");
        //            //    });
        //            //});
        //            webBuilder.UseStartup<Startup>();
        //        });

        //versão nova
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();

                // Permite que Kestrel escute em todas as interfaces de rede, porta 5000
                webBuilder.UseUrls("http://0.0.0.0:5000");
            });

    }
}
