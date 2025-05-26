using CarRentalSale.dtos.response;
using CarRentalSale.response;

namespace CarRentalSale.models
{

    public class SaleOrder
    {
        public int SaleOrderId { get; set; }

        public required UserDto User { get; set; }
        public required CarDtos Car { get; set; }
        public DateTime OrderDate { get; set; }

        public required decimal TotalAmount { get; set; }
        public string? Note { get; set; }
        public required byte Status { get; set; }
        public required DateTime UpdatedAT { get; set; }
    }
}