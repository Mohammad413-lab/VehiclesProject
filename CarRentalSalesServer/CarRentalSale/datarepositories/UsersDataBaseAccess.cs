using System.Collections;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using CarRentalSale.dtos;
using CarRentalSale.dtos.response;
using CarRentalSale.enums;
using CarRentalSale.helper;
using CarRentalSale.models;
using CarRentalSale.request;
using CarRentalSale.request.dtos;
using CarRentalSale.response;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
namespace CarRentalSale.datarepositories
{
    public static class UsersRepository
    {

        public static bool UserLogin(string email, string password)
        {
            using (SqlConnection connection = DatabaseAccess.GetConnection())
            {
                connection.Open();
                string query = "SELECT PasswordHash, PasswordSalt FROM Users WHERE Email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string storedPasswordHash = reader["PasswordHash"].ToString();
                        string storedSalt = reader["PasswordSalt"].ToString();


                        byte[] saltBytes = Convert.FromBase64String(storedSalt);


                        var hashBytes = new Rfc2898DeriveBytes(
                            password,
                            saltBytes,
                            100_000,
                            HashAlgorithmName.SHA256
                        ).GetBytes(32);

                        string hashedInputPassword = Convert.ToBase64String(hashBytes);

                        if (storedPasswordHash == hashedInputPassword)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static IEnumerable<UserDto> GetAllUsers()
        {
            List<UserDto> users = new List<UserDto>();

            using (SqlConnection connection = DatabaseAccess.GetConnection())
            {
                connection.Open();
                string query = "SELECT * ,a.AuthName,co.CountryName FROM Users INNER JOIN Auth a On Users.AuthId=a.AuthId INNER JOIN Countries co On Users.CountryId=co.CountryId";
                SqlCommand command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        users.Add(new UserDto
                        {
                            UserID = (int)reader["UserID"],
                            FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : string.Empty,
                            LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : string.Empty,
                            Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty,
                            PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? reader["PhoneNumber"].ToString() : string.Empty,
                            Adress = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : string.Empty,
                            AuthName = reader["AuthName"] != DBNull.Value ? reader["AuthName"].ToString() : string.Empty,
                            CountryName = (string)reader["CountryName"],
                            CountryId = (int)reader["CountryId"]

                        });
                    }
                }

            }

            return users;
        }

    
        public static IEnumerable<Sales> GetUserSalesById(int userId)
        {
            List<Sales> userSales = new List<Sales>();
            Dictionary<int, List<string>> carsImages = CarsRepository.GetAllCarImages();
            using (SqlConnection connection = DatabaseAccess.GetConnection())
            {
                connection.Open();
                string query = @"
                              SELECT s.SaleId, s.SaleDate, s.SalePrice, 
                              o.PaymentId, o.PaymentDate, o.Amount, o.PaymentType, o.PaymentStatus,
                              o.UserID , o.CarId, m.MakeName as Make,mo.ModelName as Model,c.Year,c.Color, c.Price AS CarPrice,  c.Description
                              FROM Sales s
                              JOIN Orders o On o.OrderId=s.OrderId
                              JOIN Cars c ON o.CarId = c.CarId 
                              JOIN Models mo ON c.ModelId = mo.ModelId
                              JOIN Makes m ON m.MakeId = mo.MakeId
                              WHERE p.UserID = 17;";

                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@userId", userId);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Sales sales = new Sales
                        {
                            SaleId = (int)reader["SaleId"],
                            SaleDate = (DateTime)reader["SaleDate"],
                            Payment = new Payment
                            {
                                PaymentId = (int)reader["PaymentId"],
                                PaymentDate = (DateTime)reader["PaymentDate"],
                                Amount = (decimal)reader["Amount"],
                                PaymentStatus = (int)reader["PaymentStatus"],
                                PaymentType = (int)reader["PaymentType"],

                            },
                            Car = new CarDtos
                            {
                                CarID = (int)reader["CarID"],
                                Make = (string)reader["Make"],
                                Model = (string)reader["Model"],
                                Year = (int)reader["Year"],
                                Color = (string)reader["Color"],
                                Description = reader["Description"] == DBNull.Value ? null : (string)reader["Description"],
                                CarPrice = (decimal)reader["CarPrice"]



                            }
                        };
                        if (carsImages.TryGetValue(sales.Car.CarID, out var images)) { sales.Car.ImagesPath = images; }
                        userSales.Add(sales);
                    }

                }

            }
            return userSales;
        }


        public static IEnumerable<Rental> GetUserRentals(int userId)
        {
            Dictionary<int, List<string>> carImages = CarsRepository.GetAllCarImages();
            List<Rental> userRental = new List<Rental>();
            using (SqlConnection connection = DatabaseAccess.GetConnection())
            {
                connection.Open();
                string query = @"select r.RentalID,r.RentalStartDate,r.RentalEndDate,r.RentalPrice,r.Ispayed,r.PaymentID,
 c.CarID,c.PriceDiscount,c.RentalDiscount,m.MakeName as Make,mo.ModelName as Model,c.Year,c.Color, c.Price AS CarPrice, c.RentalPrice, c.Description
 from Rentals r join Cars c ON r.CarID=c.CarID
 INNER JOIN Models mo ON mo.ModelId = c.ModelId
 INNER JOIN Makes m ON  m.MakeId = mo.MakeId
 where r.UserId=@userId;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Rental rental = new()
                        {
                            RentalId = (int)reader["RentalId"],
                            RentalStartDate = (DateTime)reader["RentalStartDate"],
                            RentalEndDate = (DateTime)reader["RentalEndDate"],
                            Car = new CarDtos
                            {
                                CarID = (int)reader["CarID"],
                                Make = (string)reader["Make"],
                                Model = (string)reader["Model"],
                                Year = (int)reader["Year"],
                                CarPrice = (decimal)reader["CarPrice"],
                                CarRentalPrice = reader["RentalPrice"] == DBNull.Value ? null : (decimal)reader["RentalPrice"],
                                Color = (string)reader["Color"],
                                Description = reader["Description"] == DBNull.Value ? null : (string)reader["Description"],
                                PriceDiscount = reader["PriceDiscount"] == DBNull.Value ? null : (decimal)reader["PriceDiscount"],
                                RentalDiscount = reader["RentalDiscount"] == DBNull.Value ? null : (decimal)reader["RentalDiscount"]

                            },
                            IsPayed = (bool)reader["IsPayed"],
                            RentalPrice = (decimal)reader["RentalPrice"],
                            PaymentId = reader["PaymentID"] == DBNull.Value ? null : (int)reader["PaymentID"]


                        };
                        if (carImages.TryGetValue(rental.Car.CarID, out var Images)) { rental.Car.ImagesPath = Images; }


                        userRental.Add(rental);
                    }
                }
            }
            return userRental;
        }

        public static void DeleteUserById(int userId)
        {
            try
            {
                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = @"Delete From Users where userID=@userId;";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting user : " + ex);
            }



        }

        public static void UpdateUserInformation(UserDto user)
        {

            try
            {
                using SqlConnection connection = DatabaseAccess.GetConnection();
                string query = @"UPDATE Users
                   SET FirstName = @firstName,
                   LastName=@LastName,
                   Email = @Email,
	               Address=@Address,
	               PhoneNumber=@PhoneNumber
                   WHERE UserID = @userId;";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user.UserID);
                command.Parameters.AddWithValue("@firstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Address", user.Adress);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error update " + ex);
            }




        }

        public static UserDto? GetUserById(int userId)
        {


            try
            {

                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = @"select UserId ,FirstName ,LastName, Email,co.CountryName,Users.CountryId,Address,PhoneNumber,a.AuthName from Users INNER JOIN Auth a ON Users.AuthId=a.AuthId INNER JOIN Countries co ON Users.CountryId=co.CountryId where UserID=@userId;";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    UserDto customer = new UserDto
                    {
                        UserID = (int)reader["UserID"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Email = (string)reader["Email"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        Adress = (string)reader["Address"],
                        AuthName = (string)reader["AuthName"],
                        CountryName = (string)reader["CountryName"],
                        CountryId = (int)reader["CountryId"]


                    };
                    return customer;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error -->" + ex);
            }

            return null;



        }

        public static UserDto? GetUserByEmail(string email)
        {


            try
            {

                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = @"select UserId ,FirstName ,LastName, Email,co.CountryName,co.CountryId,Address,PhoneNumber,a.AuthName from Users INNER JOIN Auth a ON Users.AuthId=a.AuthId INNER JOIN Countries co ON Users.CountryId=co.CountryId where Email=@email;";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);
                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    UserDto customer = new UserDto
                    {
                        UserID = (int)reader["UserID"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Email = (string)reader["Email"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        Adress = (string)reader["Address"],
                        AuthName = (string)reader["AuthName"],
                        CountryName = (string)reader["CountryName"],
                        CountryId = (int)reader["CountryId"]


                    };
                    return customer;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error -->" + ex);
            }

            return null;



        }

        public static int ChangeUserPassword(ChangePasswordDtos changePassword)
        {
            string oldHashedPass;
            string customerSaltedPass;
            using SqlConnection connection = DatabaseAccess.GetConnection();
            connection.Open();
            string query = "select PasswordSalt,PasswordHash from  Users where UserID=@userId; ";
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", changePassword.UserID);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (!reader.Read())
                {
                    return (int)ChangePasswordState.UserNotFound;
                }
                oldHashedPass = (string)reader["PasswordHash"];
                customerSaltedPass = (string)reader["PasswordSalt"];

            }

            PasswordEncryption passwordEncryption = new PasswordEncryption
            {
                Password = changePassword.OldPassword,
                PasswordSalt = null
            };
            passwordEncryption.EncryptionPass(customerSaltedPass);
            if (passwordEncryption.Password == oldHashedPass)
            {

                query = @"UPDATE Users
                           SET PasswordHash = @newHashedPass,
                           PasswordSalt=@OldSalt
                           WHERE UserID = @userID;";
                passwordEncryption.Password = changePassword.NewPassword;
                passwordEncryption.EncryptionPass(customerSaltedPass);
                using SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@newHashedPass", passwordEncryption.Password);
                sqlCommand.Parameters.AddWithValue("@OldSalt", passwordEncryption.PasswordSalt);
                sqlCommand.Parameters.AddWithValue("@userID", changePassword.UserID);
                sqlCommand.ExecuteNonQuery();
                return (int)ChangePasswordState.PasswordChanged;
            }



            return (int)ChangePasswordState.OldPasswordNotCorrect;



        }

        public static int ChangeUserPasswordByEmail(ResetPasswordViewModel reset)
        {
            try
            {
                string customerSaltedPass;
                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = "select PasswordSalt from  Users where Email=@email; ";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", reset.Email);



                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (!reader.Read())
                    {
                        Console.WriteLine($"email is ={reset.Email} ");
                        return (int)ChangePasswordState.EmailNotFound;
                    }
                    customerSaltedPass = (string)reader["PasswordSalt"];

                }
                Console.WriteLine($"Correct Email is ={reset.Email} ");
                PasswordEncryption passwordEncryption = new PasswordEncryption
                {
                    Password = reset.NewPassword,
                    PasswordSalt = null
                };
                passwordEncryption.EncryptionPass(customerSaltedPass);

                query = @"UPDATE Users
                           SET PasswordHash = @newHashedPass,
                           PasswordSalt=@OldSalt
                           WHERE Email = @email;";
                passwordEncryption.Password = reset.NewPassword;
                passwordEncryption.EncryptionPass(customerSaltedPass);
                using SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@newHashedPass", passwordEncryption.Password);
                sqlCommand.Parameters.AddWithValue("@OldSalt", passwordEncryption.PasswordSalt);
                sqlCommand.Parameters.AddWithValue("@email", reset.Email);
                sqlCommand.ExecuteNonQuery();
                return (int)ChangePasswordState.PasswordChanged;
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR -> {ex}");
            }



        }

        public static void VerifiedUserEmail(string email)
        {
            try
            {

                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = "Update Users set isVerified=1 where Email=@email ";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR -> {ex}");
            }



        }

        public static bool IsUserEmailExist(string email)
        {


            using SqlConnection connection = DatabaseAccess.GetConnection();
            connection.Open();
            string query = "select UserId from Users where Email=@email";
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@email", email);
            object result = command.ExecuteScalar();
            return result != null && result != DBNull.Value;

        }

        public static IEnumerable<Country> GetAllCountry()
        {
            List<Country> allCountry = [];
            using SqlConnection connection = DatabaseAccess.GetConnection();
            connection.Open();
            string query = "select * from Countries";
            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Country country = new()
                {
                    CountryId = (int)reader["CountryId"],
                    CountryName = (string)reader["CountryName"]
                };
                allCountry.Add(country);

            }

            return allCountry.AsEnumerable();
        }

        public static void AddCart(int userId, int carId)
        {

            try
            {

                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = "insert into Carts(UserId,CarId) values(@userId,@carId)";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@carId", carId);
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR -> {ex}");
            }


        }



        public static void SentMessage(Message message)
        {
            try
            {

                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = "insert into Chats (FromId,ToId,Message)values(@fromId,@toId,@message);";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@fromId", message.FromId);
                command.Parameters.AddWithValue("@toId", message.ToId);
                command.Parameters.AddWithValue("@message", message.MessageContent.Trim());
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR -> {ex}");
            }
        }

        public static IEnumerable<MessageResponse> GetUserMessages(int firstUserId, int secondUserId)
        {
            List<MessageResponse> messages = new List<MessageResponse>();
            using SqlConnection connection = DatabaseAccess.GetConnection();
            connection.Open();

            string query = @"
    SELECT 
        sender.FirstName AS SenderFirstName,
        sender.LastName AS SenderLastName,
        sender.Email AS SenderEmail,
        re.FirstName AS ReceiverFirstName,
        re.LastName AS ReceiverLastName,
        re.Email AS ReceiverEmail,
        c.ChatId,
        c.FromId as SenderId,
        c.ToId as ReceiverId,
        c.Message,
        c.DateSent,
        c.IsRead,
        c.IsDeletedFrom,
        c.IsDeletedTo
    FROM Chats c
    JOIN Users sender ON sender.UserID = c.FromId
    JOIN Users re ON re.UserID = c.ToId
    WHERE (c.FromId = @firstUserId AND c.ToId = @secondUserId  AND c.IsDeletedFrom = 0)
       OR (c.FromId = @secondUserId AND c.ToId = @firstUserId AND c.IsDeletedTo = 0)
    ORDER BY c.DateSent";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@firstUserId", firstUserId);
            command.Parameters.AddWithValue("@secondUserId", secondUserId);

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var message = new MessageResponse
                {
                    ChatId = reader.GetInt32(reader.GetOrdinal("ChatId")),
                    FromUser = new UserInfoMessage
                    {
                        UserId = reader.GetInt32(reader.GetOrdinal("SenderId")),
                        FirstName = reader.GetString(reader.GetOrdinal("SenderFirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("SenderLastName")),
                        Email = reader.GetString(reader.GetOrdinal("SenderEmail"))
                    },
                    ToUser = new UserInfoMessage
                    {
                        UserId = reader.GetInt32(reader.GetOrdinal("ReceiverId")),
                        FirstName = reader.GetString(reader.GetOrdinal("ReceiverFirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("ReceiverLastName")),
                        Email = reader.GetString(reader.GetOrdinal("ReceiverEmail"))
                    },
                    Message = reader.GetString(reader.GetOrdinal("Message")),
                    DateSent = reader.GetDateTime(reader.GetOrdinal("DateSent")),
                    IsRead = reader.GetBoolean(reader.GetOrdinal("IsRead")),
                    IsDeletedFrom = reader.GetBoolean(reader.GetOrdinal("IsDeletedFrom")),
                    IsDeletedTo = reader.GetBoolean(reader.GetOrdinal("IsDeletedTo"))
                };

                messages.Add(message);
            }

            return messages.AsEnumerable();
        }


        public static IEnumerable<UserMessagesEmail> GetMessageEmails(int userId)
        {
            List<UserMessagesEmail> UserEmails = new List<UserMessagesEmail>();
            using SqlConnection connection = DatabaseAccess.GetConnection();
            connection.Open();

            string query = @"SELECT 
    u.UserID,
    u.Email,
    u.FirstName,
	u.LastName,
    c.Message,
    c.DateSent,
    c.IsRead,
    CASE 
        WHEN c.FromId = @userId THEN CAST(0 AS BIT)
        ELSE CAST(1 AS BIT)
    END AS IsFromOthers
FROM (
    SELECT DISTINCT
        CASE 
            WHEN c.FromId = @userId THEN re.UserID
            WHEN c.ToId = @userId THEN sender.UserID
        END AS UserID,
        CASE 
            WHEN c.FromId = @userId THEN re.Email
            WHEN c.ToId = @userId THEN sender.Email
        END AS Email,
        	  CASE 
            WHEN c.FromId =@userId THEN re.FirstName
            WHEN c.ToId = @userId THEN sender.FirstName
        END AS firstName,
			  CASE 
            WHEN c.FromId =@userId THEN re.LastName
            WHEN c.ToId = @userId THEN sender.LastName
        END AS LastName
    FROM Chats c
    JOIN Users sender ON sender.UserID = c.FromId
    JOIN Users re ON re.UserID = c.ToId
    WHERE  ( (c.FromId = @userId AND c.IsDeletedFrom = 0)
     OR (c.ToId = @userId AND c.IsDeletedTo = 0) )
) u
OUTER APPLY (
    SELECT TOP 1 c2.Message, c2.DateSent, c2.FromId, c2.IsRead
    FROM Chats c2
    WHERE (
        (c2.FromId = @userId AND c2.ToId = u.UserID AND c2.IsDeletedFrom = 0)
        OR
        (c2.ToId = @userId AND c2.FromId = u.UserID AND c2.IsDeletedTo = 0)
    )
    ORDER BY c2.DateSent DESC
) c
ORDER BY c.DateSent DESC";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", userId);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                var messageEmail = new UserMessagesEmail
                {
                    UserId = reader.GetInt32(reader.GetOrdinal("UserID")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Message = reader.GetString(reader.GetOrdinal("Message")),
                    IsFromOthers = reader.GetBoolean(reader.GetOrdinal("IsFromOthers")),
                    IsRead = reader.GetBoolean(reader.GetOrdinal("IsRead")),
                    dateSent = reader.GetDateTime(reader.GetOrdinal("DateSent")),
                    FullName = $"{reader.GetString(reader.GetOrdinal("FirstName"))} {reader.GetString(reader.GetOrdinal("LastName"))}"
                };

                UserEmails.Add(messageEmail);
            }

            return UserEmails.AsEnumerable();
        }

        public static void RemoveAllMessagesFromUser(int userId, int userChatWithId)
        {
            try
            {
                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = @"UPDATE Chats
SET 
    IsDeletedFrom = CASE 
                      WHEN FromId = @userId THEN 1 
                      ELSE IsDeletedFrom 
                    END,
    IsDeletedTo = CASE 
                    WHEN ToId = @userId  THEN 1 
                    ELSE IsDeletedTo 
                  END
WHERE (FromId = @userId  AND ToId = @userChatWithId) 
   OR (ToId = @userId  AND FromId = @userChatWithId);";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@userChatWithId", userChatWithId);
                command.ExecuteNonQuery();

            }
            catch (Exception ex) { throw new Exception($"Server Error RemoveAllMessagesFromUser -> {ex}"); }

        }


        public static void ReadAllMessage(int userId, int userChatWithId)
        {
            try
            {
                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                string query = @"UPDATE Chats
SET IsRead=1
WHERE (FromId = @userChatWithId  AND ToId = @userId)";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@userChatWithId", userChatWithId);
                command.ExecuteNonQuery();

            }
            catch (Exception ex) { throw new Exception($"Server Error ReadAllMessage -> {ex}"); }

        }

        public static void EditUser(EditUserDto newUserInfo, UserDto oldUserInfo)
        {

            try
            {
                string query = @"UPDATE users 
SET FirstName = @firstName,
    LastName = @lastName,
    PhoneNumber = @phoneNumber,
    Address = @address,
    CountryId=@countryId
WHERE UserID = @userId;";

                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@firstName", SqlDbType.NVarChar, 50).Value = string.IsNullOrEmpty(newUserInfo.FirstName) ? oldUserInfo.FirstName : newUserInfo.FirstName;
                command.Parameters.Add("@lastName", SqlDbType.NVarChar, 50).Value = string.IsNullOrEmpty(newUserInfo.LastName) ? oldUserInfo.LastName : newUserInfo.LastName;
                command.Parameters.Add("@phoneNumber", SqlDbType.NVarChar, 15).Value = string.IsNullOrEmpty(newUserInfo.PhoneNumber) ? oldUserInfo.PhoneNumber : newUserInfo.PhoneNumber;
                command.Parameters.Add("@address", SqlDbType.NVarChar, 255).Value = string.IsNullOrEmpty(newUserInfo.Address) ? oldUserInfo.Adress : newUserInfo.Address;
                 command.Parameters.Add("@countryId", SqlDbType.Int).Value = newUserInfo.CountryId;
                command.Parameters.Add("@userId", SqlDbType.Int).Value = newUserInfo.UserID;
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception($"Server Error -----------> {ex}"); }






        }

          public static void UpdateUserEmail(int userId, string email)
        {

            try
            {
                string query = @"UPDATE users 
SET Email = @email
WHERE UserID = @userId;";

                using SqlConnection connection = DatabaseAccess.GetConnection();
                connection.Open();
                using SqlCommand command = new SqlCommand(query, connection);
                   command.Parameters.Add("@email", SqlDbType.NVarChar,255).Value = email;
                command.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception($"Server Error -----------> {ex}"); }






        }

    }
}