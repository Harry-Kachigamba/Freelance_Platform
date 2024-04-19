namespace Freelance_Platform_Final.Models
{
    public abstract class Account
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string District { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
