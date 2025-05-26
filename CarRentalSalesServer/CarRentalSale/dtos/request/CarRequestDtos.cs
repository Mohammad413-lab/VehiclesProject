using CarRentalSale.helper;

namespace CarRentalSale.request
{
    public class CarRequestDtos
    {
        public int CarID { get; set; }

        public string? Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }

        public string? Color { get; set; }



        public decimal CarPrice { get; set; }

        public decimal CarRentalPrice { get; set; }

        public string? Description { get; set; }
        public decimal? PriceDiscount { get; set; }
        public decimal? RentalDiscount { get; set; }
    }
    public class AddCarDtos
    {
        public required int ModelId { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public decimal? CarPrice { get; set; }

        public decimal? CarRentalPrice { get; set; }
        public string? Description { get; set; }

        public int UserId { get; set; }

    }
    public class EditCarDtos
    {
        public int CarId { get; set; }
        public required int ModelId { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public decimal? CarPrice { get; set; }
        public decimal? CarRentalPrice { get; set; }
        public string? Description { get; set; }


    }

    public class AddCarModelDtos
    {
        public string ModelName { get; set; }
        public int MakeId { get; set; }
    }

    public class CarOrder
    {
        private decimal _price;
        public int CarId { get; set; }
        public int NumberOfCars { get; set; }

        public bool IsRental { get; set; }

        public decimal Price
        {
            get => _price;
            set
            {
                if (NumberOfCars > 0)
                    _price = value * NumberOfCars;
                else
                    _price = value;
            }
        }

        public string? Notes { get; set; }
    }


}