using ECommerceSite.Database;
using ECommerceSite.Models;
using ECommerceSite.Models.DTOs;
using ECommerceSite.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using Microsoft.OpenApi.Models;
using ECommerceSite.Infrastructure.IRepository;
using ECommerceSite.Infrastructure.IService;
using ECommerceSite.Service;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Reflection;
using ECommerceSite.Middleware;
using System.Diagnostics.CodeAnalysis;


[ExcludeFromCodeCoverage]
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);

        // Add services to the container
        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline
        Configure(app, builder.Environment);

        app.Run();

        static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Configured Database
            services.AddDbContext<ECommerceDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ECommerceDatabase")));

            // Dependency Injection
            services.AddScoped<ICategoryService<Category>, CategoryService>();
            services.AddScoped<IProductService<Product>, ProductService>();
            services.AddScoped<ICategoryRepository<Category>, CategoryRepository>();
            services.AddScoped<IProductRepository<Product>, ProductRepository>();

            // Add controllers and configure JSON options
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.WriteIndented = true; // Optional, for better readability
                });

            // Register Swagger/OpenAPI services
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ECommerceSite APIs",
                    Version = "v1",
                    TermsOfService = new Uri("https://swagger.io/docs/specification/api-general-info/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Vaishnvee Upadhyay",
                        Email = "vaishnveeu@cybage.com",
                        Url = new Uri("https://help.com")
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            //Middleware for Global Exception handling
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}

