using GabrielesProject.AdformExam.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GabrielesProject.AdformExam.WebAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IExternalUserService _externalUserService;
        private readonly IOrdersService _ordersService;

        public UsersController(IExternalUserService externalUserService, IOrdersService ordersService)
        {
            _externalUserService = externalUserService;
            _ordersService = ordersService;
        }

        [HttpGet("{userId}/orders")]
        public async Task<IActionResult> GetOrdersForUser(int userId)
        {
            var userOrders = await _ordersService.GetOrdersByUserIdAsync(userId);
            return Ok(userOrders);
        }
    }
}
