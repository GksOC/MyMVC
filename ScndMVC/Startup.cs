using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ScndMVC.Models;
using ScndMVC.Data;
using ScndMVC.Models.Services;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.CookiePolicy;
using System.Text.Json;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ScndMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.Always;
            });

            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // Aceita camelCase
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;  // Ignora a diferença de maiúsculas/minúsculas
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Adiciona suporte para conversão de números para Enum
            }); 

            services.AddDbContext<MainContext>(options =>
                    options.UseMySql(
                         Configuration.GetConnectionString("MainContext"),
                         mySqlOptions => mySqlOptions.MigrationsAssembly(typeof(MainContext).Assembly.FullName)
                    )
            );

            services.AddSession();

            services.AddScoped<SeedingService>(); //serviço para popular o banco de dados caso esteja vazio
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //sistema de para agilizar a obteção de dados da sessão

            services.AddScoped<FuncionarioService>();
            services.AddScoped<ConfiguracaoService>();
            services.AddScoped<ServicoService>();
            services.AddScoped<AgendamentoService>();

            services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", options =>
            {
                options.LoginPath = "/Funcionario/Login";
                options.AccessDeniedPath = "/Home/index";
                options.LogoutPath = "/Funcionario/Index";
                //options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // 🔐 Sempre usar HTTPS
                options.Cookie.SameSite = SameSiteMode.Lax; // ou Strict, dependendo do comportamento desejado
                options.Cookie.HttpOnly = true;
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService, MainContext context)
        {

            var enUS = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUS),
                SupportedCultures = new List<CultureInfo> { enUS },
                SupportedUICultures = new List<CultureInfo>() { enUS }
            };

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MainContext>();
                dbContext.Database.Migrate(); // Incializarndo a migração para o banco de dados obrigatoriamente
                seedingService.Seed(); // Populando o banco de dados com dados iniciais
            }

            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //seedingService.Seed(); redundante, pois já foi chamado acima
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            //redirecionamento em HTTPS
            //app.UseHttpsRedirection();
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseCookiePolicy();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Funcionario}/{action=Index}/{id?}");
            });
        }
    }
}
