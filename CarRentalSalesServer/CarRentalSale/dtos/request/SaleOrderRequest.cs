namespace CarRentalSale.dtos
{
    public class CreateSaleOrder
    {

        public required int CarId { get; set; }
        public required int UserId { get; set; }
        public decimal TotalAmount { get; set; }
         public string? Notes { get; set; }


    }
}