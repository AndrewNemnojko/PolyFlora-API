
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolyFlora.Application.DTOs.Flower;
using PolyFlora.Application.Services.Domain;
using PolyFlora.Core.Enums;

namespace PolyFlora.API.Controllers
{
    [ApiController]
    [Route("api/flowers")]
    public class FlowerController : ControllerBase
    {
        private readonly FlowerService _flowerService;
        public FlowerController(FlowerService flowerService)
        {
            _flowerService = flowerService;
        }     

        [HttpGet]       
        [Route("catalog/{lang}/page/{page:int}")]
        public async Task<IActionResult> GetFlowersPaginatedAsync(CancellationToken ct, string lang = "en", int page = 1, int size = 50)
        {
            var result = await _flowerService
                .GetFlowersWithPaginationAsync<FlowerSummary>(page, size, lang, ct);
            return Ok(result);
        }

        [HttpGet]
        [Route("name/{lang}/{name}")]
        public async Task<IActionResult> GetFlowerByNameAsync(string name, CancellationToken ct, string lang = "en")
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name cannot be empty.");
            }
            var result = await _flowerService.GetFlowerByNameAsync(name, lang, ct);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("id/{lang}/{id:guid}")]
        public async Task<IActionResult> GetFlowerByIdAsync(Guid id, CancellationToken ct, string lang = "en")
        {
            var result = await _flowerService.GetFlowerByIdAsync(id, lang, ct);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        //[Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> CreateFlowerAsync([FromBody] FlowerRequest request)
        {
            var result = await _flowerService.CreateFlowerAsync(request);
            return Ok(result);            
        }
        

        [HttpPut]
        [Route("{id:guid}")]
        //[Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> ChangeFlowerAsync(Guid id, [FromBody] FlowerRequest request)
        {
            var result = await _flowerService.ChangeFlowerAsync(id, request);
            return Ok(result);
        }
    }
}
