using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetworkCustomerCounter.Core.Application;
using NetworkCustomerCounter.Core.Domain.Interfaces;
using NetworkCustomerCounter.Core.Domain.Models;
using NetworkCustomerCounter.Core.Infrastructure;

namespace NetworkCustomerCounterApi.Core
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
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            })
                .ConfigureApiBehaviorOptions(setupAction =>
                {
                    //executed when model is invalid
                    setupAction.InvalidModelStateResponseFactory = context =>
                    {
                        // create a problem details object
                        var problemDetailsFactory = context.HttpContext.RequestServices
                            .GetRequiredService<ProblemDetailsFactory>();
                        var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                            context.HttpContext,
                            context.ModelState);

                        //add additional info not added by default
                        problemDetails.Detail = "Please see the errors field for more details.";
                        problemDetails.Instance = context.HttpContext.Request.Path;

                        //find out which status code to use
                        var actionExecutingContext = context as ActionExecutingContext;

                        //if there are modelstate errors excluding empty body or unparsable values
                        if (context.ModelState.ErrorCount > 0 &&
                            actionExecutingContext?.ActionArguments.Count ==
                            context.ActionDescriptor.Parameters.Count)
                        {
                            problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                            problemDetails.Title = "One or more validation errors occured.";

                            return new UnprocessableEntityObjectResult(problemDetails)
                            {
                                ContentTypes = { "application/problem+json" }
                            };
                        }

                        ;

                        //if an arugument wasn't found or couldn't be parsed
                        problemDetails.Status = StatusCodes.Status400BadRequest;
                        problemDetails.Title = "One or more errors on input occured.";
                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                });

            services.AddScoped<ICustomerCountRequestProcessor, CustomerCountRequestProcessor>();
            services.AddScoped<INetwork, Network>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
