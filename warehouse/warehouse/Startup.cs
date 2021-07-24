using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto;
using warehouse.Dto.User;
using warehouse.Services.Authentication;
using warehouse.Services.IRepositories;
using warehouse.Services.Repositories;

namespace warehouse
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
            
            var authSettings = new AuthenticationSettings();

            Configuration.GetSection("Authentication").Bind(authSettings);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(config =>
            {
                config.SaveToken = true;
                config.RequireHttpsMetadata = false;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authSettings.JwtIssuer,
                    ValidAudience = authSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.JwtKey)
                    )
                };
            });
            //  services.AddAuthorization(options =>
            //{ // to do
            //  });
            services.AddControllers().AddFluentValidation();
            services.AddHttpContextAccessor();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddSingleton(authSettings);
            services.AddDbContext<WarehouseDbContext>(options => options.UseSqlServer("Server=.;Database=WarehouseAPI;Trusted_Connection=True;"));
            services.AddAutoMapper(typeof(WarehouseMapper).Assembly);
            services.AddScoped<IItemServices, ItemServices>();
            services.AddScoped<IIndexServices, IndexServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IClientServices, ClientServices>();
            services.AddControllers();
            services.AddScoped<IValidator<UserCreatedDto>, UserCreatedDtoValidation>();

            services.AddTransient<ErrorHandlingMiddleware>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "warehouse", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseResponseCaching();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "warehouse v1"));
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();
            var database = serviceProvider.GetService<WarehouseDbContext>();
            
            database.Database.Migrate();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
