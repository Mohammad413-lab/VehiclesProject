using System.Data;
using CarRentalSale.dtos;
using CarRentalSale.dtos.response;
using CarRentalSale.models;
using CarRentalSale.response;
using Microsoft.Data.SqlClient;

namespace CarRentalSale.datarepositories
{

    public static class SalesOrderRepository
    {


        public static void AddSalesOrder(CreateSaleOrder saleOrder)
        {
            try
            {
                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = "insert into SalesOrder(UserId,CarId,OrderDate,TotalAmount,Notes,Status,UpdatedAt) values(@userId,@carId,GETDATE(),@totalAmount,@note,0,null);";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@userId", SqlDbType.Int).Value = saleOrder.UserId;
                command.Parameters.Add("@carId", SqlDbType.Int).Value = saleOrder.CarId;
                command.Parameters.Add("@totalAmount", SqlDbType.Decimal).Value = saleOrder.TotalAmount;
                command.Parameters.Add("@note", SqlDbType.NVarChar).Value = saleOrder.Notes;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"SERVER ERROR --> {ex}");
            }

        }


        public static IEnumerable<OrderSaleResponse> GetMyOrderSale(int userId)
        {
            try
            {
                Dictionary<int, List<string>> allCarsImages = CarsRepository.GetAllCarImages();
                List<OrderSaleResponse> orderSale = [];
                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = @"
SELECT
    c.CarId,
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
    co.CountryName, 
	s.SalesOrderId,
	s.OrderDate,
	s.TotalAmount,
	s.Notes,
	s.Status as OrderStatus,
	s.UpdatedAt
FROM SalesOrder s
JOIN Cars c On s.CarId=c.CarID
JOIN Users u ON c.UserId = u.UserId
JOIN Countries co ON u.CountryId = co.CountryId
JOIN Models m ON c.ModelId = m.ModelId
JOIN Makes ma ON m.MakeId = ma.MakeId
WHERE s.UserId = 24
ORDER BY s.OrderDate ASC, s.UpdatedAt Asc ";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int carId = (int)reader["CarID"];

                    CarAllInfoDtos car = new()
                    {

                        CarID = carId,
                        Make = reader["Make"] as string,
                        Model = reader["Model"] as string,
                        Year = reader["Year"] != DBNull.Value ? (short)reader["Year"] : null,
                        CarPrice = reader["Price"] != DBNull.Value ? (decimal?)reader["Price"] : null,
                        CarRentalPrice = reader["RentalPrice"] != DBNull.Value ? (decimal?)reader["RentalPrice"] : null,
                        Color = reader["Color"] as string,
                        Status = reader["Status"] != DBNull.Value ? (short)reader["Status"] : null,
                        PurchaseDate = reader["PurchaseDate"] != DBNull.Value ? (DateTime?)reader["PurchaseDate"] : null,
                        PriceDiscount = reader["PriceDiscount"] != DBNull.Value ? (decimal?)reader["PriceDiscount"] : null,
                        RentalDiscount = reader["RentalDiscount"] != DBNull.Value ? (decimal?)reader["RentalDiscount"] : null,
                        Description = reader["Description"] as string,


                        User = new UserCarDtos
                        {
                            UserID = (int)reader["UserId"],
                            FirstName = reader["FirstName"] as string,
                            LastName = reader["LastName"] as string,
                            Email = reader["Email"] as string,
                            PhoneNumber = reader["PhoneNumber"] as string,
                            Address = reader["Address"] as string,
                            CountryId = (int)reader["CountryId"],
                            CountryName = reader["CountryName"] as string
                        }


                    };
                    if (allCarsImages.TryGetValue(car.CarID, out var images))
                    {
                        car.ImagesPath = images;
                    }

                    orderSale.Add(new OrderSaleResponse
                    {
                        SaleOrderId = (int)reader["SalesOrderId"],
                        OrderDate = (DateTime)reader["OrderDate"],
                        Note = reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : null,
                        Status = (byte)reader["OrderStatus"],
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? (DateTime)reader["UpdatedAt"] : null,
                        Car=car

                    });
                }
                return orderSale.AsEnumerable();
            }
            catch (Exception ex)
            {
                throw new Exception($"SERVER ERROR --> {ex}");
            }

        }



    }
}