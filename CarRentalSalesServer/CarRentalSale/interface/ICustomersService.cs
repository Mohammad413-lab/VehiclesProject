
using System.ComponentModel;
using CarRentalSale.dtos;
using CarRentalSale.dtos.response;
using CarRentalSale.models;
using CarRentalSale.request;
using CarRentalSale.request.dtos;
using CarRentalSale.response;

namespace CarRentalSale.interfacee
{
    public interface IUsersServices
    {
        public IEnumerable<UserDto> GetAllUsers();
        public UserDto? GetUserById(int id);
        public void UpdateUser(UserDto updatedData);
        public void DeleteUser(int id);
        public int ChangePassword(ChangePasswordDtos changePassword);

        public IEnumerable<Rental> GetRentalHistory(int userId);

        public Task<bool> RecoveryPassword(EmailRequest emailRequest);

        IEnumerable<Sales> GetUserSalesById(int userId);

        public bool IsUserEmailExist(string email);

        public IEnumerable<Country> GetAllCountry();

        public UserDto? GetUserByEmail(string email);

        public void AddCart(int userId, int carId);

        public void SentMessage(Message message);
        public IEnumerable<MessageResponse> GetUserMessages(int firstUserId, int secondUserId);

        public IEnumerable<UserMessagesEmail> GetMessageEmails(int userId);

        public void RemoveAllMessagesFromUser(int userId, int userChatWithId);

        public void ReadAllMessage(int userId, int userChatWithId);
        public void EditUser(EditUserDto newUserInfo, UserDto oldUserInfo);

    }
}