using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopAmazing.Web.Data;

namespace ShopAmazing.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)//recebe as configuracoes
        {
            Configuration = configuration;
        }
        //e a partir daqui que vai ser arrancada a aplicacao com o dependency injection
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)//e aqui que os servicos vao ser configurados
        {
            //Configurar a connection string nos servicos
            services.AddDbContext<DataContext>(config =>
            {//adiciona um contexto de dados e para usar e necessario a connection string
                config.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });



            //Aqui sao injectados os servicos que vao sendo criados


            services.AddTransient<SeedDb>();//injectar a seed db (Dependency injection)
            services.AddScoped<IRepository, Repository>();//Injectar o repositorio. em scoped porque ele e chamado sempre que se efectura uma action O startup compila e ve o interface e depois chama a class


            //teste Mock
            //services.AddScoped<IRepository, MockRepository>();


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())//aqui e feito o arranque por desenvolvimento ou deploy
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");//as routes sao configuradas aqui
            });
        }
    }
}
