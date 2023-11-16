namespace EcosystemApp.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string HashPassword { get; set; }
        public string Role { get; set; }
        public DateTime RegDate { get; set; }
    }
}
