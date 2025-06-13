using Moq;
using WorldTracker.Application.Services;
using WorldTracker.Common.DTOs;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IRepositories;
using WorldTracker.Domain.IServices;

namespace WorldTracker.Tests.Services
{
    public class CountryServiceTests
    {
        private readonly Mock<ICountryRepository> _repositoryMock;
        private readonly Mock<IExternalCountryService> _externalCountryServiceMock;
        private readonly CountryService _countryService;

        public CountryServiceTests()
        {
            _repositoryMock = new Mock<ICountryRepository>();
            _externalCountryServiceMock = new Mock<IExternalCountryService>();
            _countryService = new CountryService(_repositoryMock.Object, _externalCountryServiceMock.Object);
        }

        [Fact]
        public async Task GetAllCountriesAsync_ShouldFetchAndSave_WhenRepositoryIsEmpty()
        {
            var externalCountries = new List<Country>
            {
                CreateSampleCountry("BRA", "Brazil"),
                CreateSampleCountry("CAN", "Canada")
            };

            _repositoryMock.Setup(r => r.HasAnyAsync()).ReturnsAsync(false);
            _repositoryMock.Setup(r => r.SaveManyAsync(externalCountries)).Returns(Task.CompletedTask);
            _externalCountryServiceMock.Setup(e => e.GetCountriesAsync()).ReturnsAsync(externalCountries);

            var result = await _countryService.GetAllCountriesAsync();

            Assert.Equal(2, result.Count());

            _repositoryMock.Verify(r => r.HasAnyAsync(), Times.Once);
            _repositoryMock.Verify(r => r.SaveManyAsync(externalCountries), Times.Once);
            _externalCountryServiceMock.Verify(e => e.GetCountriesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllCountriesAsync_ShouldReturnFromRepository_WhenRepositoryHasData()
        {
            var storedCountries = new List<Country>
            {
                CreateSampleCountry("USA", "United States")
            };
            
            _repositoryMock.Setup(r => r.HasAnyAsync()).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(storedCountries);

            var result = await _countryService.GetAllCountriesAsync();

            Assert.Single(result);
            Assert.Equal("USA", result.First().Code);
            
            _repositoryMock.Verify(r => r.HasAnyAsync(), Times.Once);
            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
            _externalCountryServiceMock.Verify(e => e.GetCountriesAsync(), Times.Never);
        }

        [Fact]
        public async Task GetPagedCountriesAsync_ShouldFetchAndSave_WhenRepositoryIsEmpty()
        {
            var externalCountries = new List<Country>
            {
                CreateSampleCountry("BRA", "Brazil")
            };

            var pagedResult = new PagedResultDto<Country>
            {
                Items = externalCountries
            };

            _repositoryMock.Setup(r => r.HasAnyAsync()).ReturnsAsync(false);
            _repositoryMock.Setup(r => r.SaveManyAsync(externalCountries)).Returns(Task.CompletedTask);
            _externalCountryServiceMock.Setup(e => e.GetCountriesAsync()).ReturnsAsync(externalCountries);

            _repositoryMock.Setup(r => r.GetPagedAsync(It.IsAny<PagedRequestDto>())).ReturnsAsync(pagedResult);

            var result = await _countryService.GetPagedCountriesAsync(new PagedRequestDto());

            Assert.Single(result.Items);

            _repositoryMock.Verify(r => r.HasAnyAsync(), Times.Once);
            _repositoryMock.Verify(r => r.SaveManyAsync(externalCountries), Times.Once);
            _repositoryMock.Verify(r => r.GetPagedAsync(It.IsAny<PagedRequestDto>()), Times.Once);
            _externalCountryServiceMock.Verify(e => e.GetCountriesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetPagedCountriesAsync_ShouldReturnPaged_WhenRepositoryHasData()
        {
            _repositoryMock.Setup(r => r.HasAnyAsync()).ReturnsAsync(true);

            var pagedResult = new PagedResultDto<Country>
            {
                Items = new List<Country>
                {
                    CreateSampleCountry("CAN", "Canada")
                },
            };

            _repositoryMock.Setup(r => r.GetPagedAsync(It.IsAny<PagedRequestDto>())).ReturnsAsync(pagedResult);

            var result = await _countryService.GetPagedCountriesAsync(new PagedRequestDto());

            Assert.Single(result.Items);

            _repositoryMock.Verify(r => r.HasAnyAsync(), Times.Once);
            _repositoryMock.Verify(r => r.GetPagedAsync(It.IsAny<PagedRequestDto>()), Times.Once);
            _externalCountryServiceMock.Verify(e => e.GetCountriesAsync(), Times.Never);
        }

        [Fact]
        public async Task GetCountriesByCodesAsync_ShouldReturnCountriesFromRepository()
        {
            var codes = new[] { "BRA", "CAN" };
            var countries = new List<Country>
            {
                CreateSampleCountry("BRA", "Brazil"),
                CreateSampleCountry("CAN", "Canada")
            };

            _repositoryMock.Setup(r => r.GetByCodesAsync(codes)).ReturnsAsync(countries);

            var result = await _countryService.GetCountriesByCodesAsync(codes);

            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Code == "BRA");
            Assert.Contains(result, c => c.Code == "CAN");

            _repositoryMock.Verify(r => r.GetByCodesAsync(codes), Times.Once);
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
    }
}
