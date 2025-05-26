using Microsoft.AspNetCore.Mvc;
using CarRentalSale.dtos;
using CarRentalSale.services;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;
using CarRentalSale.datarepositories;
using CarRentalSale.validationservices;
using CarRentalSale.interfacee;
using CarRentalSale.helper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace CarRentalSale.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IConfiguration configuration, IValidationService validationService, IUsersServices usersServices) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;

        private IValidationService _validationServices = validationService;
        private IUsersServices _userServices = usersServices;



        [HttpPost("Signup")]
        public IActionResult Signup([FromBody] SignUpDtos user)
        {

            if (!_validationServices.IsValidEmail(user.Email))
            {
                return BadRequest(new { message = "Invalid email format", status = false });
            }
            if (!_validationServices.IsValidPassword(user.Password))
            {
                return BadRequest(new { message = "Password must contain 8 characters, an uppercase letter, a lowercase letter, and a number.", status = false });
            }
            if (!_validationServices.IsValidPhoneNumber(user.PhoneNumber))
            {
                return BadRequest(new { message = "Invalid phone number format.", status = false });
            }
            if (_userServices.IsUserEmailExist(user.Email))
            {
                return BadRequest(new { message = "email is already exist", status = false });
            }
            PasswordEncryption passwordEncryption = new PasswordEncryption
            {
                Password = user.Password,
                PasswordSalt = null
            };
            passwordEncryption.EncryptionPass();

            Console.WriteLine("Salt: " + passwordEncryption.PasswordSalt);
            Console.WriteLine("Hash: " + passwordEncryption.Password);

            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Users (FirstName, LastName, Email, PasswordHash, PasswordSalt,PhoneNumber,Address,AuthId,CountryId) VALUES (@fn, @ln, @em, @ph, @ps,@pn,@ad,@au,@co)", connection);
                command.Parameters.AddWithValue("@fn", user.FirstName);
                command.Parameters.AddWithValue("@ln", user.LastName);
                command.Parameters.AddWithValue("@em", user.Email);
                command.Parameters.AddWithValue("@ph", passwordEncryption.Password);
                command.Parameters.AddWithValue("@ps", passwordEncryption.PasswordSalt);
                command.Parameters.AddWithValue("@pn", user.PhoneNumber);
                command.Parameters.AddWithValue("@ad", user.Address);
                command.Parameters.AddWithValue("@au", 1);
                command.Parameters.AddWithValue("@co", user.Countryid);
                command.ExecuteNonQuery();
            }

            return Ok(new { message = "Sign up successfully", status = true });
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
           
                var checkEmailAndPassword = UsersRepository.UserLogin(dto.Email, dto.Password);

                if (checkEmailAndPassword)
                {
                    var tokenService = new TokenService(_configuration);
                    var token = tokenService.GenerateToken(dto.Email);

                    return Ok(new { token });
                }
                return Unauthorized(new { message = "Invalid email or password" });
            


        }


        [HttpPost("EmailVerified")]
        public async Task<IActionResult> EmailVerified(string email)
        {
            if (!_userServices.IsUserEmailExist(email))
            {
                return Ok(new { message = $"email {email} not found", status = false });
            }
            TokenService tokenService = new TokenService(_configuration);
            var token = tokenService.GenerateToken(email);

            var verificationLink = $"https://localhost:5051/Email/VerifiedUserEmail?token={token}";
            EmailRequest emailRequest = new EmailRequest
            {
                To = email,
                Body = $"<p>Click on the link </p><a href='{verificationLink}'>Click here to verified your email/> ",
                Subject = "Verified your email"
            };

            EmailService emailService = new EmailService();
            bool isEmailSent = await emailService.SendEmailAsync(emailRequest);
            if (isEmailSent)
            {
                return Ok(new { message = $"Verification link sent to the email {email}.", status = true });
            }



            return Ok(new { message = "Failed to sent the link.", status = false });
        }
    }
}