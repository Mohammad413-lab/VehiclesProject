
using System.Data;
using System.Text;
using System.Threading.Tasks;
using CarRentalSale.dtos;
using CarRentalSale.dtos.response;
using CarRentalSale.enums;
using CarRentalSale.helper;
using CarRentalSale.models;
using CarRentalSale.request;
using CarRentalSale.response;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace CarRentalSale.datarepositories
{

    public static class CarsRepository
    {
        public delegate void CarRemovedHandler(int carId);
        public static event CarRemovedHandler? OnCarRemoved;
        public static IEnumerable<Rental> GetCarRentals(int carId)
        {
            List<Rental> CarRental = new List<Rental>();
            using (SqlConnection connection = DatabaseAccess.GetConnection())
            {
                connection.Open();
                string query = @" select r.RentalID,r.RentalStartDate,r.RentalEndDate,r.RentalPrice,r.Ispayed,
 u.UserID,u.FirstName,u.LastName,co.CountryName,u.CountryId,u.Email,u.PhoneNumber,u.Address,a.AuthName
 from Rentals r join Users u ON r.UserID=u.UserID
 INNER JOIN Auth a On a.AuthId=u.AuthId
 INNER JOIN Countries co On u.CountryId=co.CountryId
 where r.CarID=@carId;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@carId", carId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CarRental.Add(new Rental
                        {
                            RentalId = (int)reader["RentalId"],
                            RentalStartDate = (DateTime)reader["RentalStartDate"],
                            RentalEndDate = (DateTime)reader["RentalEndDate"],
                            RentalPrice = (decimal)reader["RentalPrice"],
                            User = new UserDto
                            {
                                UserID = (int)reader["UserID"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                Email = (string)reader["Email"],
                                PhoneNumber = (string)reader["PhoneNumber"],
                                AuthName = (string)reader["AuthName"],
                                Adress = (string)reader["Adress"],
                                CountryName = (string)reader["CountryName"],
                                CountryId = (int)reader["CountryId"]

                            }


                        });

                    }
                }
            }
            return CarRental;
        }

        public static IEnumerable<Car> GetAllUserCars(int userId)
        {
            Dictionary<int, List<string>> allCarImages = GetAllCarImages();
            List<Car> allCars = [];
            using SqlConnection connection = DatabaseAccess.GetConnection();
            string query = @"SELECT 
    c.CarID, 
    ma.MakeName AS Make, 
    m.ModelName AS Model, 
    c.Year, 
    c.Price, 
    c.RentalPrice, 
    c.Color, 
    c.Status, 
    c.PurchaseDate,
	c.PriceDiscount,
	c.RentalDiscount,
    c.Description

FROM Cars c
JOIN Models m ON c.ModelId = m.ModelId
JOIN Makes ma ON m.MakeId = ma.MakeId where c.UserId =@userId;";
            connection.Open();
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", userId);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Car car = new Car
                {
                    CarID = (int)reader["CarID"],
                    Make = reader["Make"] == DBNull.Value ? null : (string)reader["Make"],
                    Model = reader["Model"] == DBNull.Value ? null : (string)reader["Model"],
                    CarPrice = reader["Price"] == DBNull.Value ? null : (decimal)reader["Price"],
                    CarRentalPrice = reader["RentalPrice"] == DBNull.Value ? null : (decimal)reader["RentalPrice"],
                    Color = reader["Color"] == DBNull.Value ? null : (string)reader["Color"],
                    Status = reader["Status"] == DBNull.Value ? null : (short)reader["Status"],
                    Description = reader["Description"] == DBNull.Value ? null : (string)reader["Description"],
                    Year = reader["Year"] == DBNull.Value ? null : (short)reader["Year"],
                    PurchaseDate = reader["PurchaseDate"] == DBNull.Value ? null : (DateTime)reader["PurchaseDate"],
                    PriceDiscount = reader["PriceDiscount"] == DBNull.Value ? null : (decimal)reader["PriceDiscount"],
                    RentalDiscount = reader["RentalDiscount"] == DBNull.Value ? null : (decimal)reader["RentalDiscount"],

                };
                if (allCarImages.TryGetValue(car.CarID, out var images))
                {
                    car.ImagesPath = images;
                }

                allCars.Add(car);



            }

            return allCars.AsEnumerable();


        }



        public static IEnumerable<CarAllInfoDtos> GetAllCountryCars(int countryId, int pageNumber)
        {
            Dictionary<int, List<string>> allCarImages = GetAllCarImages();
            List<CarAllInfoDtos> allCars = [];
            using SqlConnection connection = DatabaseAccess.GetConnection();
            string query = @"SELECT 
    c.CarID, 
    ma.MakeName AS Make, 
    m.ModelName AS Model, 
    c.Year, 
    c.Price, 
    c.RentalPrice, 
    c.Color, 
    c.Status, 
    c.PurchaseDate,
    c.PriceDiscount,
    c.RentalDiscount,
    c.Description,
    u.UserID,
    u.FirstName,
    u.LastName,
    u.Email,
    u.PhoneNumber,
    u.Address,
    u.CountryId,
    co.CountryName
FROM Cars c
JOIN Users u ON c.UserId = u.UserId
JOIN Countries co ON u.CountryId = co.CountryId
JOIN Models m ON c.ModelId = m.ModelId
JOIN Makes ma ON m.MakeId = ma.MakeId
WHERE u.CountryId = @countryId
ORDER BY c.CarID
OFFSET (@pageNumber - 1) * 50 ROWS 
FETCH NEXT 50 ROWS ONLY;";
            connection.Open();
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@countryId", countryId);
            command.Parameters.AddWithValue("@pageNumber", pageNumber);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CarAllInfoDtos car = new CarAllInfoDtos
                {
                    CarID = (int)reader["CarID"],
                    Make = reader["Make"] == DBNull.Value ? null : (string)reader["Make"],
                    Model = reader["Model"] == DBNull.Value ? null : (string)reader["Model"],
                    CarPrice = reader["Price"] == DBNull.Value ? null : (decimal)reader["Price"],
                    CarRentalPrice = reader["RentalPrice"] == DBNull.Value ? null : (decimal)reader["RentalPrice"],
                    Color = reader["Color"] == DBNull.Value ? null : (string)reader["Color"],
                    Status = reader["Status"] == DBNull.Value ? null : (short)reader["Status"],
                    Description = reader["Description"] == DBNull.Value ? null : (string)reader["Description"],
                    Year = reader["Year"] == DBNull.Value ? null : (short)reader["Year"],
                    PurchaseDate = reader["PurchaseDate"] == DBNull.Value ? null : (DateTime)reader["PurchaseDate"],
                    PriceDiscount = reader["PriceDiscount"] == DBNull.Value ? null : (decimal)reader["PriceDiscount"],
                    RentalDiscount = reader["RentalDiscount"] == DBNull.Value ? null : (decimal)reader["RentalDiscount"],
                    User = new UserCarDtos
                    {
                        UserID = (int)reader["UserId"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Email = (string)reader["Email"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        Address = (string)reader["Address"],
                        CountryId = (int)reader["CountryId"],
                        CountryName = (string)reader["CountryName"]


                    }

                };
                if (allCarImages.TryGetValue(car.CarID, out var images))
                {
                    car.ImagesPath = images;
                }

                allCars.Add(car);



            }

            return allCars.AsEnumerable();


        }

        public static IEnumerable<Car> GetCarsByStatus(int carStatus)
        {
            List<Car> allCars = [];
            using SqlConnection connection = DatabaseAccess.GetConnection();
            string query = @"SELECT 
    c.CarID, 
    ma.MakeName AS Make, 
    m.ModelName AS Model, 
    c.Year, 
    c.Price, 
    c.RentalPrice, 
    c.Color, 
    c.Status, 
    c.PurchaseDate,
	c.PriceDiscount,
	c.RentalDiscount,
    c.Description
    FROM Cars c
    JOIN Models m ON c.ModelId = m.ModelId
    JOIN Makes ma ON m.MakeId = ma.MakeId;";
            connection.Open();
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@status", carStatus);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                allCars.Add(new Car
                {
                    CarID = (int)reader["CarID"],
                    Make = reader["Make"] == DBNull.Value ? null : (string)reader["Make"],
                    Model = reader["Model"] == DBNull.Value ? null : (string)reader["Model"],
                    CarPrice = reader["Price"] == DBNull.Value ? null : (decimal)reader["Price"],
                    CarRentalPrice = reader["RentalPrice"] == DBNull.Value ? null : (decimal)reader["RentalPrice"],
                    Color = reader["Color"] == DBNull.Value ? null : (string)reader["Color"],
                    Status = reader["Status"] == DBNull.Value ? null : (short)reader["Status"],
                    Year = reader["Year"] == DBNull.Value ? null : (short)reader["Year"],
                    PurchaseDate = reader["PurchaseDate"] == DBNull.Value ? null : (DateTime)reader["PurchaseDate"],
                    PriceDiscount = reader["PriceDiscount"] == DBNull.Value ? null : (decimal)reader["PriceDiscount"],
                    RentalDiscount = reader["RentalDiscount"] == DBNull.Value ? null : (decimal)reader["RentalDiscount"],

                });
            }

            return allCars.AsEnumerable();


        }

        public static void ChangeCarStatus(int carId, int status)
        {
            using SqlConnection connection = DatabaseAccess.GetConnection();
            connection.Open();
            string query = @"UPDATE Cars
                         SET Status = @status
                         WHERE CarID = @carId;";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@status", status);
            command.Parameters.AddWithValue("@carId", carId);
            command.ExecuteNonQuery();
        }

        public static Car? FindCarById(int carId)
        {
            Car? car = null;
            using SqlConnection connection = DatabaseAccess.GetConnection();
            connection.Open();
            string query = @"select c.CarID, 
    ma.MakeName AS Make, 
    m.ModelName AS Model, 
    c.Year, 
    c.Price, 
    c.RentalPrice, 
    c.Color, 
    c.Status, 
    c.PurchaseDate,
	c.PriceDiscount,
	c.RentalDiscount,
    c.Description
FROM Cars c
JOIN Models m ON c.ModelId = m.ModelId
JOIN Makes ma ON m.MakeId = ma.MakeId  where carID=@carId;";
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@carId", carId);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                car = new Car
                {
                    CarID = (int)reader["CarID"],
                    Make = reader["Make"] == DBNull.Value ? null : (string)reader["Make"],
                    Model = reader["Model"] == DBNull.Value ? null : (string)reader["Model"],
                    CarPrice = reader["Price"] == DBNull.Value ? null : (decimal)reader["Price"],
                    CarRentalPrice = reader["RentalPrice"] == DBNull.Value ? null : (decimal)reader["RentalPrice"],
                    Color = reader["Color"] == DBNull.Value ? null : (string)reader["Color"],
                    Status = reader["Status"] == DBNull.Value ? null : (short)reader["Status"],
                    Year = reader["Year"] == DBNull.Value ? null : (short)reader["Year"],
                    PurchaseDate = reader["PurchaseDate"] == DBNull.Value ? null : (DateTime)reader["PurchaseDate"],
                    PriceDiscount = reader["PriceDiscount"] == DBNull.Value ? null : (decimal)reader["PriceDiscount"],
                    RentalDiscount = reader["RentalDiscount"] == DBNull.Value ? null : (decimal)reader["RentalDiscount"],
                };
            }





            return car;

        }

        public static int AddCarMake(string carMake, int userId)
        {
            try
            {
                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = "insert into Makes (MakeName,UserId)values(@carMake,@userId); SELECT SCOPE_IDENTITY();";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@carMake", carMake);
                command.Parameters.AddWithValue("@userId", userId);
                object insertedId = command.ExecuteScalar();
                int makeId = Convert.ToInt32(insertedId);
                return makeId;
            }
            catch (Exception ex) { throw new Exception($"Error - > {ex}"); }

        }

        public static int AddCarModel(AddCarModelDtos carModel)
        {
            try
            {
                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = "insert into Models(ModelName,MakeId)values(@modelName,@makeId); SELECT SCOPE_IDENTITY();";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@modelName", carModel.ModelName);
                command.Parameters.AddWithValue("@makeId", carModel.MakeId);
                object insertedId = command.ExecuteScalar();
                int modelId = Convert.ToInt32(insertedId);
                return modelId;
            }
            catch (Exception ex) { throw new Exception($"Error - > {ex}"); }


        }

        public static async Task<int> AddCar(AddCarDtos addCarDtos)
        {
            try
            {
                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = @"INSERT INTO Cars(Year, Price,RentalPrice,Color,Status,Description,ModelID,UserId)
VALUES
(@year, @price,@rentalPrice,@color,0,@desc,@modelId,@userId); SELECT SCOPE_IDENTITY();";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@year", addCarDtos.Year);
                command.Parameters.AddWithValue("@price", addCarDtos.CarPrice == null ? DBNull.Value : addCarDtos.CarPrice);
                command.Parameters.AddWithValue("@color", addCarDtos.Color);
                command.Parameters.AddWithValue("@desc", addCarDtos.Description == null ? DBNull.Value : addCarDtos.Description);
                command.Parameters.AddWithValue("@modelId", addCarDtos.ModelId);
                command.Parameters.AddWithValue("@rentalPrice", addCarDtos.CarRentalPrice == null ? DBNull.Value : addCarDtos.CarRentalPrice);
                command.Parameters.AddWithValue("@userId", addCarDtos.UserId);

                var insertedCar = command.ExecuteScalar();
                int carId = Convert.ToInt32(insertedCar);
                return carId;

            }
            catch (Exception ex) { throw new Exception($"Error ->  {ex}"); }


        }

        public static void RemoveMake(int makeId)
        {
            try
            {
                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = "Delete from Makes where MakeId=@makeId";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@makeId", makeId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception($"Error {ex} "); }



        }

        public static void RemoveModel(int modelId)
        {
            try
            {
                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = "Delete from Models where ModelId=@modelId";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@modelId", modelId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception($"Error {ex} "); }



        }
        public static async Task<int> UploadCarImagesByCarId(IFormFile[] images, int carId)
        {
            try
            {
                Car? car = FindCarById(carId);
                if (car == null) { return (int)UploadImageCar.CarNotFound; }

                UploadCarPhoto uploadCarPhoto = new UploadCarPhoto();
                List<string> valuesList = new List<string>();
                List<SqlParameter> parameters = new List<SqlParameter>();
                int index = 0;

                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();

                parameters.Add(new SqlParameter("@carId", carId));

                foreach (IFormFile image in images)
                {
                    string? imagePath = await uploadCarPhoto.UploadCarImage(image);
                    if (imagePath == null) { continue; }

                    string imageParam = "@imagePath" + index;

                    valuesList.Add($"({imageParam}, @carId)");

                    parameters.Add(new SqlParameter(imageParam, imagePath));
                    index++;
                }

                if (valuesList.Count > 0)
                {
                    string query = $"INSERT INTO CarImages (ImagePath, CarId) VALUES {string.Join(",", valuesList)};";
                    using SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddRange(parameters.ToArray());
                    command.ExecuteNonQuery();
                    return (int)UploadImageCar.ImageUploaded;
                }
                else
                {
                    return (int)UploadImageCar.ImageFileIsNull;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error --> {ex}");
            }
        }

        public static List<CarImage> GetCarImages(int carId)
        {
            List<CarImage> carImages = new List<CarImage>();
            using SqlConnection connection = DatabaseAccess.GetConnection();
            connection.Open();
            string query = "select ImageId, ImagePath from CarImages where CarId=@carId";
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@carId", carId);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string image = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty;
                int imageId = (int)reader["ImageId"];
                carImages.Add(new CarImage
                {
                    ImagePath = image ?? "",
                    ImageId = imageId
                });
            }
            return carImages;
        }
        public static Dictionary<int, List<string>> GetAllCarImages()
        {
            Dictionary<int, List<string>> allCarImages = new();
            StringBuilder imagePath = new StringBuilder();

            using SqlConnection connection = DatabaseAccess.GetConnection();
            connection.Open();

            string query = "SELECT CarId, ImagePath FROM CarImages";

            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int carId = (int)reader["CarId"];
                imagePath.Append(reader["ImagePath"].ToString());

                if (!allCarImages.ContainsKey(carId))
                {
                    allCarImages[carId] = new List<string>();
                }

                allCarImages[carId].Add(imagePath.ToString());
                imagePath.Clear();
            }

            return allCarImages;
        }

        public static void RemoveCar(int carId)
        {
            try
            {
                List<string> imagesCar = [];
                UploadCarPhoto carUpload = new UploadCarPhoto();
                using (SqlConnection connection = DatabaseAccess.GetConnection())
                {
                    connection.Open();
                    string query = "Select ImagePath from CarImages where carId=@carId";

                    using SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@carId", carId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            imagesCar.Add(reader["ImagePath"].ToString());

                        }
                    }

                    query = "delete from cars where carId=@carId";
                    using SqlCommand deleteCommand = new SqlCommand(query, connection);
                    deleteCommand.Parameters.AddWithValue("@carId", carId);
                    int rows = deleteCommand.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        carUpload.DeleteImagesFromServer(imagesCar);
                        OnCarRemoved?.Invoke(carId);
                    }
                }


            }
            catch (Exception ex)
            {
                throw new Exception($"Error --> {ex}");
            }


        }

        public static IEnumerable<CarAllInfoDtos> GetUserCarCart(int userId)
        {
            Dictionary<int, List<string>> allCarImages = GetAllCarImages();
            List<CarAllInfoDtos> allCars = [];
            using SqlConnection connection = DatabaseAccess.GetConnection();
            string query = @"SELECT
       
    c.CarID, 
    ma.MakeName AS Make, 
    m.ModelName AS Model, 
    c.Year, 
    c.Price, 
    c.RentalPrice, 
    c.Color, 
    c.Status, 
    c.PurchaseDate,
	c.PriceDiscount,
	c.RentalDiscount,
    c.Description,
    u.UserID,
    u.FirstName,
    u.LastName,
    u.Email,
    u.PhoneNumber,
    u.Address,
    u.CountryId,
	co.CountryName
FROM Carts cart
JOIN Users u On cart.UserId=u.UserID
JOIN Cars c on cart.CarId=c.CarID
Join Countries co On u.CountryId=co.CountryId
JOIN Models m ON c.ModelId = m.ModelId
JOIN Makes ma ON m.MakeId = ma.MakeId where cart.UserId=@userId;";
            connection.Open();
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", userId);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CarAllInfoDtos car = new CarAllInfoDtos
                {
                    CarID = (int)reader["CarID"],
                    Make = reader["Make"] == DBNull.Value ? null : (string)reader["Make"],
                    Model = reader["Model"] == DBNull.Value ? null : (string)reader["Model"],
                    CarPrice = reader["Price"] == DBNull.Value ? null : (decimal)reader["Price"],
                    CarRentalPrice = reader["RentalPrice"] == DBNull.Value ? null : (decimal)reader["RentalPrice"],
                    Color = reader["Color"] == DBNull.Value ? null : (string)reader["Color"],
                    Status = reader["Status"] == DBNull.Value ? null : (short)reader["Status"],
                    Description = reader["Description"] == DBNull.Value ? null : (string)reader["Description"],
                    Year = reader["Year"] == DBNull.Value ? null : (short)reader["Year"],
                    PurchaseDate = reader["PurchaseDate"] == DBNull.Value ? null : (DateTime)reader["PurchaseDate"],
                    PriceDiscount = reader["PriceDiscount"] == DBNull.Value ? null : (decimal)reader["PriceDiscount"],
                    RentalDiscount = reader["RentalDiscount"] == DBNull.Value ? null : (decimal)reader["RentalDiscount"],
                    User = new UserCarDtos
                    {
                        UserID = (int)reader["UserId"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Email = (string)reader["Email"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        Address = (string)reader["Address"],
                        CountryId = (int)reader["CountryId"],
                        CountryName = (string)reader["CountryName"]


                    }

                };
                if (allCarImages.TryGetValue(car.CarID, out var images))
                {
                    car.ImagesPath = images;
                }

                allCars.Add(car);



            }

            return allCars.AsEnumerable();


        }

        public static void RemoveCart(int userId, int carId)
        {
            try
            {
                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = "delete from carts where UserId=@userId and CarId=@carId";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@carId", carId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception($"ServerError RemoveCart ->{ex}"); }



        }

        public static IEnumerable<CarModel> GetUserModel(int userId)
        {

            List<CarModel> carModels = [];
            using SqlConnection connection = DatabaseAccess.GetConnection();
            string query = @"select m.modelId ,m.ModelName from models m Join Makes ma on m.MakeId=ma.MakeId where ma.UserId=@userId;";
            connection.Open();
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", userId);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                carModels.Add(new CarModel
                {
                    ModelId = (int)reader["modelId"],
                    ModelName = (string)reader["ModelName"]
                });

            }

            return carModels.AsEnumerable();


        }

        public static IEnumerable<CarMake> GetUserMake(int userId)
        {

            List<CarMake> carMake = [];
            using SqlConnection connection = DatabaseAccess.GetConnection();
            string query = @"select MakeId,MakeName from Makes where UserId=@userId;";
            connection.Open();
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", userId);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                carMake.Add(new CarMake
                {
                    MakeId = (int)reader["MakeId"],
                    MakeName = (string)reader["MakeName"]
                });

            }

            return carMake.AsEnumerable();


        }

        public static void RemoveCarImage(int imageId, string imagePath)
        {

            try
            {
                UploadCarPhoto upload = new UploadCarPhoto();
                upload.DeleteImagesFromServer([imagePath]); // هاي اختصار new string[]{imagePath} عشان ما انسى
                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = "delete from CarImages where ImageId=@imageId";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@imageId", imageId);
                command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw new Exception($"Server Error -> {ex}");
            }

        }

        public static void EditCar(EditCarDtos car)
        {

            try
            {

                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = @"UPDATE Cars
                               SET
                                 ModelId = @ModelId,
                                 Year = @Year,
                                 Color = @Color,
                                 Price = @CarPrice,
                                 RentalPrice = @CarRentalPrice,
                                 Description = @Description
                                 WHERE CarId = @CarId;";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@CarId", SqlDbType.Int).Value = car.CarId;
                command.Parameters.Add("@ModelId", SqlDbType.Int).Value = car.ModelId;
                command.Parameters.Add("@Year", SqlDbType.Int).Value = car.Year;
                command.Parameters.Add("@Color", SqlDbType.NVarChar, 20).Value = car.Color ?? (object)DBNull.Value;
                command.Parameters.Add("@CarPrice", SqlDbType.Decimal).Value = car.CarPrice ?? (object)DBNull.Value;
                command.Parameters.Add("@CarRentalPrice", SqlDbType.Decimal).Value = car.CarRentalPrice ?? (object)DBNull.Value;
                command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = car.Description ?? (object)DBNull.Value;
                command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw new Exception($"Server Error -> {ex}");
            }

        }

        public static IEnumerable<CarAllInfoDtos> GetCarsSortedByYear(int countryId, int pageNumber)
        {

            string query = @"SELECT 
c.CarID,
c.Year,
c.Price,
c.RentalPrice,
c.Color,
c.Status,
c.Description,
c.UserId,
m.MakeName as make,
mo.ModelName as model,
u.Address,
u.FirstName,
u.LastName,
u.UserId,
u.Email,
u.PhoneNumber,
co.CountryName,
co.CountryId
FROM Cars c
INNER JOIN Users u on c.UserId=u.UserID
INNER JOIN Models mo on c.ModelId=mo.modelId
INNER JOIN Makes m on mo.MakeId=m.MakeId
INNER JOIN Countries co on u.CountryId=co.CountryId
where u.CountryId=@countryId
ORDER BY Year DESC
OFFSET (@pageNumber-1) * 50 ROWS 
FETCH NEXT 50 ROWS ONLY;";

            return SortedCar(query, countryId, pageNumber).AsEnumerable();
        }

        public static IEnumerable<CarAllInfoDtos> GetCarsSortedByOldest(int countryId, int pageNumber)
        {
            string query = @"SELECT 
c.CarID,
c.Year,
c.Price,
c.RentalPrice,
c.Color,
c.Status,
c.Description,
c.UserId,
m.MakeName as make,
mo.ModelName as model,
u.Address,
u.FirstName,
u.LastName,
u.UserId,
u.Email,
u.PhoneNumber,
co.CountryName,
co.CountryId
FROM Cars c
INNER JOIN Users u on c.UserId=u.UserID
INNER JOIN Models mo on c.ModelId=mo.modelId
INNER JOIN Makes m on mo.MakeId=m.MakeId
INNER JOIN Countries co on u.CountryId=co.CountryId
where u.CountryId=@countryId
ORDER BY Year 
OFFSET (@pageNumber-1) * 50 ROWS 
FETCH NEXT 50 ROWS ONLY;";

            return SortedCar(query, countryId, pageNumber).AsEnumerable();

        }

        public static IEnumerable<CarAllInfoDtos> GetCarsForSale(int countryId, int pageNumber)
        {
            string query = @"SELECT 
c.CarID,
c.Year,
c.Price,
c.RentalPrice,
c.Color,
c.Status,
c.Description,
c.UserId,
m.MakeName as make,
mo.ModelName as model,
u.Address,
u.FirstName,
u.LastName,
u.UserId,
u.Email,
u.PhoneNumber,
co.CountryName,
co.CountryId
FROM Cars c
INNER JOIN Users u on c.UserId=u.UserID
INNER JOIN Models mo on c.ModelId=mo.modelId
INNER JOIN Makes m on mo.MakeId=m.MakeId
INNER JOIN Countries co on u.CountryId=co.CountryId
where u.CountryId=@countryId and Price is not Null
order by c.carID
OFFSET (@pageNumber-1) * 50 ROWS 
FETCH NEXT 50 ROWS ONLY;";

            return SortedCar(query, countryId, pageNumber).AsEnumerable();

        }

        public static IEnumerable<CarAllInfoDtos> GetCarForRental(int countryId, int pageNumber)
        {
            string query = @"SELECT 
c.CarID,
c.Year,
c.Price,
c.RentalPrice,
c.Color,
c.Status,
c.Description,
c.UserId,
m.MakeName as make,
mo.ModelName as model,
u.Address,
u.FirstName,
u.LastName,
u.UserId,
u.Email,
u.PhoneNumber,
co.CountryName,
co.CountryId
FROM Cars c
INNER JOIN Users u on c.UserId=u.UserID
INNER JOIN Models mo on c.ModelId=mo.modelId
INNER JOIN Makes m on mo.MakeId=m.MakeId
INNER JOIN Countries co on u.CountryId=co.CountryId
where u.CountryId=@countryId and RentalPrice is not Null
order by c.carID
OFFSET (@pageNumber-1) * 50 ROWS 
FETCH NEXT 50 ROWS ONLY;";

            return SortedCar(query, countryId, pageNumber).AsEnumerable();

        }


        public static IEnumerable<CarAllInfoDtos> GetCarsContaine(int countryId, string contain, int pageNumber)
        {
            Dictionary<int, List<string>> allCarImages = GetAllCarImages();
            List<CarAllInfoDtos> allCars = [];
            using SqlConnection connection = DatabaseAccess.GetConnection();
            string query = @"DECLARE @containNumeric FLOAT;
DECLARE @containYear INT;
SET @containNumeric = TRY_CAST(@contain AS FLOAT);
SET @containYear = TRY_CAST(@contain AS INT);
SELECT 
    c.CarID, 
    ma.MakeName AS Make, 
    m.ModelName AS Model, 
    c.Year, 
    c.Price, 
    c.RentalPrice, 
    c.Color, 
    c.Status, 
    c.PurchaseDate,
    c.PriceDiscount,
    c.RentalDiscount,
    c.Description,
    u.UserID,
    u.FirstName,
    u.LastName,
    u.Email,
    u.PhoneNumber,
    u.Address,
    u.CountryId,
    co.CountryName
FROM Cars c
JOIN Users u ON c.UserId = u.UserId
JOIN Countries co ON u.CountryId = co.CountryId
JOIN Models m ON c.ModelId = m.ModelId
JOIN Makes ma ON m.MakeId = ma.MakeId
WHERE u.CountryId = @countryId
  AND (
        (@containNumeric is not null and c.Price = @containNumeric )
        OR (@containNumeric is not null and  c.RentalPrice = @containNumeric)
		OR
        ( @containYear is not null and c.Year = @containYear) 

        OR m.ModelName LIKE '%' + @contain + '%'

        OR ma.MakeName LIKE '%' + @contain + '%' 
      )
ORDER BY c.CarID
OFFSET (@pageNumber - 1) * 50 ROWS 
FETCH NEXT 50 ROWS ONLY;";
            connection.Open();
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@countryId", countryId);
            command.Parameters.AddWithValue("@contain", contain);
            command.Parameters.AddWithValue("@pageNumber", pageNumber);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CarAllInfoDtos car = new CarAllInfoDtos
                {
                    CarID = (int)reader["CarID"],
                    Make = reader["Make"] == DBNull.Value ? null : (string)reader["Make"],
                    Model = reader["Model"] == DBNull.Value ? null : (string)reader["Model"],
                    CarPrice = reader["Price"] == DBNull.Value ? null : (decimal)reader["Price"],
                    CarRentalPrice = reader["RentalPrice"] == DBNull.Value ? null : (decimal)reader["RentalPrice"],
                    Color = reader["Color"] == DBNull.Value ? null : (string)reader["Color"],
                    Status = reader["Status"] == DBNull.Value ? null : (short)reader["Status"],
                    Description = reader["Description"] == DBNull.Value ? null : (string)reader["Description"],
                    Year = reader["Year"] == DBNull.Value ? null : (short)reader["Year"],
                    PurchaseDate = reader["PurchaseDate"] == DBNull.Value ? null : (DateTime)reader["PurchaseDate"],
                    PriceDiscount = reader["PriceDiscount"] == DBNull.Value ? null : (decimal)reader["PriceDiscount"],
                    RentalDiscount = reader["RentalDiscount"] == DBNull.Value ? null : (decimal)reader["RentalDiscount"],
                    User = new UserCarDtos
                    {
                        UserID = (int)reader["UserId"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Email = (string)reader["Email"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        Address = (string)reader["Address"],
                        CountryId = (int)reader["CountryId"],
                        CountryName = (string)reader["CountryName"]


                    }

                };
                if (allCarImages.TryGetValue(car.CarID, out var images))
                {
                    car.ImagesPath = images;
                }

                allCars.Add(car);



            }

            return allCars.AsEnumerable();


        }
        private static List<CarAllInfoDtos> SortedCar(string query, int countryId, int pageNumber)
        {
            List<CarAllInfoDtos> cars = [];
            Dictionary<int, List<string>> allCarImages = GetAllCarImages();
            using SqlConnection connection = DatabaseAccess.GetConnection();
            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@countryId", SqlDbType.Int).Value = countryId;
            command.Parameters.Add("@pageNumber", SqlDbType.Int).Value = pageNumber;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    CarAllInfoDtos car = new CarAllInfoDtos
                    {
                        CarID = (int)reader["CarID"],
                        Year = reader["Year"] as short?,
                        CarPrice = reader["Price"] as decimal?,
                        CarRentalPrice = reader["RentalPrice"] as decimal?,
                        Color = reader["Color"] as string,
                        Status = reader["Status"] as short?,
                        Description = reader["Description"] as string,



                        User = new UserCarDtos
                        {
                            UserID = (int)reader["UserId"],
                            Address = reader["Address"] as string,
                            FirstName = reader["FirstName"] as string,
                            LastName = reader["LastName"] as string,
                            Email = reader["Email"] as string,
                            CountryId = (int)reader["CountryId"],
                            CountryName = reader["CountryName"] as string,
                            PhoneNumber = reader["PhoneNumber"] as string
                        },

                        Make = reader["make"] as string,
                        Model = reader["model"] as string
                    };

                    if (allCarImages.TryGetValue(car.CarID, out var images))
                    {
                        car.ImagesPath = images;
                    }

                    cars.Add(car);
                }
            }

            return cars;

        }



    }



}
