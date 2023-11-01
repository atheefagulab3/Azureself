using prj.Model;


namespace prj.Repository.Interface
{
    public interface IUserProfile
    {
        public Task<ICollection<UserProfile>> GetUserProfile();

        public  Task<UserProfile> AddUserProfile(UserProfile profile) ;   
       public Task<UserProfile> UpdateUserProfile(UserProfile profile);
        public Task<UserProfile> DeleteUserProfile(int customerId);

        public Task<UserProfile> GetUserProfileById(int customerId);

        public Task<UserProfile> GetUser(string emailId);
    }
}
