using Microsoft.IdentityModel.Tokens;
using prj.Model;
using prj.Model.DTO;
using prj.Model.Helpers;
using prj.Repository.Interface;
using prj.Service.Interface;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace prj.Service.Implementation
{
    public class UserProfileService : IUserProfileService
    {
        
        private readonly IUserProfile repo;
        private readonly IConfiguration _configuration;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileService"/> class.
        /// </summary>
        /// <param name="_repo">The profile repository.</param>
        /// <param name="configuration">The configuration for JWT.</param>

        public UserProfileService(IUserProfile _repo, IConfiguration configuration)
        {
            repo = _repo;
            _configuration = configuration;
        }
        #endregion

        #region GetProfile

        /// <summary>
        /// Retrieve a collection of user profiles.
        /// </summary>
        /// <returns>A task representing the asynchronous operation and returning a collection of user profiles.</returns>
        public Task<ICollection<UserProfile>> GetUserProfile()
        {
            return repo.GetUserProfile();
        }

        #endregion

        #region AddUserProfile

        /// <summary>
        /// Create a new user profile.
        /// </summary>
        /// <param name="profile">The user profile data to create.</param>
        /// <returns>A task representing the asynchronous operation and returning the created user profile.</returns>
        public Task<UserProfile> AddUserProfile(UserProfile profile)
        {
            return repo.AddUserProfile(profile);
        }

        #endregion

        #region UpdateUserProfile

        /// <summary>
        /// Update an existing user profile.
        /// </summary>
        /// <param name="Profile">The updated user profile data.</param>
        /// <returns>A task representing the asynchronous operation and returning the updated user profile.</returns>
        public Task<UserProfile> UpdateUserProfile(UserProfile Profile)
        {
            return repo.UpdateUserProfile(Profile);
        }

        #endregion

        #region DeleteUserProfile

        /// <summary>
        /// Delete a user profile by Customer ID.
        /// </summary>
        /// <param name="CustomerId">The Customer ID of the user profile to delete.</param>
        /// <returns>A task representing the asynchronous operation and returning the deleted user profile.</returns>
        public Task<UserProfile> DeleteUserProfile(int customerId)
        {
            return repo.DeleteUserProfile(customerId);
        }

        #endregion

        #region GetUserProfileById

        /// <summary>
        /// Retrieve a user profile by Customer ID.
        /// </summary>
        /// <param name="CustomerId">The Customer ID of the user profile to retrieve.</param>
        /// <returns>A task representing the asynchronous operation and returning the retrieved user profile.</returns>
        public Task<UserProfile> GetUserProfileById(int customerId)
        {
            return repo.GetUserProfileById(customerId);
        }

        #endregion

        #region GetUser

        /// <summary>
        /// Retrieve a user profile by email ID.
        /// </summary>
        /// <param name="emailId">The email ID of the user profile to retrieve.</param>
        /// <returns>A task representing the asynchronous operation and returning the retrieved user profile.</returns>
        public Task<UserProfile> GetUser(string emailId)
        {
            return repo.GetUser(emailId);
        }

        #endregion

        #region GetUserLoginById

        /// <summary>
        /// Get a login DTO by profile ID.
        /// </summary>
        /// <param name="id">The ID of the profile.</param>
        /// <returns>The login DTO with profile details.</returns>
        /// <exception cref="NullReferenceException">Thrown when the profile with id is null.</exception>

        public async Task<UserLoginDTO> GetUserLoginById(int customerId)
        {
            var profile = await repo.GetUserProfileById(customerId);

            if (profile == null)
            {
                throw new NullReferenceException($"Profile with id {customerId} not found");
            }

            var loginDTO = new UserLoginDTO
            {
                MobileNumber = profile.MobileNumber,
                EmailId = profile.EmailId,
                CustomerId = profile.CustomerId
            };

            return loginDTO;
        }

        #endregion

        #region AddUserLogin

        /// <summary>
        /// Update login information of a profile.
        /// </summary>
        /// <param name="id">The ID of the profile.</param>
        /// <param name="dto">The updated login DTO.</param>
        /// <returns>The updated login DTO.</returns>
        /// <exception cref="NullReferenceException">Thrown when the profile is null.</exception>

        public async Task<UserLoginDTO> AddUserLogin(int customerId, UserLoginDTO loginDTO)
        {
            var profile = await repo.GetUserProfileById(customerId);

            if (profile == null)
            {
                throw new NullReferenceException($"Profile with id {customerId} not found");
            }

            if (loginDTO?.MobileNumber != null)
                profile.MobileNumber = loginDTO.MobileNumber;
            if (loginDTO?.EmailId != null)
                profile.EmailId = loginDTO.EmailId;

            await repo.UpdateUserProfile(profile);

            var logDTO = new UserLoginDTO
            {
                MobileNumber = profile.MobileNumber,
                EmailId = profile.EmailId 
            };

            return logDTO;
        }

        #endregion

        #region GetUserProfilesById

        /// <summary>
        /// Get a profile DTO by profile ID.
        /// </summary>
        /// <param name="id">The ID of the profile.</param>
        /// <returns>The profile DTO with profile details.</returns>
        /// <exception cref="NullReferenceException">Thrown when the profile with id is null.</exception>

        public async Task<UserProfileDTO> GetUserProfilesById(int customerId)
        {
            UserProfile profile = await repo.GetUserProfileById(customerId);

            if (profile == null)
            {
                throw new NullReferenceException($"Profile with id {customerId} not found");
            }

            var profileDTO = new UserProfileDTO
            {
                Name = profile.Name,
                Dob = profile.Dob,
                Gender = profile.Gender,
                MaritalStatus = profile.MaritalStatus,
                CustomerId = profile.CustomerId
            };

            return profileDTO;
        }

        #endregion

        #region UpdateProfiles

        /// <summary>
        /// Update profile information of a profile.
        /// </summary>
        /// <param name="dto">The updated profile DTO.</param>
        /// <param name="id">The ID of the profile.</param>
        /// <returns>The updated profile DTO.</returns>
        /// <exception cref="NullReferenceException">Thrown when the profile is null.</exception>

        public async Task<UserProfileDTO> AddUserProfiles(UserProfileDTO profileDTO, int customerId)
        {
            UserProfile profile = await repo.GetUserProfileById(customerId);

            if (profile == null)
            {
                throw new NullReferenceException($"Profile with id {customerId} not found");
            }

            profile.Name = profileDTO.Name;
            profile.Dob = profileDTO.Dob;
            profile.Gender = profileDTO.Gender;
            profile.MaritalStatus = profileDTO.MaritalStatus;

            await repo.UpdateUserProfile(profile);

            var loginDTO = new UserProfileDTO
            {
                Name = profile.Name,
                Dob = profile.Dob,
                Gender = profile.Gender,
                MaritalStatus = profile.MaritalStatus,
            };

            return loginDTO;
        }

        #endregion

        #region ChangeUserPassword

        /// <summary>
        /// Change the password for a profile.
        /// </summary>
        /// <param name="id">The ID of the profile.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>A DTO containing the updated password information.</returns>
        /// <exception cref="NullReferenceException">Thrown when the profile is null.</exception>

        public async Task<HashPasswordDTO> ChangeUserPassword(int customerId, string oldPassword, string newPassword)
        {
            UserProfile profile = await repo.GetUserProfileById(customerId);

            if (profile == null)
            {
                throw new NullReferenceException($"Profile with id {customerId} not found");
            }

            if (oldPassword != null && profile.Password != null)
            {
                bool isOldPasswordCorrect = PasswordHasher.VerifyPassword(oldPassword, profile.Password);
                if (!isOldPasswordCorrect)
                {
                    throw new Exception("Old password is incorrect");
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(oldPassword), "Both oldPassword and profile.Password must not be null");
            }
            

            string newHashedPassword = PasswordHasher.HashPassword(newPassword);
            profile.Password = newHashedPassword;
            await repo.UpdateUserProfile(profile);

            return new HashPasswordDTO
            {
                CustomerId = profile.CustomerId,
                Password = newPassword,
                HashedPassword = newHashedPassword
            };
        }

        #endregion

        #region UpdateUserImage

        /// <summary>
        /// Update the profile image for a profile.
        /// </summary>
        /// <param name="id">The ID of the profile.</param>
        /// <param name="changeImg_DTO">DTO containing the new image data.</param>
        /// <returns>A DTO containing the updated image information.</returns>
        /// <exception cref="NullReferenceException">Thrown when the profile is null.</exception>

        public async Task<ChangeImageDTO> UpdateUserImage(int customerId, ChangeImageDTO changeImageDTO)
        {
            UserProfile? profile = await repo.GetUserProfileById(customerId);

            if (profile == null)
            {
                throw new NullReferenceException($"Profile with id {customerId} not found");
            }

            if (!string.IsNullOrEmpty(profile.Image))
            {
                string oldImagePath = Path.Combine(@"C:\Users\HP\Desktop\Figma\tripswel\public\Img", profile.Image);
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            string randomSequence = Guid.NewGuid().ToString("N");
            string newImageFilename = $"{randomSequence}.jpg";
            changeImageDTO.image = newImageFilename;
            string imagePath = Path.Combine(@"C:\Users\HP\Desktop\Figma\tripswel\public\Img", newImageFilename);

            if (changeImageDTO.file != null)
            {
                using (Stream stream = new FileStream(imagePath, FileMode.Create))
                {
                    await changeImageDTO.file.CopyToAsync(stream);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(changeImageDTO.file), "Image file is required");
            }

            profile.Image = newImageFilename;
            await repo.UpdateUserProfile(profile);

            return changeImageDTO;
        }

        #endregion

        #region UserRegister

        /// <summary>
        /// Register a new profile.
        /// </summary>
        /// <param name="register_DTO">DTO containing registration information.</param>
        /// <returns>A DTO containing the registered profile information.</returns>
        public async Task<UserRegisterDTO> UserRegister(UserRegisterDTO registerDTO)
        {
            if (registerDTO.Password != null)
            {
                var profile = new UserProfile
                {
                    CustomerId = registerDTO.CustomerId,
                    EmailId = registerDTO.EmailId,
                    Name = registerDTO.Name,
                    Password = PasswordHasher.HashPassword(registerDTO.Password),
                };
                await repo.AddUserProfile(profile);

                var loginDTO = new UserRegisterDTO
                {
                    CustomerId = profile.CustomerId,
                    EmailId = profile.EmailId,
                    Password = profile.Password,
                    Name = profile.Name,
                };
                return loginDTO;

            }
            else
            {
                throw new ArgumentNullException(nameof(registerDTO.Password), "Password is required");
            }

        }

        #endregion

        #region ViewUserImage

        /// <summary>
        /// View the profile image for a profile.
        /// </summary>
        /// <param name="id">The ID of the profile.</param>
        /// <returns>A DTO containing the profile image information.</returns>
        /// <exception cref="NullReferenceException">Thrown when the profile is null.</exception>

        public async Task<ChangeImageDTO> ViewUserImage(int customerId)
        {
            UserProfile profile = await repo.GetUserProfileById(customerId);

            if (profile == null)
            {
                throw new NullReferenceException($"Image with id {customerId} not found");
            }   

            var profileDTO = new ChangeImageDTO
            {
                CustomerId = profile.CustomerId,
                image = profile.Image
            };
                 
            return profileDTO;
        }

        #endregion

        #region UserLogin

        /// <summary>
        /// Perform user login.
        /// </summary>
        /// <param name="auth_DTO">DTO containing user authentication information.</param>
        /// <returns>JWT token if login is successful.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the authentication value is null.</exception>

        [ExcludeFromCodeCoverage]
        public async Task<string> UserLogin(AuthenticatorDTO authenticatorDTO)
        {
            if (_configuration == null)
                throw new NullReferenceException("Configuration object is null");
            if (authenticatorDTO != null && !string.IsNullOrEmpty(authenticatorDTO.EmailId) && !string.IsNullOrEmpty(authenticatorDTO.Password))
            {
                var user = await GetUser(authenticatorDTO.EmailId);

                if (user != null)
                {
                    if (user.Password != null && PasswordHasher.VerifyPassword(authenticatorDTO.Password, user.Password))
                    {
                        var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                };
                        if (!string.IsNullOrEmpty(user.CustomerId.ToString()))
                        {
                            claims.Add(new Claim("CustomerId", user.CustomerId.ToString()));
                        }
                        else
                        {
                            throw new Exception("Customer ID is required");
                        }


                        if (!string.IsNullOrEmpty(user.Name))
                        {
                            claims.Add(new Claim("Name", user.Name));
                        }
                        else
                        {
                            throw new Exception("User name is required");
                        }

                        if (_configuration != null)
                        {
                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                            var token = new JwtSecurityToken(
                                _configuration["Jwt:Issuer"],
                                _configuration["Jwt:Audience"],
                                claims,
                                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:TokenExpirationMinutes"])),
                                signingCredentials: signIn);

                            return new JwtSecurityTokenHandler().WriteToken(token);
                        }
                        else
                        {
                            throw new Exception("Configuration is null");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid credentials");
                    }
                }
                else
                {
                    throw new Exception("User not found"); // Handle the case where user is null
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(authenticatorDTO), "Authentication DTO cannot be null");
            }
        }

        #endregion


    }
}
