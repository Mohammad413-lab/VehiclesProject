using CarRentalSale.dtos.response;

namespace CarRentalSale.response
{
    public class CarDtos
    {
        public int CarID { get; set; }

        public string? Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }

        public string? Color { get; set; }

        public decimal CarPrice { get; set; }

        public decimal? CarRentalPrice { get; set; }

        public List<string> ImagesPath { get; set; } = [];
        public string? Description { get; set; }

        public decimal? PriceDiscount { get; set; }
        public decimal? RentalDiscount { get; set; }
    }

    public class CarResponseDtos
    {
        public int CarID { get; set; }

        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }

        public short Status { get; set; }
        public string? Color { get; set; }
        public decimal? CarPrice { get; set; }

        public decimal? CarRentalPrice { get; set; }



    }

       public class CarOrdered
    {
        public int CarID { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public decimal? CarPrice { get; set; }
        public decimal? CarRentalPrice { get; set; }
        public string? Color { get; set; }
        public short? Status { get; set; }
        public List<string> ImagesPath { get; set; } = [];
        public decimal? PriceDiscount { get; set; }
        public decimal? RentalDiscount { get; set; }
        public CarOrderResponse CarOrder { get; set; }

    }

       public class CarOrderResponse
    {
        private decimal _price;
   
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

       public class CarAllInfoDtos
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
        public required UserCarDtos User{get;set;}
   
    }


    public class CarModel{
        public int ModelId{get;set;}
        public required string  ModelName{get;set;}
    }

      public class CarMake{
        public int MakeId { get; set; }
        public required string MakeName { get; set; }
    }


  


}