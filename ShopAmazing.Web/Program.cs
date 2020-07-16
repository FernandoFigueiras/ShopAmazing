using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ShopAmazing.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();//controi um host que vai correr em cima de um qq servidor (Windows / Linux / Mac os)
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();//Aqui cria um host e injecta o startup com as configuracoes que vao ser contruidas
    }//injecta as configuracoes que estao no startup, qu atravez do IOC faz a gestao das instancias que sao necessarias
}
