using CarRentalSale.Controllers.CarRentalSale.Controllers;
using CarRentalSale.dtos;
using CarRentalSale.models;
using CarRentalSale.request;
using CarRentalSale.response;

namespace CarRentalSale.interfacee
{
    public interface ICarsServices
    {
        public IEnumerable<Rental> GetCarRental(int carId);
        public IEnumerable<Car> GetAllUserCars(int userId);
        public IEnumerable<CarAllInfoDtos> GetAllCountryCars(int countryId,int pageNumber);
        public IEnumerable<Car> GettCarsBystatus(int carStatus);

        public void ChangeCarStatus(int carId, int status);

        public Car? FindCarById(int carId);

        public Task<int> AddCar(AddCarDtos addCar);

        public int AddCarMake(string carMake, int userId);
        public int AddCarModel(AddCarModelDtos carModel);

        public void RemoveMake(int makeId);
        public void RemoveModel(int modelId);

        public Task<int> UploadCarImagesByCarId(IFormFile[] file, int carId);

        public void RemoveCar(int carId);

        public IEnumerable<CarAllInfoDtos> GetUserCarCart(int userId);


        public void RemoveCart(int userId, int carId);


        public IEnumerable<CarModel> GetUserModel(int userId);

        public IEnumerable<CarMake> GetUserMake(int userId);
        public IEnumerable<CarImage> GetCarImages(int carId);

        public void RemoveCarImage(int imageId, string imagePath);
        public void EditCar(EditCarDtos car);

        public IEnumerable<CarAllInfoDtos> GetCarsSortedByYear(int countryId, int pageNumber);

        public IEnumerable<CarAllInfoDtos> GetCarsSortedByOldest(int countryId, int pageNumber);

        public IEnumerable<CarAllInfoDtos> GetCarsForSale(int countryId, int pageNumber);
        public IEnumerable<CarAllInfoDtos> GetCarForRental(int countryId, int pageNumber);

        public IEnumerable<CarAllInfoDtos> GetCarsContaine(int countryId, string contain, int pageNumber);







    }
}