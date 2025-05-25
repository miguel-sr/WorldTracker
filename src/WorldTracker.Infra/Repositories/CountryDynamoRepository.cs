using Amazon.DynamoDBv2.DataModel;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IRepositories;

namespace WorldTracker.Infra.Repositories
{
    public class CountryDynamoRepository(IDynamoDBContext context) : ICountryRepository
    {
        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await context.ScanAsync<Country>([]).GetRemainingAsync();
        }

        public async Task<bool> HasAnyAsync()
        {
            var search = context.ScanAsync<Country>([]);
            var firstBatch = await search.GetNextSetAsync();

            return firstBatch.Any();
        }

        public async Task SaveManyAsync(IEnumerable<Country> countries)
        {
            var tasks = countries.Select(country => context.SaveAsync(country));
            await Task.WhenAll(tasks);
        }
    }
}
