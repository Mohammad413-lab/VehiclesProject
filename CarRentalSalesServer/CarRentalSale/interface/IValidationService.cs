
namespace CarRentalSale.interfacee
{
    public interface IValidationService
    {
        bool IsValidEmail(string email);
        bool IsValidPassword(string password);
        bool IsValidPhoneNumber(string phoneNumber);
    }
}

