using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainerPokedex.Server.Services.TypeServices;
using TrainerPokedex.Shared.Models.Type;

namespace TrainerPokedex.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _typeService;
        public TypeController(ITypeService typeService) => _typeService = typeService;

        private string GetUserId()
        {
            var userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            return userIdClaim ?? null;
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null) return false;
            _typeService.SetUserId(userId);
            return true;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!SetUserIdInService()) return Unauthorized();
            var type = await _typeService.GetAllTypeAsync();
            return Ok(type.ToList());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Type(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var type = await _typeService.GetTypeByIdAsync(id);
            if (type == null) return NotFound();
            return Ok(type);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create(TypeCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (!ModelState.IsValid || model is null) return BadRequest();

            var wasSuccessful = await _typeService.CreateTypeAsync(model);

            if (wasSuccessful) return Ok();
            return BadRequest();
        }

        //PUT
        [HttpPut("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, TypeEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (!ModelState.IsValid || model is null) return BadRequest();
            if (model.Id != id) return BadRequest();

            var wasSuccessful = await _typeService.UpdateTypeAsync(model);

            if (wasSuccessful) return Ok();
            return BadRequest();
        }

        //DELETE
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var type = await _typeService.GetTypeByIdAsync(id);
            if (type == null) return NotFound();

            var wasSuccessful = await _typeService.DeleteTypeAsync(id);

            if (!wasSuccessful) return BadRequest();
            return Ok();
        }
    }
}