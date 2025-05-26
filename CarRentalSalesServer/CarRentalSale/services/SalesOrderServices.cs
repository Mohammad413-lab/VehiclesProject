

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
    }

}