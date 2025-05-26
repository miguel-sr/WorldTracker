using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using WorldTracker.Common.DTOs;
using WorldTracker.Common.Helpers;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IRepositories;

namespace WorldTracker.Infra.Repositories
{
    public class CountryDynamoRepository(IAmazonDynamoDB client, IDynamoDBContext context) : ICountryRepository
    {
        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await context.ScanAsync<Country>([]).GetRemainingAsync();
        }

        public async Task<PagedResultDto<Country>> GetPagedAsync(PagedRequestDto request)
        {
            var startKey = PaginationTokenHelper.Decode(request.PaginationToken);

            var query = new QueryRequest
            {
                TableName = Country.TABLE_NAME,
                IndexName = "Category-Name-index",
                KeyConditionExpression = "Category = :category",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":category", new AttributeValue { S = "Country" } }
                },
                Limit = request.Size,
                ExclusiveStartKey = startKey,
                ScanIndexForward = true
            };

            var response = await client.QueryAsync(query);

            var countries = response.Items
                 .Select(Document.FromAttributeMap)
                 .Select(context.FromDocument<Country>);

            var lastEvaluatedKey = PaginationTokenHelper.Encode(response.LastEvaluatedKey);

            return new PagedResultDto<Country>
            {
                Items = countries,
                PaginationToken = lastEvaluatedKey
            };
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
