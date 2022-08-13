using System;
using MediaManager.Entities;
using MediaManager.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MediaManager
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                {
                    options.ReturnHttpNotAcceptable = true;
                })
                .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(setupAction =>
                {
                    setupAction.InvalidModelStateResponseFactory = context =>
                    {
                        //create problem detail object
                        var problemDetailFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
                        var problemDetails = problemDetailFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);

                        //add additional info
                        problemDetails.Detail = "See error fields for details";
                        problemDetails.Instance = context.HttpContext.Request.Path;

                        //find out which status code to use
                        var actionExecutingContext = context as ActionExecutingContext;

                        //if there are modelstate errors && all arguments were correctly found/parsed we're dealing with validation errors
                        if (context.ModelState.ErrorCount > 0 && actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count)
                        {
                            problemDetails.Type = "path to help page";
                            problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                            problemDetails.Title = "One or more validation errors occurred";

                            return new UnprocessableEntityObjectResult(problemDetails)
                            {
                                ContentTypes = { GetContentTypeApplicationProblemJson() }
                            };
                        }

                        //if one of the arguments wasn't correctly found/couldn't be parsed we're dealing with null / unparsable input
                        problemDetails.Status = StatusCodes.Status400BadRequest;
                        problemDetails.Title = "One or more errors on input occurred";
                        return new BadRequestObjectResult(problemDetails)
                        {
                            ContentTypes = { GetContentTypeApplicationProblemJson() }
                        };

                        string GetContentTypeApplicationProblemJson()
                        {
                            return "application/problem+json";
                        }
                    };

                });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // register the DbContext on the container, getting the connection string from
            // appSettings (note: use this during development; in a production environment,
            // it's better to store the connection string in an environment variable)
            var connectionString = Configuration["connectionStrings:ConnectionString"];
            services.AddDbContext<MediaContext>(o => o.UseSqlServer(connectionString).EnableSensitiveDataLogging());

            // register the repository
            services.AddScoped<IRepository<Movie>, MovieRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IRepository<Platform>, PlatformRepository>();
            services.AddScoped<IRepository<MediaType>, MediaTypeRepository>();
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<IApplicationConfiguration, ApplicationConfiguration>();
            services.AddScoped<IDatabaseConfiguration, DatabaseConfiguration>();
            services.AddScoped<IPageConfiguration, PageConfiguration>();
            services.AddScoped<IConfigurationRoot>(provider => Configuration);
            services.AddScoped<IUrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>().ActionContext;
                var factory = implementationFactory.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });
            services.AddTransient<IPropertyMappingService, PropertyMappingService>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MediaContext mediaContext, ILoggerFactory loggerFactory)
        {
            LoggerFactory.Create(builder => builder.AddConsole()
                                                   .AddDebug());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                        {
                            var exepExceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                            if (exepExceptionHandlerFeature != null)
                            {
                                var logger = loggerFactory.CreateLogger("Global exception logger");
                                logger.LogError(500, exepExceptionHandlerFeature.Error, exepExceptionHandlerFeature.Error.Message);
                            }
                            context.Response.StatusCode = 500;
                            await context.Response.WriteAsync("An unexpected fault occurred. Try again later.");
                        }
                    );
                });
            }

            app.UseCors(builder => builder.SetIsOriginAllowed(origin => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            UpdateDatabase(app);
        }

        private void SeedDatabase(MediaContext mediaContext)
        {
            mediaContext.EnsureSeedDataForContext();
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<MediaContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}