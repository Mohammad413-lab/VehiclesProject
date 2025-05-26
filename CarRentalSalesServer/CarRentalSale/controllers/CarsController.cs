
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;
using global::CarRentalSale.dtos.response;
using global::CarRentalSale.interfacee;
using global::CarRentalSale.models;
using CarRentalSale.request;
using CarRentalSale.datarepositories;
using CarRentalSale.services;
using CarRentalSale.request.dtos;
using CarRentalSale.enums;
using CarRentalSale.response;
using System.Runtime.CompilerServices;
using CarRentalSale.helper;
using CarRentalSale.dtos;
namespace CarRentalSale.Controllers
{


    namespace CarRentalSale.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class CarsContoller(ICarsServices carServices) : ControllerBase
        {
            private ICarsServices _carServices = carServices;



            [HttpGet("GetCarRental")]
            public IActionResult GetCarRental(int carId)
            {

                IEnumerable<Rental> carResponseDtos = _carServices.GetCarRental(carId);
                if (carResponseDtos == null || !carResponseDtos.Any())
                {
                    return NotFound("No rental for this vehicle ");
                }
                return Ok(carResponseDtos);


            }


            [HttpGet("GetAllUserCars")]
            public IActionResult GetAllUserCars(int userId)
            {

                IEnumerable<Car> allCars = _carServices.GetAllUserCars(userId);
                if (allCars == null || !allCars.Any())
                {
                    return Ok(new { message = "There is no vehicle" });
                }

                return Ok(allCars);


            }


            [HttpGet("GetAllCountryCars")]
            public IActionResult GetAllCountryCars(int countryId, int pageNumber)
            {

                IEnumerable<CarAllInfoDtos> allCountryCars = _carServices.GetAllCountryCars(countryId, pageNumber);
                if (allCountryCars == null || !allCountryCars.Any())
                {
                    return Ok(new { message = "There is no vehicle" });
                }

                return Ok(allCountryCars);


            }


            [HttpGet("GetCarsByStatus")]
            public IActionResult GetCarsByStatus(int carStatus)
            {

                IEnumerable<Car> allCars = _carServices.GettCarsBystatus(carStatus);
                return Ok(allCars);


            }

            [HttpPost("ChangeCarStatus")]
            public IActionResult ChangeCarStatus(int carId, int carStatus)
            {

                if (_carServices.FindCarById(carId) == null)
                {
                    return NotFound("There is no vehicle with this id :)");
                }
                if (carStatus > (int)CarStatus.Maintenance || carStatus < (int)CarStatus.Pending)
                {
                    return BadRequest("There is no status in this number");
                }

                _carServices.ChangeCarStatus(carId, carStatus);
                return Ok("status changed :)");

            }

            [HttpPost("AddCarModel")]
            public IActionResult AddCarModel(AddCarModelDtos carModel)
            {
                try
                {
                    int id = _carServices.AddCarModel(carModel);
                    return Ok(new { message = "Vehicle model added successfully", status = true, modelId = id });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Server error: " + ex.Message);
                }
            }

            [HttpPost("AddCarMake")]
            public IActionResult AddCarMake(int userId, string carMake)
            {
                try
                {
                    int id = _carServices.AddCarMake(carMake, userId);
                    return Ok(new { message = "Vehicle make added successfully", status = true, makeId = id });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Server error: " + ex.Message);
                }
            }

            [HttpPost("AddCar")]
            public async Task<IActionResult> AddCar(AddCarDtos car)
            {
                try
                {
                    int id = await _carServices.AddCar(car);
                    return Ok(new { message = "Vehicle added successfully", status = true, carId = id });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Server error: " + ex.Message);
                }
            }

            [HttpDelete("RemoveModel")]
            public IActionResult RemoveModel(int modelId)
            {
                try
                {
                    _carServices.RemoveModel(modelId);
                    return Ok(new { message = "Model removed successfully", status = true });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Server error: " + ex.Message);
                }
            }

            [HttpDelete("RemoveMake")]
            public IActionResult RemoveMake(int makeId)
            {
                try
                {
                    _carServices.RemoveMake(makeId);
                    return Ok(new { message = "Make removed successfully", status = true });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "Server error: " + ex.Message, status = false });
                }
            }

            [HttpPost("UploadImageCarByCarId")]
            public async Task<IActionResult> UploadImageCarByCarId([FromForm] IFormFile[] imageFile, int carId)
            {
                try
                {
                    int state = await _carServices.UploadCarImagesByCarId(imageFile, carId);
                    switch (state)
                    {
                        case (int)UploadImageCar.CarNotFound: return NotFound("Car not found");
                        case (int)UploadImageCar.ImageFileIsNull: return NotFound("Your file is null");
                        case (int)UploadImageCar.ImageUploaded: return Ok(new { message = "image Upload", status = true });
                        default: return StatusCode(500, "Server error happend please try again");
                    }

                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Server error: " + ex.Message);
                }
            }


            [HttpDelete("RemoveCar")]
            public IActionResult RemoveCar(int carId)
            {
                try
                {
                    _carServices.RemoveCar(carId);
                    return Ok(new { message = "Vehicle removed successfully", status = true });

                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Server error: " + ex.Message);
                }
            }

            [HttpGet("GetUserCarCart")]
            public IActionResult GetUserCarCart(int userId)
            {

                IEnumerable<CarAllInfoDtos> allCars = _carServices.GetUserCarCart(userId);
                return Ok(allCars);


            }

            [HttpDelete("RemoveCart")]
            public IActionResult RemoveCart(int userId, int carId)
            {
                try
                {
                    _carServices.RemoveCart(userId, carId);
                    return Ok(new { message = "Removed from list successfully", status = true });

                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Server error: " + ex.Message);
                }
            }



            [HttpGet("GetUserModel")]
            public IActionResult GetUserModel(int userId)
            {

                IEnumerable<CarModel> carModels = _carServices.GetUserModel(userId);
                return Ok(carModels);


            }


            [HttpGet("GetUserMake")]
            public IActionResult GetUserMake(int userId)
            {
                IEnumerable<CarMake> carMake = _carServices.GetUserMake(userId);
                return Ok(carMake);
            }

            [HttpGet("GetCarImages")]
            public IActionResult GetCarImages(int carId)
            {
                IEnumerable<CarImage> carMake = _carServices.GetCarImages(carId);
                return Ok(carMake);
            }

            [HttpDelete("RemoveCarImage")]
            public IActionResult RemoveCarImage(int imageId, string imagePath)
            {
                try
                {
                    _carServices.RemoveCarImage(imageId, imagePath);
                    return Ok(new { message = "Image removed successfully", status = true });

                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Server error: " + ex.Message);
                }
            }

            [HttpPatch("EditCar")]
            public IActionResult EditCar([FromBody] EditCarDtos car)
            {
                try
                {
                    _carServices.EditCar(car);
                    return Ok(new { message = "Vehicle edited successfully", status = true });

                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Server error: " + ex.Message);
                }
            }

            [HttpGet("GetCarSortedByYear")]
            public IActionResult GetCarSortedByYear(int countryId, int pageNumber)
            {
                IEnumerable<CarAllInfoDtos> cars = _carServices.GetCarsSortedByYear(countryId, pageNumber);
                return Ok(cars);
            }

            [HttpGet("GetCarSortedByOldest")]
            public IActionResult GetCarSortedByOldest(int countryId, int pageNumber)
            {
                IEnumerable<CarAllInfoDtos> cars = _carServices.GetCarsSortedByOldest(countryId, pageNumber);
                if (cars == null || !cars.Any())
                {
                    return Ok(new { message = "No vehicle found" });
                }
                return Ok(cars);
            }

            [HttpGet("GetCarsForSale")]
            public IActionResult GetCarsForSale(int countryId, int pageNumber)
            {

                IEnumerable<CarAllInfoDtos> cars = _carServices.GetCarsForSale(countryId, pageNumber);
                if (cars == null || !cars.Any())
                {
                    return Ok(new { message = "No vehicle found" });
                }
                return Ok(cars);
            }
            [HttpGet("GetCarsForRental")]
            public IActionResult GetCarsForRental(int countryId, int pageNumber)
            {
                IEnumerable<CarAllInfoDtos> cars = _carServices.GetCarForRental(countryId, pageNumber);
                if (cars == null || !cars.Any())
                {
                    return Ok(new { message = "No vehicle found" });
                }
                return Ok(cars);
            }

            [HttpGet("GetCarsContain")]
            public IActionResult GetCarsContain(int countryId, string contain, int pageNumber)
            {

                IEnumerable<CarAllInfoDtos> allCountryCars = _carServices.GetCarsContaine(countryId, contain, pageNumber);
                if (allCountryCars == null || !allCountryCars.Any())
                {
                    return Ok(new { message = "No vehicle found" });
                }

                return Ok(allCountryCars);


            }


























        }
    }

}
