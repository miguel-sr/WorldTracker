using WorldTracker.Application.Services;
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
            services.AddScoped<ITokenService, JwtTokenService>();

            services.AddHttpClient<IExternalWeatherService, ExternalWeatherService>(client =>
            {
                var baseUrl = Constants.ENV_OPEN_WEATHER_BASE_URL.GetRequiredEnvironmentVariable();

                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddScoped<ICountryRepository, CountryDynamoRepository>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddHttpClient<IExternalCountryService, ExternalCountryService>(client =>
            {
                var baseUrl = Constants.ENV_REST_COUNTRY_BASE_URL.GetRequiredEnvironmentVariable();

                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddScoped<IUserFavoriteRepository, UserFavoriteDynamoRepository>();
            services.AddScoped<IUserFavoriteService, UserFavoriteService>();
        }
    }
}
