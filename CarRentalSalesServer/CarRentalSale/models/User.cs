
namespace CarRentalSale.models
{

    public class User
    {
        public required int CustomerId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required Guid Salt { get; set; }
        public required string Password { get; set; }
        public required short AuthNumber { get; set; }
        public required string AuthName{get;set;}

        public required int CountryId { get; set; }
        public bool isVerified { get; set; }
        public string? Adress { get; set; }

    }
}