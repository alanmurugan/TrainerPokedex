using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainerPokedex.Shared.Models.Pokemon;

namespace TrainerPokedex.Server.Services.PokemonServices
{
    public interface IPokemonService
    {
        Task<bool> CreatePokemonAsync(PokemonCreate model);
        Task<IEnumerable<PokemonListItem>> GetAllPokemonAsync();
        Task<PokemonDetail> GetPokemonByIdAsync(int pokemonId);
        Task<bool> UpdatePokemonAsync(PokemonEdit model);
        Task<bool> DeletePokemonAsync(int pokemonId);
        Task<bool> AddPokemonToRegionAsync(int pokemonId, PokemonRegion request);
        Task<bool> AddTypeToPokemonAsync(int pokemonId, PokemonType request);
        Task<bool> AddMoveToPokemonAsync(int pokemonId, PokemonMove request);
        void SetUserId(string userId);
    }
}