

using CarRentalSale.dtos;
using CarRentalSale.dtos.response;

namespace CarRentalSale.interfacee
{
    public interface ISalesOrderService
    {

        public void AddSalesOrder(CreateSaleOrder saleOrder);
        public IEnumerable<OrderSaleResponse> GetMyOrderSale(int userId);




    }
}