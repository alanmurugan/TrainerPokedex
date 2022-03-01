using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainerPokedex.Server.Services.PokemonServices;
using TrainerPokedex.Shared.Models.Pokemon;

namespace TrainerPokedex.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        public PokemonController(IPokemonService pokemonService) => _pokemonService = pokemonService;
        
        private string GetUserId()
        {
            var userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            return userIdClaim ?? null;
        }
        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null) return false;
            _pokemonService.SetUserId(userId);
            return true;
        }
        // GET
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!SetUserIdInService()) return Unauthorized();
            var pokemon = await _pokemonService.GetAllPokemonAsync();
            return Ok(pokemon.ToList());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Pokemon(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
            if (pokemon == null) return NotFound();
            return Ok(pokemon);
        }
        
        // POST
        [HttpPost]
        public async Task<IActionResult> Create(PokemonCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (!ModelState.IsValid || model is null) return BadRequest();
            
            var wasSuccessful = await _pokemonService.CreatePokemonAsync(model);
            
            if (wasSuccessful) return Ok();
            return BadRequest();
        }
        
        //PUT
        [HttpPut("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, PokemonEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (!ModelState.IsValid || model is null) return BadRequest();
            if (model.Id != id) return BadRequest();

            var wasSuccessful = await _pokemonService.UpdatePokemonAsync(model);

            if (wasSuccessful) return Ok();
            return BadRequest();
        }
        
        //DELETE
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
            if (pokemon == null) return NotFound();
            
            var wasSuccessful = await _pokemonService.DeletePokemonAsync(id);
            
            if (!wasSuccessful) return BadRequest();
            return Ok();
        }
    }
}