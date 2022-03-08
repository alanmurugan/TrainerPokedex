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
           foreach(var moveId in model.MoveIds)
           {
               var moveEntity = await _context
                   .Moves
                   .Include(p => p.TeachablePokemon)
                   .FirstOrDefaultAsync(m => m.Id == moveId);
               pokemonEntity.Moves.Add(moveEntity);
           }
           foreach(var typeId in model.TypeIds) 
           {
               var typeEntity = await _context
                   .Types
                   .Include(p => p.Pokemon)
                   .FirstOrDefaultAsync(t => t.Id == typeId);
               pokemonEntity.Types.Add(typeEntity);
           }
           foreach(var regionId in model.RegionIds)
           {
               var regionEntity = await _context
                   .Regions
                   .Include(p => p.LocalPokemon)
                   .FirstOrDefaultAsync(r => r.Id == regionId);
               pokemonEntity.RegionsFound.Add(regionEntity);
           }
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
                Name = pokemonEntity.Name,
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

        public async Task<bool> AddPokemonToRegionAsync(int pokemonId, PokemonRegion request)
        {
            var pokemonEntity = await _context
                .Pokemon
                .FindAsync(pokemonId);
            var regionEntity = await _context
                .Regions
                .Include(p => p.LocalPokemon)
                .FirstOrDefaultAsync(r => r.Id == request.RegionId);

            if (request.PokemonId == pokemonId)
            {
                regionEntity.LocalPokemon.Add(pokemonEntity);
                return await _context.SaveChangesAsync() == 1;
            }

            return false;
        }

        public async Task<bool> AddTypeToPokemonAsync(int pokemonId, PokemonType request)
        {
            var pokemonEntity = await _context
                .Pokemon
                .FindAsync(pokemonId);
            var typeEntity = await _context
                .Types
                .Include(p => p.Pokemon)
                .FirstOrDefaultAsync(t => t.Id == request.TypeId);

            if (request.PokemonId == pokemonId)
            {
                typeEntity.Pokemon.Add(pokemonEntity);
                return await _context.SaveChangesAsync() == 1;
            }

            return false;
        }

        public async Task<bool> AddMoveToPokemonAsync(int pokemonId, PokemonMove request)
        {
            var pokemonEntity = await _context
                .Pokemon
                .FindAsync(pokemonId);
            var moveEntity = await _context
                .Moves
                .Include(p => p.TeachablePokemon)
                .FirstOrDefaultAsync(m => m.Id == request.MoveId);

            if (request.PokemonId == pokemonId)
            {
                moveEntity.TeachablePokemon.Add(pokemonEntity);
                return await _context.SaveChangesAsync() == 1;
            }

            return false;
        }
    }
}