using System.Text.RegularExpressions;
using CarRentalSale.interfacee;

namespace CarRentalSale.validationservices
{

    public class ValidationService : IValidationService
    {
        public bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            var phonePattern = @"^(\+?\(?\d{1,4}\)?|\d{1})\d{7,15}$";
            return Regex.IsMatch(phoneNumber, phonePattern);
        }

        public bool IsValidPassword(string password)
        {

            return password.Length >= 8 &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsDigit);
        }
    }

}