using Microsoft.AspNetCore.Mvc;
using prj.Model.DTO;
using prj.Model;

namespace prj.Service.Interface
{
    public interface IUserProfileService
    {
        public Task<ICollection<UserProfile>> GetUserProfile();

        public Task<UserProfile> AddUserProfile( UserProfile profile);
        public Task<UserProfile> UpdateUserProfile(UserProfile profile);
        public Task<UserProfile> DeleteUserProfile(int customerId);

        public Task<UserProfile> GetUserProfileById(int customerId);

        public Task<UserProfile> GetUser(string emailId);

        public Task<UserLoginDTO> AddUserLogin(int customerId, UserLoginDTO loginDTO);

        public Task<UserProfileDTO> GetUserProfilesById(int customerId);

        public Task<UserProfileDTO> AddUserProfiles(UserProfileDTO profileDTO, int customerId);

        public Task<HashPasswordDTO> ChangeUserPassword(int customerId, string oldPassword, string newPassword);
        public Task<ChangeImageDTO> UpdateUserImage(int customerId, ChangeImageDTO changeImageDTO);
        public Task<ChangeImageDTO> ViewUserImage(int customerId);
        public Task<UserRegisterDTO> UserRegister(UserRegisterDTO registerDTO);

        public Task<string> UserLogin(AuthenticatorDTO authenticatorDTO);

        
    }
}
