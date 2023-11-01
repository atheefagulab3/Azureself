using System.ComponentModel.DataAnnotations;

namespace prj.Model
{
    public class AdditionalTraveller
    {

        [Key]
        public int AdditionalId { get; set; }


        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string? AdditionalName { get; set; }

        public UserProfile? Profile { get; set; }
    }
}
