using CRM.Api.Configuration;
using CRM.Common.File;
using CRM.Common.LoggedIn;
using CRM.Common.Token;
using CRM.Domain;
using CRM.Persistence.Database;
using CRM.Service.Query;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;

namespace CRM.Api
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
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "The CRM Api", Version = "v1" });
            });

            // Register Entity Framework Core
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"))
            );

            // Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Identity configuration
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            // Register Common Services
            services.AddHttpContextAccessor();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IImageUploadService, ImageUploadPhysicalService>();
            services.AddTransient<ITokenCreationService, TokenCreationService>();            

            // Register Query Services
            services.AddTransient<ICustomerQueryService, CustomerQueryService>();
            services.AddTransient<IUserQueryService, UserQueryService>();

            // Register Command handlers
            services.AddMediatR(Assembly.Load("CRM.Service.EventHandler"));

            // IdentityServer Configuration
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential(persistKey: true)
                    .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                    .AddInMemoryApiResources(IdentityServerConfiguration.GetApis())
                    .AddInMemoryClients(IdentityServerConfiguration.GetClients(Configuration))
                    .AddAspNetIdentity<ApplicationUser>();
            

            // Schema Authentication
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration.GetValue<string>("IdentityServer:Authority");
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "CRM.Api";
                    options.RoleClaimType = ClaimTypes.Role;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "The CRM Api");
            });

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                 endpoints.MapControllers();
            });
        }
    }
}
