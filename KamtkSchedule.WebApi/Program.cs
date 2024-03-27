using KamtkSchedule.WebApi.Jobs;
using KamtkSchedule.WebApi.Utils;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Quartz;
using System.Reflection;
using System.Text.Json;

namespace KamtkSchedule.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddWebApiServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            var app = builder.Build();
            app.Configure();
            app.Run();
        }
    }    

    internal static class WebApiConfiguration
    {
        internal static JsonSerializerOptions GetJsonSerializerOptions()
        {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web); ;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.WriteIndented = false;

            return options;
        }

        internal static void AddWebApiServices(
            this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(
                    new SlugifyParameterTransformer()));
            })
            .AddJsonOptions(options =>
            {
                var opts = GetJsonSerializerOptions();
                options.JsonSerializerOptions.PropertyNamingPolicy 
                    = opts.PropertyNamingPolicy;
                options.JsonSerializerOptions.WriteIndented 
                    = opts.WriteIndented;
            });



            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "KamtkSchedule.Api",
                    Version = "v1",
                    Contact = new()
                    {
                        Name = "gadzilaaa3",
                        Url = new("https://github.com/gadzilaaa3"),
                    },
                });

                string xmlFile = $"{Assembly.GetExecutingAssembly()
                    .GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory
                    , xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.EnableAnnotations();
            });

            //Quarts
            services.AddQuartz(q =>
            {
                var updateSchedulesJobKey = UpdateSchedulesJob.Key;
                q.AddJob<UpdateSchedulesJob>(opts =>
                    opts.WithIdentity(updateSchedulesJobKey));

                q.AddTrigger(opts => opts
                    .ForJob(updateSchedulesJobKey)
                    .WithIdentity($"{UpdateSchedulesJob.Key.Name}-trigger")
                    .StartNow()
                );
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }

        internal static void Configure(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json",
                        "KamtkSchedule.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
