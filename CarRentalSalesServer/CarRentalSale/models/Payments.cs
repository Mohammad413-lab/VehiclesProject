namespace CarRentalSale.models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }

        public int PaymentStatus { get; set; }
        public int PaymentType { get; set; }
        public int? InstallmentPlane { get; set; }
        public decimal Amount { get; set; }



    }
}