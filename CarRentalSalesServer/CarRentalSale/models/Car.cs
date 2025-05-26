namespace CarRentalSale.models
{
    public class Car
    {
        public int CarID { get; set; }

        public string? Make { get; set; }
        public string? Model { get; set; }
        public short? Year { get; set; }

        public decimal? CarPrice { get; set; }
        public decimal? CarRentalPrice { get; set; }

        public string? Color { get; set; }

        public short? Status { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public List<string> ImagesPath { get; set; } = [];

        public string? Description { get; set; }
        public decimal? PriceDiscount { get; set; }
        public decimal? RentalDiscount { get; set; }

   
    }


}