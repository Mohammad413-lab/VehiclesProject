namespace CarRentalSale.request.dtos{
   public class ChangePasswordDtos
{
    public required int UserID{get;set;}
    public required string OldPassword { get; set;}
    public required string NewPassword { get; set;}

}
}