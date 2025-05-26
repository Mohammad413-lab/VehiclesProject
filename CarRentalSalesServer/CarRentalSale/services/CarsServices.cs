using System.Threading.Tasks;
using CarRentalSale.datarepositories;
using CarRentalSale.dtos;
using CarRentalSale.enums;
using CarRentalSale.interfacee;
using CarRentalSale.models;
using CarRentalSale.request;
using CarRentalSale.response;

namespace CarRentalSale.services
{
    public class CarsServices : ICarsServices
    {
        public async Task<int> AddCar(AddCarDtos addCar)
        {
            try
            {
                 return await CarsRepository.AddCar(addCar);
            }
            catch (Exception ex) { throw new Exception($"Error ->> {ex}"); }

        }

        public int AddCarMake(string carMake,int userId)
        {
           return CarsRepository.AddCarMake(carMake,userId);
        }



        public int AddCarModel(AddCarModelDtos carModel)
        {
          return  CarsRepository.AddCarModel(carModel);
        }

  

        public void ChangeCarStatus(int carId, int status)
        {
            if (status > (int)CarStatus.Maintenance || status < (int)CarStatus.Pending)
            {
                throw new IndexOutOfRangeException("there is no status in this input");
            }

            CarsRepository.ChangeCarStatus(carId, status);
        }

        public void EditCar(EditCarDtos car)
        {
           CarsRepository.EditCar(car);
        }

        public Car? FindCarById(int carId)
        {
            return CarsRepository.FindCarById(carId);
        }

        public IEnumerable<CarAllInfoDtos> GetAllCountryCars(int countryId,int pageNumber)
        {
            return CarsRepository.GetAllCountryCars(countryId,pageNumber);
        }

        public IEnumerable<Car> GetAllUserCars(int userId)
        {
            return CarsRepository.GetAllUserCars(userId);
        }

        public IEnumerable<CarAllInfoDtos> GetCarForRental(int countryId, int pageNumber)
        {
           return CarsRepository.GetCarForRental(countryId,pageNumber);
        }

        public IEnumerable<CarImage> GetCarImages(int carId)
        {
            return CarsRepository.GetCarImages(carId);
        }

        public IEnumerable<Rental> GetCarRental(int carId)
        {
            return CarsRepository.GetCarRentals(carId);
        }

        public IEnumerable<CarAllInfoDtos> GetCarsContaine(int countryId, string contain, int pageNumber)
        {
            return CarsRepository.GetCarsContaine(countryId, contain, pageNumber);
        }

        public IEnumerable<CarAllInfoDtos> GetCarsForSale(int countryId, int pageNumber)
        {
               return CarsRepository.GetCarsForSale(countryId,pageNumber);
        }

        public IEnumerable<CarAllInfoDtos> GetCarsSortedByOldest(int countryId, int pageNumber)
        {
            return CarsRepository.GetCarsSortedByOldest(countryId,pageNumber);
        }

        public IEnumerable<CarAllInfoDtos> GetCarsSortedByYear(int countryId, int pageNumber)
        {
            return CarsRepository.GetCarsSortedByYear(countryId,pageNumber);
        }

        public IEnumerable<Car> GettCarsBystatus(int carStatus)
        {
            return CarsRepository.GetCarsByStatus(carStatus);
        }

        public IEnumerable<CarAllInfoDtos> GetUserCarCart(int userId)
        {
            return CarsRepository.GetUserCarCart(userId);
        }

        public IEnumerable<CarMake> GetUserMake(int userId)
        {
            return CarsRepository.GetUserMake(userId);
        }

        public IEnumerable<CarModel> GetUserModel(int userId)
        {
           return CarsRepository.GetUserModel(userId);
        }

        public void RemoveCar(int carId)
        { 
            CarsRepository.OnCarRemoved+=carId=>{
                Console.WriteLine($"CAR Removed -> {carId}" );
            };

            
            CarsRepository.RemoveCar(carId);
        }

        public void RemoveCarImage(int imageId, string imagePath)
        {
           CarsRepository.RemoveCarImage(imageId,imagePath);
        }

        public void RemoveCart(int userId, int carId)
        {
            CarsRepository.RemoveCart(userId,carId);
        }

        public void RemoveMake(int makeId)
        {
            try
            {
                CarsRepository.RemoveMake(makeId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error--> {ex}");
            }
        }

        public void RemoveModel(int modelId)
        {
            try
            {
                CarsRepository.RemoveModel(modelId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error--> {ex}");
            }
        }

        public Task<int> UploadCarImagesByCarId(IFormFile[] file, int carId)
        {
           return CarsRepository.UploadCarImagesByCarId(file,carId);
        }

        

        
    }
}