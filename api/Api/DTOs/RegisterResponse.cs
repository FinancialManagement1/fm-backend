namespace Api.DTOs
{
    public class RegisterResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PreferredCurrency { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
