
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


        [HttpGet("GetMyOrderSale")]
        public IActionResult GetMyOrderSale(int userId)
        {
            IEnumerable<OrderSaleResponse> orderSales = _salesOrderService.GetMyOrderSale(userId);
            if (!orderSales.Any())
            {
                return Ok(new { message = "No vehicle request", status = false });
            }
            return Ok(orderSales);

        }

        [HttpGet("GetMyRequestVehiclesId")]
        public IActionResult GetMyRequestVehiclesId(int userId)
        {
            IEnumerable<int> VehiclesId = _salesOrderService.GetMyRequestVehicleId(userId);

            return Ok(VehiclesId);

        }

        [HttpGet("GetMyRequestedVehicles")]
        public IActionResult GetMyRequestedVehicles(int userId)
        {
            IEnumerable<OrderSaleResponse> MyRequestedVehicles = _salesOrderService.GetMyRequestedVehicle(userId);

            return Ok(MyRequestedVehicles);

        }
        


              
        [HttpPatch("UpdateRequestSale")]
        public IActionResult UpdateRequestSale(short status,int saleOrderId)
        {
            try
            {
                if (saleOrderId == 0 )
                { 
                     return Ok(new { message = "You need to fill exist information", status = true });
                }
                _salesOrderService.UpdateRequested(status, saleOrderId);
           
                return Ok(new { message = "Request status updated", status = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server Error -->" + ex, status = false });
            }


        }

    


    }
}