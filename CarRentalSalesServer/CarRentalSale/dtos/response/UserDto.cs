using Microsoft.Identity.Client;

namespace CarRentalSale.dtos.response
{

    public class UserDto
    {
        public required int UserID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }

        public required string AuthName { get; set; }

        public required string CountryName { get; set; }

        public required int CountryId { get; set; }

        public string? Adress { get; set; }

    }

    public class UserOrderDto
    {
        public required int UserID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string CountryName { get; set; }
        public string? Address { get; set; }

    }


    public class UserCarDtos
    {
        public required int UserID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }

        public required int CountryId { get; set; }
        public required string CountryName { get; set; }
        public string? Address { get; set; }

    }

        public class EditUserDto
    {
        public required int UserID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required int CountryId { get; set; }
        public string? Address { get; set; }

    }

    public class UserInfoMessage
    {

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

     public class UserMessagesEmail
    {

        public int UserId { get; set; }
        public string Email { get; set; }
         
         public string? FullName { get; set; }
        public string Message { get; set; }
        
        public DateTime? dateSent { get; set; }
        public bool IsFromOthers { get; set; }

        public bool IsRead{get;set;}
        
    }
}