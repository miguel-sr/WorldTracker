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

            var expressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                [":category"] = new AttributeValue { S = "Country" }
            };

            string keyConditionExpression = "Category = :category";
            Dictionary<string, string>? expressionAttributeNames = null;

            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                keyConditionExpression += " AND begins_with(#nameLower, :namePrefix)";
                expressionAttributeValues[":namePrefix"] = new AttributeValue { S = request.Filter.ToLowerInvariant() };
                expressionAttributeNames = new Dictionary<string, string>
                {
                    ["#nameLower"] = "NameLower"
                };
            }

            var query = new QueryRequest
            {
                TableName = Country.TABLE_NAME,
                IndexName = "Category-NameLower-index",
                KeyConditionExpression = keyConditionExpression,
                ExpressionAttributeValues = expressionAttributeValues,
                ExpressionAttributeNames = expressionAttributeNames,
                Limit = request.Size,
                ExclusiveStartKey = startKey,
                ScanIndexForward = true,
            };

            var response = await client.QueryAsync(query);

            var countries = response.Items
                .Select(Document.FromAttributeMap)
                .Select(context.FromDocument<Country>);

            return new PagedResultDto<Country>
            {
                Items = countries,
                PaginationToken = PaginationTokenHelper.Encode(response.LastEvaluatedKey)
            };
        }

        public async Task<IEnumerable<Country>> GetByCodesAsync(string[] codes)
        {
            var keys = codes.Select(code => new Dictionary<string, AttributeValue>
            {
                [nameof(Country.Code)] = new AttributeValue { S = code }
            }).ToList();

            var batchRequest = new BatchGetItemRequest
            {
                RequestItems = new Dictionary<string, KeysAndAttributes>
                {
                    [Country.TABLE_NAME] = new KeysAndAttributes
                    {
                        Keys = keys
                    }
                }
            };

            var response = await client.BatchGetItemAsync(batchRequest);

            var countries = response.Responses[Country.TABLE_NAME]
                .Select(Document.FromAttributeMap)
                .Select(context.FromDocument<Country>);

            return countries;
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
