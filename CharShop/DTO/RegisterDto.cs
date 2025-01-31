namespace CharShop.DTO
{
    public class RegisterDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int Age { get; set; }
        public string Address { get; set; } = null!;

        public string? CreditCard { get; set; }
    }
}
