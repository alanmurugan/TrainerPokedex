using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainerPokedex.Server.Services.MoveServices;
using TrainerPokedex.Shared.Models.Move;

namespace TrainerPokedex.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoveController : ControllerBase
    {
        private readonly IMoveService _moveService;
        public MoveController(IMoveService moveService) => _moveService = moveService;

        private string GetUserId()
        {
            var userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            return userIdClaim ?? null;
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null) return false;
            _moveService.SetUserId(userId);
            return true;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!SetUserIdInService()) return Unauthorized();
            var move = await _moveService.GetAllMoveAsync();
            return Ok(move.ToList());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Move(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var move = await _moveService.GetMoveByIdAsync(id);
            if (move == null) return NotFound();
            return Ok(move);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create(MoveCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (!ModelState.IsValid || model is null) return BadRequest();

            var wasSuccessful = await _moveService.CreateMoveAsync(model);

            if (wasSuccessful) return Ok();
            return BadRequest();
        }

        //PUT
        [HttpPut("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, MoveEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (!ModelState.IsValid || model is null) return BadRequest();
            if (model.Id != id) return BadRequest();

            var wasSuccessful = await _moveService.UpdateMoveAsync(model);

            if (wasSuccessful) return Ok();
            return BadRequest();
        }

        //DELETE
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var move = await _moveService.GetMoveByIdAsync(id);
            if (move == null) return NotFound();

            var wasSuccessful = await _moveService.DeleteMoveAsync(id);

            if (!wasSuccessful) return BadRequest();
            return Ok();
        }
    }
}