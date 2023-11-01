using Microsoft.EntityFrameworkCore;
using prj.Context;
using prj.Model;
using prj.Repository.Interface;

namespace prj.Repository.Implementation
{
    public class UserProfileRepository : IUserProfile
    {
        private readonly ProfileContext _profileContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileRepository"/> class.
        /// </summary>
        /// <param name="context">The ProfileContext to use.</param>

        public UserProfileRepository(ProfileContext context)
        {
            _profileContext = context;
        }

        #region GetProfile

        /// <summary>
        /// Retrieve all profiles from the database.
        /// </summary>
        /// <returns>A collection of profiles.</returns>
        /// <exception cref="NullReferenceException">Thrown when no profiles are found.</exception>

        public async Task<ICollection<UserProfile>> GetUserProfile()
        {
            var profile = await _profileContext.profiles.ToListAsync();
            if (profile != null)
            {
                return profile;
            }
            throw new NullReferenceException("No profiles found.");
        }

        #endregion

        #region AddUserProfile

        /// <summary>
        /// Add a new profile to the database.
        /// </summary>
        /// <param name="profiles">The profile to add.</param>
        /// <returns>The added profile.</returns>
        /// <exception cref="NullReferenceException">Thrown when the profile is null.</exception>

        public async Task<UserProfile> AddUserProfile(UserProfile profile)
        {
            if (profile != null)
            {
                _profileContext.profiles.Add(profile);
                await _profileContext.SaveChangesAsync();
                return profile;
            }
            throw new NullReferenceException("Profile is null.");
        }

        #endregion

        #region UpdateUserProfile

        /// <summary>
        /// Update an existing profile in the database.
        /// </summary>
        /// <param name="profile">The updated profile data.</param>
        /// <returns>The updated profile.</returns>
        /// <exception cref="NullReferenceException">Thrown when the profile to update is not found.</exception>

        public async Task<UserProfile> UpdateUserProfile(UserProfile profile)
        {
            var existingProfile = await GetUserProfileById(profile.CustomerId);
            if (existingProfile != null)
            {
                existingProfile.MobileNumber = profile.MobileNumber;
                await _profileContext.SaveChangesAsync();
                return profile;
            }
            throw new NullReferenceException($"Profile with CustomerId {profile.CustomerId} not found.");
        }

        #endregion

        #region DeleteUserProfile

        /// <summary>
        /// Delete a profile by CustomerId from the database.
        /// </summary>
        /// <param name="CustomerId">The CustomerId of the profile to delete.</param>
        /// <returns>The deleted profile.</returns>
        /// <exception cref="NullReferenceException">Thrown when the profile to delete is null.</exception>

        public async Task<UserProfile> DeleteUserProfile(int customerId)
        {
            UserProfile? profile = await _profileContext.profiles.FirstOrDefaultAsync(x => x.CustomerId == customerId);
            if (profile != null)
            {
                _profileContext.profiles.Remove(profile);
                await _profileContext.SaveChangesAsync();
            }
            throw new NullReferenceException($"Profile with CustomerId {customerId} not found.");
        }

        #endregion

        #region GetUserProfileById

        /// <summary>
        /// Retrieve a profile by CustomerId from the database.
        /// </summary>
        /// <param name="customer_id">The CustomerId of the profile to retrieve.</param>
        /// <returns>The retrieved profile.</returns>
        /// <exception cref="NullReferenceException">Thrown when the profile with the specified CustomerId is not found.</exception>

        public async Task<UserProfile> GetUserProfileById(int customerId)
        {
            var profile = await _profileContext.profiles.FindAsync(customerId);
            if (profile != null)
            {
                return profile;
            }
            throw new NullReferenceException($"Profile with CustomerId {customerId} not found.");
        }

        #endregion

        #region GetUser

        /// <summary>
        /// Retrieve a profile by email ID from the database.
        /// </summary>
        /// <param name="email_id">The email ID of the profile to retrieve.</param>
        /// <returns>The retrieved profile.</returns>
        /// <exception cref="NullReferenceException">Thrown when the profile with the specified email ID is not found.</exception>

        public async Task<UserProfile> GetUser(string emailId)
        {
            UserProfile? profile = await _profileContext.profiles.FirstOrDefaultAsync(u => u.EmailId == emailId);
            if (profile != null)
            {
                return profile;
            }
            throw new NullReferenceException($"Profile with email ID {emailId} not found.");
        }

        #endregion
    }
}
