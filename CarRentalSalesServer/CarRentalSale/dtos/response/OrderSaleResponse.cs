using CarRentalSale.response;

namespace CarRentalSale.dtos.response
{
    public class OrderSaleResponse
    {
        public required int SaleOrderId { get; set; }
        public required CarAllInfoDtos Car { get; set; }

        public required DateTime OrderDate { get; set; }
        public string? Note { get; set; }
        
        public decimal TotalAmount { get; set; }
        public byte Status { get; set; }
        public DateTime? UpdatedAt { get; set; }

    } 
}