using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShopAmazing.Web.Data;

namespace ShopAmazing.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Esta parte do builder sai por causa da seed BD que tem de verificar se a base de dados esta criada para correr o seed e so depois o server pode correr

            var host = CreateWebHostBuilder(args).Build();
            RunSeeding(host);//esta desdobrado porque primeiro temos de mandar correr o seeddb
            host.Run();

            //CreateWebHostBuilder(args).Build().Run();//controi um host que vai correr em cima de um qq servidor (Windows / Linux / Mac os)
        }

        private static void RunSeeding(IWebHost host)
        {
            //Factory design patter 
            var ScopeFactory = host.Services.GetService<IServiceScopeFactory>();//servico que permite gerar servicos, pesquisa um servico que gera servicos
            using (var scope = ScopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<SeedDb>();//injecta o seeddb na fabrica
                seeder.SeedAsync().Wait();//espera que esteja criado para correr o host no main.
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();//Aqui cria um host e injecta o startup com as configuracoes que vao ser contruidas
    }//injecta as configuracoes que estao no startup, qu atravez do IOC faz a gestao das instancias que sao necessarias
}
