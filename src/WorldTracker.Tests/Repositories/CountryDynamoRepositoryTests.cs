using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Moq;
using WorldTracker.Common.DTOs;
using WorldTracker.Domain.Entities;
using WorldTracker.Infra.Repositories;

namespace WorldTracker.Tests.Repositories
{
    public class CountryDynamoRepositoryTests
    {
        private readonly Mock<IAmazonDynamoDB> _clientMock;
        private readonly Mock<IDynamoDBContext> _contextMock;
        private readonly CountryDynamoRepository _repository;

        public CountryDynamoRepositoryTests()
        {
            _clientMock = new Mock<IAmazonDynamoDB>();
            _contextMock = new Mock<IDynamoDBContext>();

            _repository = new CountryDynamoRepository(_clientMock.Object, _contextMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllCountries_WhenCountriesExist()
        {
            var countries = new List<Country>
            {
                CreateSampleCountry("BRA", "Brazil"),
                CreateSampleCountry("CAN", "Canada")
            };

            _contextMock
                .Setup(x => x.ScanAsync<Country>(It.IsAny<IEnumerable<ScanCondition>>()))
                .Returns(MockAsyncSearch(countries));

            var result = await _repository.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task HasAnyAsync_ShouldReturnTrue_WhenCountriesExist()
        {
            var countries = new List<Country> { CreateSampleCountry("BRA", "Brazil") };

            var searchMock = new Mock<AsyncSearch<Country>>();
            searchMock.Setup(s => s.GetNextSetAsync(default)).ReturnsAsync(countries);

            _contextMock
                .Setup(x => x.ScanAsync<Country>(It.IsAny<IEnumerable<ScanCondition>>()))
                .Returns(searchMock.Object);

            var result = await _repository.HasAnyAsync();

            Assert.True(result);
        }

        [Fact]
        public async Task HasAnyAsync_ShouldReturnFalse_WhenNoCountriesExist()
        {
            var countries = new List<Country>();

            var searchMock = new Mock<AsyncSearch<Country>>();
            searchMock.Setup(s => s.GetNextSetAsync(default)).ReturnsAsync(countries);

            _contextMock
                .Setup(x => x.ScanAsync<Country>(It.IsAny<IEnumerable<ScanCondition>>()))
                .Returns(searchMock.Object);

            var result = await _repository.HasAnyAsync();

            Assert.False(result);
        }

        [Fact]
        public async Task SaveManyAsync_ShouldCallSaveAsyncForEachCountry()
        {
            var countries = new List<Country>
            {
                CreateSampleCountry("BRA", "Brazil"),
                CreateSampleCountry("USA", "United States")
            };

            _contextMock
                .Setup(x => x.SaveAsync(It.IsAny<Country>(), default))
                .Returns(Task.CompletedTask);

            await _repository.SaveManyAsync(countries);

            _contextMock.Verify(x => x.SaveAsync(It.IsAny<Country>(), default), Times.Exactly(2));
        }

        [Fact]
        public async Task GetByCodesAsync_ShouldReturnCountries_WhenCodesExist()
        {
            var codes = new[] { "BRA", "CAN" };

            var countries = new List<Country>
            {
                CreateSampleCountry("BRA", "Brazil"),
                CreateSampleCountry("CAN", "Canada")
            };

            var fakeItems = new List<Dictionary<string, AttributeValue>>
            {
                new() { { "Code", new AttributeValue("BRA") } },
                new() { { "Code", new AttributeValue("CAN") } }
            };

            var document1 = Document.FromAttributeMap(fakeItems[0]);
            var document2 = Document.FromAttributeMap(fakeItems[1]);

            _contextMock
                .Setup(x => x.FromDocument<Country>(It.Is<Document>(d => d["Code"] == "BRA")))
                .Returns(countries[0]);

            _contextMock
                .Setup(x => x.FromDocument<Country>(It.Is<Document>(d => d["Code"] == "CAN")))
                .Returns(countries[1]);

            _clientMock
                .Setup(x => x.BatchGetItemAsync(It.IsAny<BatchGetItemRequest>(), default))
                .ReturnsAsync(new BatchGetItemResponse
                {
                    Responses = new Dictionary<string, List<Dictionary<string, AttributeValue>>>
                    {
                        [Country.TABLE_NAME] = fakeItems
                    }
                });

            var result = await _repository.GetByCodesAsync(codes);

            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Code == "BRA");
            Assert.Contains(result, c => c.Code == "CAN");
        }

        [Fact]
        public async Task GetPagedAsync_ShouldReturnPagedCountries()
        {
            var countries = new List<Country>
            {
                CreateSampleCountry("BRA", "Brazil"),
                CreateSampleCountry("CAN", "Canada")
            };

            var requestDto = new PagedRequestDto
            {
                Size = 2,
                PaginationToken = null,
                Filter = null
            };

            var fakeItems = new List<Dictionary<string, AttributeValue>>
            {
                new(),
                new()
            };

            var response = new QueryResponse
            {
                Items = fakeItems,
                LastEvaluatedKey = new Dictionary<string, AttributeValue>
                {
                    { "Code", new AttributeValue("CAN") }
                }
            };

            _clientMock
                .Setup(c => c.QueryAsync(It.IsAny<QueryRequest>(), default))
                .ReturnsAsync(response);

            _contextMock
                 .SetupSequence(x => x.FromDocument<Country>(It.IsAny<Document>()))
                 .Returns(countries[0])
                 .Returns(countries[1]);

            var result = await _repository.GetPagedAsync(requestDto);

            Assert.Equal(2, result.Items.Count());
            Assert.NotNull(result.PaginationToken);
        }


        private Country CreateSampleCountry(string code, string name)
        {
            return new Country
            {
                Code = code,
                Name = name,
                Region = "Test Region",
                Subregion = "Test Subregion",
                Flag = new Flag { Url = $"https://flagcdn.com/{code.ToLower()}.svg" }
            };
        }

        private AsyncSearch<Country> MockAsyncSearch(List<Country> result)
        {
            var searchMock = new Mock<AsyncSearch<Country>>();
            searchMock.Setup(s => s.GetRemainingAsync(default)).ReturnsAsync(result);

            return searchMock.Object;
        }
    }
}
