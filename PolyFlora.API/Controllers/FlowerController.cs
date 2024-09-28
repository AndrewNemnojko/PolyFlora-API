
using Microsoft.AspNetCore.Mvc;
using PolyFlora.Application.DTOs.Flower;
using PolyFlora.Application.Services.Domain;

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
        [Route("/summary")]
        public async Task<IActionResult> GetFlowersSummaryAsync(uint page, CancellationToken ct)
        {
            var result = await _flowerService.GetFlowersAsync<FlowerSummary>(page, ct);
            return Ok(result);
        }

        [HttpGet]
        [Route("/name/{name}")]
        public async Task<IActionResult> GetFlowerByNameAsync(string name, CancellationToken ct)
        {
            var result = await _flowerService.GetFlowerByNameAsync(name, ct);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("/id/{id}")]
        public async Task<IActionResult> GetFlowerByNameAsync(Guid id, CancellationToken ct)
        {
            var result = await _flowerService.GetFlowerByIdAsync(id, ct);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("/")]
        public async Task<IActionResult> CreateFlowerAsync([FromForm]FlowerCreateRequest request)
        {
            var result = await _flowerService.CreateFlowerAsync(request);
            return Ok(result);
        }
    }
}
