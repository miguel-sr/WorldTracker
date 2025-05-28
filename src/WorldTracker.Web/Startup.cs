using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WorldTracker.Common;
using WorldTracker.Common.Extensions;
using WorldTracker.Infra.Services;
using WorldTracker.Web.Exceptions;

namespace WorldTracker.Web
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDependencyInjections(services);
            ConfigureAuthService(services);
            ConfigureDynamoDb(services);

            services.AddProblemDetails();
            services.AddExceptionHandler<GlobalExceptionHandler>();

            services.AddControllers();

            // Verify the security about this
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "World Tracker");
                });
            }

            app.UseStaticFiles();

            app.UseExceptionHandler(options => { });

            app.UseRouting();
            app.UseCors();
            app.UseForwardedHeaders();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            { 
                endpoints.MapControllers();

                endpoints.MapFallback(context =>
                {
                    context.Response.ContentType = "text/html";
                    return context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
                });
            });
        }

        private void ConfigureDependencyInjections(IServiceCollection services)
        {
            DependencyModule.BindServices(services);
        }

        private void ConfigureAuthService(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(TokenService.GetSecuritySecret()),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        private void ConfigureDynamoDb(IServiceCollection services)
        {
            var accessKey = Constants.ENV_AWS_ACCESS_KEY_ID.GetRequiredEnvironmentVariable();
            var secretKey = Constants.ENV_AWS_SECRET_ACCESS_KEY.GetRequiredEnvironmentVariable();
            var region = Constants.ENV_AWS_DEFAULT_REGION.GetRequiredEnvironmentVariable();

            var awsOptions = new AWSOptions
            {
                Credentials = new BasicAWSCredentials(accessKey, secretKey),
                Region = RegionEndpoint.GetBySystemName(region)
            };

            services.AddDefaultAWSOptions(awsOptions);
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddScoped<IDynamoDBContext, DynamoDBContext>();
        }
    }
}
