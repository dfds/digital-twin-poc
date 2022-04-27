using CloudEngineering.CodeOps.Security.Policies;
using CostJanitor.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Saunter;
using Saunter.AsyncApiSchema.v2;

namespace CostJanitor.Host.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AddHostServices(services);

            services.AddApplication(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("open");
            app.UseAuthentication();
            app.UseAuthorization();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CostJanitor.Host.Api v1"));

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapAsyncApiDocuments();
                    endpoints.MapAsyncApiUi();
                });
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        protected virtual void AddHostServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.DefaultPolicyName = "open";
                options.AddDefaultPolicy(p =>
                {
                    p.AllowAnyHeader();
                    p.AllowAnyMethod();
                    p.AllowCredentials();
                    p.WithExposedHeaders("X-Pagination");
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CostJanitor.Host.Api",
                    Version = "v1",
                    License = new OpenApiLicense 
                    {
                        Name = "MIT",
                        Url = new System.Uri("https://github.com/dfds/cost-janitor-2/blob/master/LICENSE")
                    },
                    Contact = new OpenApiContact
                    {
                        Name = "CloudEngineering",
                        Email = "ITBuildSourceDevEx@dfds.com",
                        Url = new System.Uri("https://teams.microsoft.com/l/team/19%3a908baef5ad284b7faa5d806738a1098a%40thread.tacv2/conversations?groupId=2830ad5b-41bc-4f38-a89e-34f5ceb33cd5&tenantId=73a99466-ad05-4221-9f90-e7142aa2f6c1")
                    },
                    Description = "CostJanitor is responsible for tracking capability AWS cloud expenses"
                });
            });

            services.AddAsyncApiSchemaGeneration(options =>
            {
                options.AssemblyMarkerTypes = new[] { typeof(Startup) };

                options.AsyncApi = new AsyncApiDocument
                {
                    Info = new Info("CostJanitor.Host.Api", "1.0.0")
                    {
                        Contact = new Contact 
                        {
                            Name = "CloudEngineering",
                            Email = "ITBuildSourceDevEx@dfds.com",
                            Url = "https://teams.microsoft.com/l/team/19%3a908baef5ad284b7faa5d806738a1098a%40thread.tacv2/conversations?groupId=2830ad5b-41bc-4f38-a89e-34f5ceb33cd5&tenantId=73a99466-ad05-4221-9f90-e7142aa2f6c1"
                        },
                        Description = "CostJanitor is responsible for tracking capability AWS cloud expenses",
                        License = new License("MIT License")
                        {
                            Url = "https://github.com/dfds/cost-janitor-2/blob/master/LICENSE"
                        }
                    }
                };

                options.Middleware.Route = "/asyncapi/asyncapi.json";
                options.Middleware.UiBaseRoute = "/asyncapi/ui/";
                options.Middleware.UiTitle = "CostJanitor.Host.Api AsyncAPI Documentation";
            });

            services.AddSecurityPolicies();

            AddHostAuthentication(services);
        }

        protected virtual void AddHostAuthentication(IServiceCollection services)
        {
            services.AddMicrosoftIdentityWebApiAuthentication(Configuration);
        }
    }
}