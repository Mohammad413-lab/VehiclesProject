

using CarRentalSale.datarepositories;
using CarRentalSale.dtos;
using CarRentalSale.dtos.response;
using CarRentalSale.interfacee;

namespace CarRentalSale.services
{

    public class SalesOrderServices : ISalesOrderService
    {
        public void AddSalesOrder(CreateSaleOrder saleOrder)
        {
            SalesOrderRepository.AddSalesOrder(saleOrder);
        }

        public IEnumerable<OrderSaleResponse> GetMyOrderSale(int userId)
        {
          return  SalesOrderRepository.GetMyOrderSale(userId);
        }

        public IEnumerable<OrderSaleResponse> GetMyRequestedVehicle(int userId)
        {
            return SalesOrderRepository.GetMyRequestedVehicle(userId);
        }

        public IEnumerable<int> GetMyRequestVehicleId(int userId)
        {
            return SalesOrderRepository.GetMyRequestVehicleId(userId);
        }

        public void RemoveRequest(int salesOrderId)
        {
            SalesOrderRepository.RemoveRequest(salesOrderId);
        }

        public void UpdateRequested(short status, int saleOrderId)
        {
            SalesOrderRepository.UpdateRequested(status, saleOrderId);
        }
    }

}