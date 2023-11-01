using System.ComponentModel.DataAnnotations;

namespace prj.Model
{
    public class UserProfile
    {
        [Key]
        public int CustomerId { get; set; }


        public string? Name { get; set; }


        public DateTime Dob { get; set; }


        public string? Gender { get; set; }


        public string? MaritalStatus { get; set; }


        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid mobile number")]
        public long MobileNumber { get; set; }


        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? EmailId { get; set; }


        public string? Password { get; set; }

        public string? Image { get; set; }
    }
}
