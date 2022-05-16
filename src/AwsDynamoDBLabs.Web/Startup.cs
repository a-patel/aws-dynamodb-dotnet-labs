#region Imports
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using AwsDynamoDBLabs.Web.Data;
using AwsDynamoDBLabs.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
#endregion

namespace AwsDynamoDBLabs.Web
{
    public class Startup
    {
        private readonly IHostEnvironment _env;

        public Startup(IHostEnvironment env)
        {
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsDevelopment())
            {
                services.AddSingleton<IAmazonDynamoDB>(cc =>
                {
                    var clientConfig = new AmazonDynamoDBConfig { ServiceURL = "http://localhost:8000" };
                    return new AmazonDynamoDBClient(clientConfig);
                });
            }
            else
            {
                services.AddAWSService<IAmazonDynamoDB>();
                services.AddDefaultAWSOptions(
                    new AWSOptions
                    {
                        Region = RegionEndpoint.GetBySystemName("us-west-2")
                    });
            }

            services.AddSingleton<IMovieService, MovieService>();
            services.AddSingleton<IMovieRepository, MovieRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.AddControllers();
            });
        }
    }
}
