using System.Threading.Tasks;
using CarRentalSale.datarepositories;
using CarRentalSale.dtos;
using CarRentalSale.dtos.response;
using CarRentalSale.interfacee;
using CarRentalSale.models;
using CarRentalSale.request;
using CarRentalSale.request.dtos;
using CarRentalSale.response;

namespace CarRentalSale.services
{
    public class UsersService : IUsersServices
    {


        public int ChangePassword(ChangePasswordDtos changePassword)
        {
            return UsersRepository.ChangeUserPassword(changePassword);
        }

        public void DeleteUser(int id)
        {
            UsersRepository.DeleteUserById(id);
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            return UsersRepository.GetAllUsers();
        }

    
        public IEnumerable<Sales> GetUserSalesById(int customerId)
        {
            return UsersRepository.GetUserSalesById(customerId);
        }

        public UserDto? GetUserById(int id)
        {
            return UsersRepository.GetUserById(id);
        }

        public IEnumerable<Rental> GetRentalHistory(int customerId)
        {
            return UsersRepository.GetUserRentals(customerId);
        }

        public async Task<bool> RecoveryPassword(EmailRequest emailRequest)
        {
            try
            {
                EmailService emailService = new EmailService();

                if(await emailService.SendEmailAsync(emailRequest)){
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"error -> {ex}");
            }
            return false;

        }

        public void UpdateUser(UserDto user)
        {
            UsersRepository.UpdateUserInformation(user);
        }

    

        public bool IsUserEmailExist(string email)
        {
            return UsersRepository.IsUserEmailExist(email);
        }

        public IEnumerable<Country> GetAllCountry()
        {
            return UsersRepository.GetAllCountry();
        }

        public UserDto? GetUserByEmail(string email)
        {
           return UsersRepository.GetUserByEmail(email);
        }

        public void AddCart(int userId, int carId)
        {
            UsersRepository.AddCart(userId,carId);
        }

        public void SentMessage(Message message)
        {
            UsersRepository.SentMessage(message);
        }

        public IEnumerable<MessageResponse> GetUserMessages(int firstUserId, int secondUserId)
        {
            return UsersRepository.GetUserMessages(firstUserId,secondUserId);
        }

        public IEnumerable<UserMessagesEmail> GetMessageEmails(int userId)
        {
            return UsersRepository.GetMessageEmails(userId);
        }

        public void RemoveAllMessagesFromUser(int userId, int userChatWithId)
        {
           UsersRepository.RemoveAllMessagesFromUser(userId,userChatWithId);
        }

        public void ReadAllMessage(int userId, int userChatWithId)
        {
           UsersRepository.ReadAllMessage(userId,userChatWithId);
        }

        public void EditUser(EditUserDto newUserInfo, UserDto oldUserInfo)
        {
               UsersRepository.EditUser(newUserInfo, oldUserInfo);
        }
    }

}