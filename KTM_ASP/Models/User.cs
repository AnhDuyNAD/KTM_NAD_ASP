namespace KTM_ASP.Models
{
    public class User
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; } // admin hoặc customer
    }
}
