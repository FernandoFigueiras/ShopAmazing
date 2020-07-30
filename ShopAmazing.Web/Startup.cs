using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopAmazing.Web.Data;
using ShopAmazing.Web.Data.Entities;
using ShopAmazing.Web.Helpers;

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
            //configurar a autenticacao e gestao dos users
            //aqui passamos o nosso user porque estendemos a class para juntar o first e last name, caso contrario passavamos apenas o IdentityUser
            services.AddIdentity<User, IdentityRole>(config =>
            {    //quando passar para producao temos de mudar isto
                config.User.RequireUniqueEmail = true;
                config.Password.RequireDigit = false;
                config.Password.RequiredUniqueChars = 0;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<DataContext>();






            //Configurar a connection string nos servicos
            services.AddDbContext<DataContext>(config =>
            {//adiciona um contexto de dados e para usar e necessario a connection string
                config.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });



            //Aqui sao injectados os servicos que vao sendo criados


            services.AddTransient<SeedDb>();//injectar a seed db (Dependency injection)
            /*services.AddScoped<IRepository, Repository>();*///Injectar o repositorio. em scoped porque ele e chamado sempre que se efectura uma action O startup compila e ve o interface e depois chama a class
            services.AddScoped<IProductRepository, ProductRepository>();//repositorio generico aplicado ao produto
            services.AddScoped<ICountryRepository, CountryRepository>();//repositorio generico aplicado ao Country
            services.AddScoped<IUserHelper, UserHelper>();//Bypass do UserManager do core que nao usamos directamente
            services.AddScoped<IImageHelper, ImageHelper>();
            services.AddScoped<IConverterHelper, ConverterHelper>();

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
            app.UseAuthentication();//Servico de autenticacao
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
