namespace CarRentalSale.dtos
{
    public class Message
    {
     
        public required int FromId { get; set; }
        public required int ToId { get; set; }
        public required string MessageContent{get;set;}
        

    }
}