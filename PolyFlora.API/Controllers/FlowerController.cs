
using Microsoft.AspNetCore.Mvc;
using PolyFlora.Application.Services;

namespace PolyFlora.API.Controllers
{
    [ApiController]
    [Route("flowers")]
    public class FlowerController : ControllerBase
    {
        private readonly FlowerService _flowerService;
        public FlowerController(FlowerService flowerService)
        {
            _flowerService = flowerService;
        }

        [HttpGet]
        [Route("/name/{name}")]
        public async Task<IActionResult> GetFlowerByNameAsync(string name, CancellationToken ct)
        {
            var result = await _flowerService.GetFlowerByNameAsync(name, ct);
            return Ok(result);
        }

        [HttpGet]
        [Route("/id/{id}")]
        public async Task<IActionResult> GetFlowerByNameAsync(Guid id, CancellationToken ct)
        {
            var result = await _flowerService.GetFlowerByIdAsync(id, ct);
            return Ok(result);
        }
    }
}
