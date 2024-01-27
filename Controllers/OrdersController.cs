using OrdersModel = mongodb_dotnet_example.Models.Orders;
using OrdersService = mongodb_dotnet_example.Services.OrdersService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace mongodb_dotnet_example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersService _ordersService;

        public OrdersController(OrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        public ActionResult<List<OrdersModel>> Get()
        {
            var orders = _ordersService.Get();
            return Ok(orders);
        }

[HttpGet("{id:length(24)}", Name = "GetOrderById")]
public ActionResult<OrdersModel> Get(string id)
{
    var orders = _ordersService.Get(id);

    if (orders == null)
    {
        return NotFound();
    }

    return Ok(orders);
}



[HttpPost]
public ActionResult<OrdersModel> Create(OrdersModel orders)
{
    _ordersService.Create(orders);

    return CreatedAtRoute("GetOrderById", new { id = orders.Id.ToString() }, orders);
}


        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, OrdersModel ordersIn)
        {
            var orders = _ordersService.Get(id);

            if (orders == null)
            {
                return NotFound();
            }

            _ordersService.Update(id, ordersIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var orders = _ordersService.Get(id);

            if (orders == null)
            {
                return NotFound();
            }

            _ordersService.Delete(id);

            return NoContent();
        }
    }
}
