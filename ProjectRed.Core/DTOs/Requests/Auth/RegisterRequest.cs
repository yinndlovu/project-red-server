namespace ProjectRed.Core.DTOs.Requests.Auth
{
    public class RegisterRequest
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int BirthYear { get; set; }
    }
}
