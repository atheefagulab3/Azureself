using System.ComponentModel.DataAnnotations.Schema;

namespace prj.Model.DTO
{
    public class ChangeImageDTO
    {
        public int CustomerId { get; set; }
        public string? image { get; set; }
        [NotMapped]
        public IFormFile? file { get; set; }
    }
}
