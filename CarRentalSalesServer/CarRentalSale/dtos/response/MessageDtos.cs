using CarRentalSale.dtos.response;

namespace CarRentalSale.dtos
{
    public class MessageResponse
    {
        public int ChatId { get; set; }

        public UserInfoMessage FromUser { get; set; }
        public UserInfoMessage ToUser { get; set; }

        public string Message { get; set; }
        public DateTime DateSent { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeletedFrom { get; set; }
        public bool IsDeletedTo { get; set; }
    }
}