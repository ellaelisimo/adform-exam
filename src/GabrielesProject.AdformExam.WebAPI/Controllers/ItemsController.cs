using GabrielesProject.AdformExam.Application.DTOs;
using GabrielesProject.AdformExam.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GabrielesProject.AdformExam.WebAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("v1/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemsService;

        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _itemsService.GetItemsAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ItemDTO item)
        {
            var itemAdd = await _itemsService.AddItemAsync(item);
            return Ok(itemAdd);
        }
    }
}
