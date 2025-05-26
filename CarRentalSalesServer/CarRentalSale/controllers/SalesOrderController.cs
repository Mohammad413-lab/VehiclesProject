
using CarRentalSale.dtos;
using CarRentalSale.dtos.response;
using CarRentalSale.interfacee;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSale.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesOrderController(ISalesOrderService salesOrderService) : ControllerBase
    {
        private ISalesOrderService _salesOrderService = salesOrderService;

        [HttpPost("AddSaleOrder")]
        public IActionResult AddSaleOrder([FromBody] CreateSaleOrder saleOrder)
        {
            _salesOrderService.AddSalesOrder(saleOrder);
            return Ok(new { message = "Sale order added for this vehicle", status = true });

        }


        [HttpPost("GetMyOrderSale")]
        public IActionResult GetMyOrderSale(int userId)
        {
            IEnumerable<OrderSaleResponse> orderSales = _salesOrderService.GetMyOrderSale(userId);
            if (!orderSales.Any())
            { 
                 return Ok(new { message = "No vehicle request", status = false });
            }
            return Ok(orderSales);

        }


    }
}