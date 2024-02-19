using GabrielesProject.AdformExam.Application.DTOs;
using GabrielesProject.AdformExam.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GabrielesProject.AdformExam.WebAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderDTO>> Get()
        {
            return await _ordersService.GetOrdersAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderDTO order)
        {
            int orderAdd = await _ordersService.AddOrder(order);
            return Ok(orderAdd);
        }

        [HttpPut("{id}")]
        [Route("/complete")]
        public async Task<IActionResult> Update([FromQuery] int id, [FromQuery] string orderStatus)
        {
            var order = await _ordersService.UpdateOrder(id, orderStatus);
            return Ok(order);

        }
    }
}
