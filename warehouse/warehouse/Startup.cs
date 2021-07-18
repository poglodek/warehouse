using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using warehouse.Database;
using warehouse.Dto;
using warehouse.Dto.Item;
using warehouse.Services.IRepositories;
using warehouse.Services.Repositories;
using warehouse.Services.Validation;

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
            services.AddDbContext<WarehouseDbContext>(options => options.UseSqlServer("Server=.;Database=WarehouseAPI;Trusted_Connection=True;"));
            services.AddAutoMapper(typeof(WarehouseMapper).Assembly);
            services.AddScoped<IValidator<ItemCreateDto>, ItemValidation>();
            services.AddScoped<IItemServices, ItemServices>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "warehouse", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "warehouse v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            var database = serviceProvider.GetService<WarehouseDbContext>();
            database.Database.EnsureCreated();
           // database.Database.Migrate();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
