namespace CarRentalSale.dtos
{

    public class SignUpDtos
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }

        public required int Countryid{get;set;}
        public string? Address { get; set; }

    }
}