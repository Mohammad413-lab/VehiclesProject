
using CarRentalSale.dtos.response;
using CarRentalSale.response;

namespace CarRentalSale.models
{

    public class Rental
    {
        public required int RentalId { get; set; }

        public CarDtos? Car { get; set; }

        public UserDto? User { get; set; }

        public DateTime RentalStartDate{get;set;}
        public DateTime RentalEndDate { get; set; }
        
        public bool IsPayed{get; set;}
        public decimal RentalPrice { get; set; }

        public int? PaymentId{get;set;}
      

    }
}