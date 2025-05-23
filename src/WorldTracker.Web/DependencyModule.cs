using WorldTracker.Common;
using WorldTracker.Common.Extensions;
using WorldTracker.Domain.IRepositories;
using WorldTracker.Domain.IServices;
using WorldTracker.Infra.Repositories;
using WorldTracker.Infra.Services;

namespace WorldTracker.Web
{
    public class DependencyModule
    {
        public static void BindServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserDynamoRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddHttpClient<IWeatherService, WeatherService>(client =>
            {
                var baseUrl = Constants.ENV_OPEN_WEATHER_BASE_URL.GetRequiredEnvironmentVariable();

                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        }
    }
}
