using Microsoft.EntityFrameworkCore;
using prj.Model;

namespace prj.Context
{
    public class ProfileContext : DbContext
    {
        public DbSet<UserProfile> profiles { get; set; }

        public DbSet<AdditionalTraveller> additional_Travellers { get; set; }

        public ProfileContext(DbContextOptions<ProfileContext> options) : base(options) { }
    }
}
