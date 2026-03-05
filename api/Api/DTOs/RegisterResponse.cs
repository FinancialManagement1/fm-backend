namespace Api.DTOs
{
    public class RegisterResponse
    {
        public string Token { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
