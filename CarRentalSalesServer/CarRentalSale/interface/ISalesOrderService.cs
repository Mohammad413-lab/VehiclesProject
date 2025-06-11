

using CarRentalSale.dtos;
using CarRentalSale.dtos.response;

namespace CarRentalSale.interfacee
{
    public interface ISalesOrderService
    {

        public void AddSalesOrder(CreateSaleOrder saleOrder);
        public IEnumerable<OrderSaleResponse> GetMyOrderSale(int userId);

        public IEnumerable<int> GetMyRequestVehicleId(int userId);

        public IEnumerable<OrderSaleResponse> GetMyRequestedVehicle(int userId);
        public void UpdateRequested(short status, int saleOrderId);



        void RemoveRequest(int salesOrderId);




    }
}