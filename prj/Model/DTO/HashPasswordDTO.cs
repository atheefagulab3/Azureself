namespace prj.Model.DTO
{
    public class HashPasswordDTO
    {
        public int CustomerId { get; set; }
        public string? Password { get; set; }
        public string? HashedPassword { get; set; }
    }
}
