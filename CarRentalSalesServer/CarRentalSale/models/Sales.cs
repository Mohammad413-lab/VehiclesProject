using CarRentalSale.response;

namespace CarRentalSale.models
{
    public class Sales
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public required Payment Payment { get; set; }
        public required CarDtos Car { get; set; }



    }
}