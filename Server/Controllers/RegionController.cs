using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainerPokedex.Server.Services.RegionServices;
using TrainerPokedex.Shared.Models.Region;

namespace TrainerPokedex.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;
        public RegionController(IRegionService regionService) => _regionService = regionService;

        private string GetUserId()
        {
            var userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            return userIdClaim ?? null;
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null) return false;
            _regionService.SetUserId(userId);
            return true;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!SetUserIdInService()) return Unauthorized();
            var region = await _regionService.GetAllRegionsAsync();
            return Ok(region.ToList());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Region(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var region = await _regionService.GetRegionByIdAsync(id);
            if (region == null) return NotFound();
            return Ok(region);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create(RegionCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (!ModelState.IsValid || model is null) return BadRequest();

            var wasSuccessful = await _regionService.CreateRegionAsync(model);

            if (wasSuccessful) return Ok();
            return BadRequest();
        }

        //PUT
        [HttpPut("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, RegionEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (!ModelState.IsValid || model is null) return BadRequest();
            if (model.Id != id) return BadRequest();

            var wasSuccessful = await _regionService.UpdateRegionAsync(model);

            if (wasSuccessful) return Ok();
            return BadRequest();
        }

        //DELETE
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var region = await _regionService.GetRegionByIdAsync(id);
            if (region == null) return NotFound();

            var wasSuccessful = await _regionService.DeleteRegionAsync(id);

            if (!wasSuccessful) return BadRequest();
            return Ok();
        }
    }
}