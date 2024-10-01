
using Microsoft.AspNetCore.Authorization;
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
        [Route("catalog/page/{page:int}")]
        public async Task<IActionResult> GetFlowersPaginatedAsync(CancellationToken ct, int page = 1, int size = 50)
        {
            var result = await _flowerService
                .GetFlowersWithPaginationAsync<FlowerSummary>(page, size, ct);
            return Ok(result);
        }

        [HttpGet]
        [Route("name/{name}")]
        public async Task<IActionResult> GetFlowerByNameAsync(string name, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name cannot be empty.");
            }
            var result = await _flowerService.GetFlowerByNameAsync(name, ct);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("id/{id:guid}")]
        public async Task<IActionResult> GetFlowerByNameAsync(Guid id, CancellationToken ct)
        {
            var result = await _flowerService.GetFlowerByIdAsync(id, ct);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> CreateFlowerAsync([FromForm] FlowerRequest request)
        {
            var result = await _flowerService.CreateFlowerAsync(request);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> ChangeFlowerAsync(Guid id, [FromForm] FlowerRequest request)
        {
            var result = await _flowerService.ChangeFlowerAsync(id, request);
            return Ok(result);
        }
    }
}
