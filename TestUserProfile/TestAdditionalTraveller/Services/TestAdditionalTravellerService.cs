using Moq;
using prj.Model;
using prj.Repository.Interface;
using prj.Service.Implementation;


namespace TestUserProfile.TestAdditionalTraveller.Services
{
    [TestClass]
    public class TestAdditionalTravellerService
    {
        private readonly Mock<IAdditionalTravellers> _repoMock;
        private readonly AdditionalTravellersService _travellerService;

        public TestAdditionalTravellerService()
        {
            _repoMock = new Mock<IAdditionalTravellers>();
            _travellerService = new AdditionalTravellersService(_repoMock.Object);
        }

        [TestMethod]
        public async Task GetAdditionalTravellersReturnsListOfTravllers()
        {

            //Arrange
            var travellers = new List<AdditionalTraveller>()
            {
                new()
                {
                    AdditionalId = 1,
                    CustomerId = 1
                },
                new()
                {
                    AdditionalId = 2,
                    CustomerId = 2
                },
                new()
                {
                    AdditionalId = 1,
                    CustomerId = 2
                },
            };

            _repoMock.Setup(repo => repo.GetAdditionalTraveller()).ReturnsAsync(travellers);

            //Act
            var result = await _travellerService.GetAdditionalTraveller();

            //Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public async Task AddAdditionalTravellersReturnsTravller()
        {

            //Arrange
            var traveller = new AdditionalTraveller()
            {
                    AdditionalId = 1,
                    CustomerId = 1
            };

            _repoMock.Setup(repo => repo.AddAdditionalTraveller(traveller)).ReturnsAsync(traveller);

            //Act
            var result = await _travellerService.AddAdditionalTraveller(traveller);

            //Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public async Task UpdateAdditionalTravellersReturnsTravller()
        {

            //Arrange
            var traveller = new AdditionalTraveller()
            {
                AdditionalId = 1,
                CustomerId = 1
            };

            _repoMock.Setup(repo => repo.UpdateAdditionalTraveller(traveller.CustomerId , traveller)).ReturnsAsync(traveller);

            //Act
            var result = await _travellerService.UpdateAdditionalTraveller(traveller.CustomerId,traveller);

            //Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public async Task DeleteAdditionalTravellersReturnsTravller()
        {

            //Arrange
            var traveller = new AdditionalTraveller()
            {
                AdditionalId = 1,
                CustomerId = 1
            };

            _repoMock.Setup(repo => repo.DeleteAdditionalTraveller(traveller.AdditionalId)).ReturnsAsync(traveller);

            //Act
            var result = await _travellerService.DeleteAdditionalTraveller(traveller.AdditionalId);

            //Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public async Task GetAdditionalTravellersByIdReturnsTravller()
        {

            //Arrange
            var traveller = new AdditionalTraveller()
            {
                AdditionalId = 1,
                CustomerId = 1
            };

            _repoMock.Setup(repo => repo.GetAdditionalTravellerById(traveller.AdditionalId)).ReturnsAsync(traveller);

            //Act
            var result = await _travellerService.GetAdditionalTravellerById(traveller.AdditionalId);

            //Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public async Task GetAdditionalTravellersByCustomerIdReturnsTravller()
        {

            //Arrange
            var traveller = new List<AdditionalTraveller>()
            {
                  new()
                {
                    AdditionalId = 1,
                    CustomerId = 1
                },
                new()
                {
                    AdditionalId = 2,
                    CustomerId = 2
                },
                new()
                {
                    AdditionalId = 1,
                    CustomerId = 2
                },
            };

            int CustomerId = 1;

            _repoMock.Setup(repo => repo.GetAdditionalTravellerByCustomerId(CustomerId)).ReturnsAsync(traveller);

            //Act
            var result = await _travellerService.GetAdditionalTravellerByCustomerId(CustomerId);

            //Assert
            Assert.IsNotNull(result);

        }
    }
}
