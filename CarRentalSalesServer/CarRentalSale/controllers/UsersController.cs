
using Microsoft.AspNetCore.Mvc;
using global::CarRentalSale.dtos.response;
using global::CarRentalSale.interfacee;
using global::CarRentalSale.models;
using CarRentalSale.request.dtos;
using CarRentalSale.enums;
using CarRentalSale.dtos;
using Microsoft.IdentityModel.Tokens;
using CarRentalSale.response;
using System.Threading.Tasks;
using CarRentalSale.services;
namespace CarRentalSale.Controllers
{


    namespace CarRentalSale.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class UsersController(IUsersServices usersServices, IValidationService validationService) : Controller
        {


            private IUsersServices _userServices = usersServices;
            private IValidationService _validation = validationService;

            [HttpGet("GetAllUsers")]
            public IActionResult GetAllUsers()
            {
                IEnumerable<UserDto> user = _userServices.GetAllUsers();
                if (user == null || !user.Any())
                {
                    return NotFound("No customers found.");
                }
                return Ok(user);
            }

            [HttpGet("GetUserSalesById")]
            public IActionResult GetUserSalesById(int userId)
            {
                IEnumerable<Sales> userSales = _userServices.GetUserSalesById(userId);
                if (userSales == null || !userSales.Any())
                {
                    return NotFound(new { message = "No sales yet for this user", status = false });
                }
                return Ok(userSales);
            }

            [HttpGet("GetUserRentalsById")]
            public IActionResult GetUserRentalsById(int userId)
            {
                IEnumerable<Rental> userRental = _userServices.GetRentalHistory(userId);
                if (userRental == null || !userRental.Any())
                {
                    return NotFound("This user has no rental ");
                }
                return Ok(userRental);
            }

        

            [HttpDelete("DeleteUserById")]
            public IActionResult DeleteUserById(int userId)
            {
                _userServices.DeleteUser(userId);
                return Ok("User Deleted");
            }


            [HttpGet("GetUserById")]
            public IActionResult GetUserById(int userId)
            {
                UserDto? user = _userServices.GetUserById(userId);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                return Ok(user);
            }


            [HttpGet("GetUserByEmail")]
            public IActionResult GetUserByEmail(string email)
            {
                UserDto? user = _userServices.GetUserByEmail(email);
                if (user == null)
                {
                    return NotFound(new { message = "User not found", status = true });
                }
                return Ok(user);
            }



            [HttpPost("ChangeUserPassword")]
            public IActionResult ChangeUserPassword(ChangePasswordDtos changePassword)
            {
             
                if (!_validation.IsValidPassword(changePassword.NewPassword))
                {
                    return Ok(new { message = "New password must contain 8 characters or number, an uppercase letter, a lowercase letter... ", status = false });
                }
                int changePasswordProcessing = _userServices.ChangePassword(changePassword);
                switch (changePasswordProcessing)
                {
                    case (int)ChangePasswordState.PasswordChanged: return Ok(new { message = "Password changed", status = true });

                    case (int)ChangePasswordState.OldPasswordNotCorrect: return Ok(new { message = "Old password not correct", status = false });

                    case (int)ChangePasswordState.UserNotFound: return NotFound("User not found");

                }
                return BadRequest("Some error happend");
            }

            [HttpGet("ResetUserPassword")]
            public async Task<IActionResult> ResetUserPassword(string email)
            {
                try
                {
                    if (!_userServices.IsUserEmailExist(email))
                    {
                        return NotFound(new { message = $"Email {email} not found", status = false });
                    }

                    var token = Guid.NewGuid();
                    var resetLink = $"{ApiSetting.urlWithoutApi}Email/ResetPassword?token={token}&email={email}";

                    EmailRequest emailRequest = new EmailRequest
                    {
                        To = email,
                        Body = $"<p>Click on the link </p><a href='{resetLink}'>change password here/> ",
                        Subject = "Reset Password"
                    };




                    if (await _userServices.RecoveryPassword(emailRequest))
                    {

                        return Ok(new { message = $"We sent a link to your email {email}", status = true });
                    }
                    else
                    {
                        return BadRequest(new { message = "Error  while sending message please try again", status = true });
                    }





                }
                catch (Exception ex)
                {
                    return BadRequest("Error->" + ex);
                }


            }
   
            [HttpGet("GetAllCountries")]
            public IActionResult GetAllCountries()
            {
                try
                {
                    IEnumerable<Country> allCountry = _userServices.GetAllCountry();
                    return Ok(allCountry);
                }
                catch (Exception ex) { throw new Exception($"Server Error --> {ex}"); }


            }

            [HttpPost("AddCart")]
            public IActionResult AddCart(int userId, int carId)
            {
                _userServices.AddCart(userId, carId);
                return Ok(new { message = "Cart Added", status = true });
            }

            [HttpPost("SentMessage")]
            public IActionResult SentMessage(Message message)
            {
                _userServices.SentMessage(message);
                return Ok(new { message = "message sent", status = true });
            }

            [HttpGet("GetUserMessages")]
            public IActionResult GetUserMessages(int firstUserId, int secondUserId)
            {
                IEnumerable<MessageResponse> requestOrders = _userServices.GetUserMessages(firstUserId, secondUserId);
                return Ok(requestOrders);
            }



            [HttpGet("GetEmailsYouContact")]
            public IActionResult GetEmailsYouContact(int yourId)
            {
                IEnumerable<UserMessagesEmail> requestOrders = _userServices.GetMessageEmails(yourId);
                return Ok(requestOrders);
            }

            [HttpPatch("RemoveMessageFromUserYouChatWith")]
            public IActionResult RemoveMessagesFromUserYouChatWith(int yourId, int userId)
            {
                _userServices.RemoveAllMessagesFromUser(yourId, userId);

                return Ok(new { message = "Removed", status = true });
            }

            [HttpPost("ReadAllMessageFromUser")]
            public IActionResult ReadAllMessageFromUser(int yourId, int userId)
            {
                _userServices.ReadAllMessage(yourId, userId);

                return Ok(new { message = "MessageReads", status = true });
            }

            [HttpPatch("EditUser")]
            public async Task<IActionResult> EditUser(EditUserDto newUserInfo)
            {


                var oldUserInfo = _userServices.GetUserById(newUserInfo.UserID);
                if (oldUserInfo == null)
                {
                    return Ok(new { message = "user not exist", status = false });
                }
                if (!_validation.IsValidPhoneNumber(newUserInfo.PhoneNumber))
                {
                    return Ok(new { message = "Invalid phone number", status = false });

                }
                if (!_validation.IsValidEmail(newUserInfo.Email))
                {
                    return Ok(new { message = "Invalid new Email", status = false });
                }
                _userServices.EditUser(newUserInfo, oldUserInfo);
                if (string.IsNullOrEmpty(newUserInfo.Email) || newUserInfo.Email.Trim() == oldUserInfo.Email.Trim())
                {

                    return Ok(new { message = "your profile has been Edited", status = true });
                }
                else
                {
                    EmailService emailService = new();
                    var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                    var tokenService = new TokenService(config);
                    var token = tokenService.GenerateToken(newUserInfo.Email);
                    var updateEmailLink = $"{ApiSetting.urlWithoutApi}Email/UpdateEmail?token={token}&id={newUserInfo.UserID}";
                    EmailRequest emailRequest = new()
                    {
                        To = newUserInfo.Email,
                        Body = $"<p>if you click on the Link your email account will updated </p> <a href='{updateEmailLink}'>..UPDATE HERE/> ",
                        Subject = "Update Email"

                    };
                    await emailService.SendEmailAsync(emailRequest);
                    return Ok(new { message = $"profile edited and we sent a message to {newUserInfo.Email} to update your email", status = true });
                }

            }








        }
    }

}





