using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using prj.Model;
using prj.Model.DTO;
using prj.Model.Helpers;
using prj.Repository.Interface;
using prj.Service.Implementation;

namespace TestUserProfile.TestUserProfile.Service
{
    [TestClass]
    public class TestUserProfileService
    {
        private readonly Mock<IUserProfile> _profileRepoMock;
        private readonly Mock<IFormFile> _fileMock;
        private readonly UserProfileService _profileService;


        public TestUserProfileService() 
        {
            _profileRepoMock = new Mock<IUserProfile>();
            _fileMock = new Mock<IFormFile>();
            _profileService = new UserProfileService(_profileRepoMock.Object,new Mock<IConfiguration>().Object);
        }

        [TestMethod]
        public async Task UpdateUserImageReturnsChangeImageDTO()
        {
            //Assert
            ChangeImageDTO imageDTO = new()
            {
                CustomerId = 1,
                image = "img.jpg",
                file = _fileMock.Object,
            };

            UserProfile profile = new()
            {
                CustomerId = 1,
                Name = "User1",
                Dob = new DateTime(2000,12,12)
            };
            _profileRepoMock.Setup(repo => repo.GetUserProfileById(1)).ReturnsAsync(profile);
            _profileRepoMock.Setup(repo => repo.UpdateUserProfile(profile)).ReturnsAsync(profile);

            //Act
            var result = await _profileService.UpdateUserImage(1,imageDTO);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.image,imageDTO.image);

        }

        [TestMethod]
        public async Task AddUserImageReturnsChangeImageDTO()
        {
            //Assert
            ChangeImageDTO imageDTO = new()
            {
                CustomerId = 1,
                image = "img.jpg",
                file = _fileMock.Object,
            };

            UserProfile profile = new()
            {
                CustomerId = 1,
                Name = "User1",
                Dob = new DateTime(2000, 12, 12)
            };

            UserProfileDTO profileDTO = new()
            {
                CustomerId = 1,
                Dob = new DateTime(2000, 12, 12)
            };
            _profileRepoMock.Setup(repo => repo.GetUserProfileById(1)).ReturnsAsync(profile);
            _profileRepoMock.Setup(repo => repo.AddUserProfile(profile)).ReturnsAsync(profile);

            //Act
            var result = await _profileService.AddUserProfiles(profileDTO,1);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Dob, profileDTO.Dob);

        }

        [TestMethod]
        public async Task GetUserProfileReturnsUserProfile()
        {
            //Assert

             var profiles = new List<UserProfile>()
            {
                new(){
                CustomerId = 1,
                Name = "User1",
                Dob = new DateTime(2000, 12, 12)
                },
                new(){
                CustomerId = 2,
                Name = "User2",
                Dob = new DateTime(2000, 12, 12)
                },
            };
            _profileRepoMock.Setup(repo => repo.GetUserProfile()).ReturnsAsync(profiles);

            //Act
            var result = await _profileService.GetUserProfile();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count,2);

        }

        [TestMethod]
        public async Task AddUserProfileReturnsUserProfile()
        {
            //Assert

            UserProfile profile = 
                new(){
                CustomerId = 1,
                Name = "User1",
                Dob = new DateTime(2000, 12, 12)
                };
            _profileRepoMock.Setup(repo => repo.AddUserProfile(profile)).ReturnsAsync(profile);

            //Act
            var result = await _profileService.AddUserProfile(profile);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetUserbyLoginReturnsUserProfile()
        {
            //Assert

            UserProfile profile =
                new()
                {
                    CustomerId = 1,
                    Name = "User1",
                    Dob = new DateTime(2000, 12, 12)
                };

            UserLoginDTO loginDTO = new()
            {
                CustomerId = 1,

            };

            _profileRepoMock.Setup(repo => repo.GetUserProfileById(1)).ReturnsAsync(profile);

            //Act
            var result = await _profileService.GetUserLoginById(1);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateUserProfileReturnsUserProfile()
        {
            //Assert

            UserProfile profile =
                new()
                {
                    CustomerId = 1,
                    Name = "User1",
                    Dob = new DateTime(2000, 12, 12)
                };
            _profileRepoMock.Setup(repo => repo.UpdateUserProfile(profile)).ReturnsAsync(profile);

            //Act
            var result = await _profileService.UpdateUserProfile(profile);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DeleteUserProfileReturnsUserProfile()
        {
            //Assert

            UserProfile profile =
                new()
                {
                    CustomerId = 1,
                    Name = "User1",
                    Dob = new DateTime(2000, 12, 12)
                };
            _profileRepoMock.Setup(repo => repo.DeleteUserProfile(profile.CustomerId)).ReturnsAsync(profile);

            //Act
            var result = await _profileService.DeleteUserProfile(profile.CustomerId);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetUserProfileByIDReturnsUserProfile()
        {
            //Assert

            UserProfile profile =
                new()
                {
                    CustomerId = 1,
                    Name = "User1",
                    Dob = new DateTime(2000, 12, 12)
                };
            _profileRepoMock.Setup(repo => repo.GetUserProfileById(profile.CustomerId)).ReturnsAsync(profile);

            //Act
            var result = await _profileService.GetUserProfileById(profile.CustomerId);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetUserProfileByEmailReturnsUserProfile()
        {
            //Assert

            UserProfile profile =
                new()
                {
                    CustomerId = 1,
                    Name = "User1",
                    Dob = new DateTime(2000, 12, 12),
                     EmailId = "user1@gmail.com"
                };
            _profileRepoMock.Setup(repo => repo.GetUser(profile.EmailId)).ReturnsAsync(profile);

            //Act
            var result = await _profileService.GetUser(profile.EmailId);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ViewUserImage_ReturnsUpdatedImage()
        {
            UserProfile profile = new()
            {
                CustomerId = 1,
                Image = "img.jpg"
            };

            var profileDTO = new ChangeImageDTO
            {
                CustomerId = 1,
                image = "img.jpg"
            };

            _profileRepoMock.Setup(repo => repo.GetUserProfileById(1)).ReturnsAsync(profile);

            var result = await _profileService.ViewUserImage(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UserRegister_Should_Return_LoginDTO()
        {
            // Arrange
            var registerDTO = new UserRegisterDTO
            {
                CustomerId = 1,
                EmailId = "test@example.com",
                Name = "Test User",
                Password = PasswordHasher.HashPassword("Password")
            };

            var profile = new UserProfile
            {
                CustomerId = 1,
                EmailId = "test@example.com",
                Name = "Test User",
                Password = PasswordHasher.HashPassword("Password")
            };

            _profileRepoMock.Setup(repo => repo.AddUserProfile(It.IsAny<UserProfile>())).ReturnsAsync(profile);

            // Act
            var result = await _profileService.UserRegister(registerDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(registerDTO.CustomerId, result.CustomerId);
            Assert.AreEqual(registerDTO.EmailId, result.EmailId);
            Assert.AreEqual(registerDTO.Name, result.Name);
        }

    }
}