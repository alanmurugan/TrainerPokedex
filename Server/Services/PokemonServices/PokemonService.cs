using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainerPokedex.Server.Data;
using TrainerPokedex.Server.Models;
using TrainerPokedex.Shared.Models.Move;
using TrainerPokedex.Shared.Models.Pokemon;
using TrainerPokedex.Shared.Models.Region;
using TrainerPokedex.Shared.Models.Type;

namespace TrainerPokedex.Server.Services.PokemonServices
{
    public class PokemonService : IPokemonService
    {
        private readonly ApplicationDbContext _context;
        public PokemonService(ApplicationDbContext context) => _context = context;
        
        private string _userId;
        public void SetUserId(string userId) => _userId = userId;
        
        public async Task<bool> CreatePokemonAsync(PokemonCreate model)
        {
            var pokemonEntity = new Pokemon
            {
                DexNumber = model.DexNumber,
                Name = model.Name,
                ImgUrl = model.ImgUrl,
                Generation = model.Generation,
                DexEntry = model.DexEntry
            };
            _context.Pokemon.Add(pokemonEntity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<PokemonListItem>> GetAllPokemonAsync()
        {
            var pokemonQuery = _context.Pokemon
                .Select(p => new PokemonListItem
                {
                    Id = p.Id,
                    DexNumber = p.DexNumber,
                    Name = p.Name
                });
            return await pokemonQuery.ToListAsync();
        }

        public async Task<PokemonDetail> GetPokemonByIdAsync(int pokemonId)
        {
            var pokemonEntity = await _context
                .Pokemon
                .Include(t => t.Types)
                .Include(r => r.RegionsFound)
                .Include(m => m.Moves)
                .FirstOrDefaultAsync(p => p.Id == pokemonId);
            if (pokemonEntity is null) return null;
            var detail = new PokemonDetail
            {
                Id = pokemonEntity.Id,
                DexEntry = pokemonEntity.DexEntry,
                DexNumber = pokemonEntity.DexNumber,
                Generation = pokemonEntity.Generation,
                ImgUrl = pokemonEntity.ImgUrl,
                Types = pokemonEntity.Types
                    .Select(t => new TypeListItem
                    {
                        Name = t.Name
                    }).ToList(),
                Moves = pokemonEntity.Moves
                    .Select(m => new MoveListItem
                    {
                        Name = m.Name
                    }).ToList(),
                RegionsFound = pokemonEntity.RegionsFound
                    .Select(r => new RegionListItem
                    {
                        Name = r.Name
                    }).ToList()
            };
            return detail;
        }

        public async Task<bool> UpdatePokemonAsync(PokemonEdit model)
        {
            if (model == null) return false;
            var entity = await _context.Pokemon.FindAsync(model.Id);

            entity.Name = (model.Name ?? entity.Name);
            entity.DexNumber = (model.DexNumber ?? entity.DexNumber);
            entity.ImgUrl = (model.ImgUrl ?? entity.ImgUrl);
            entity.Generation = (model.Generation ?? entity.Generation);
            entity.DexEntry = (model.DexEntry ?? entity.DexEntry);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeletePokemonAsync(int pokemonId)
        {
            var entity = await _context.Pokemon.FindAsync(pokemonId);
            _context.Pokemon.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }
    }
}