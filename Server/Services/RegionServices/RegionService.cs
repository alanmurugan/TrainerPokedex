using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainerPokedex.Server.Data;
using TrainerPokedex.Server.Models;
using TrainerPokedex.Shared.Models.Pokemon;
using TrainerPokedex.Shared.Models.Region;

namespace TrainerPokedex.Server.Services.RegionServices
{
    public class RegionService : IRegionService
    {
        private readonly ApplicationDbContext _context;
        public RegionService(ApplicationDbContext context) => _context = context;
        
        private string _userId;
        public void SetUserId(string userId) => _userId = userId;
        
        public async Task<bool> CreateRegionAsync(RegionCreate model)
        {
            var regionEntity = new Region
            {
                Name = model.Name,
                GenIntroduced = model.GenIntroduced
            };
            _context.Regions.Add(regionEntity);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<RegionListItem>> GetAllRegionsAsync()
        {
            var regionQuery = _context.Regions
                .Select(r => new RegionListItem
                {
                    Name = r.Name
                });
            return await regionQuery.ToListAsync();
        }

        public async Task<RegionDetail> GetRegionByIdAsync(int regionId)
        {
            var regionEntity = await _context
                .Regions
                .Include(p => p.LocalPokemon)
                .FirstOrDefaultAsync(r => r.Id == regionId);
            return regionEntity is null
                ? null
                : new RegionDetail
                {
                    Id = regionEntity.Id,
                    Name = regionEntity.Name,
                    GenIntroduced = regionEntity.GenIntroduced,
                    LocalPokemon = regionEntity.LocalPokemon
                        .Select(p => new PokemonListItem
                        {
                            Name = p.Name
                        }).ToList()
                };
        }

        public async Task<bool> UpdateRegionAsync(RegionEdit model)
        {
            var entity = 
        }

        public Task<bool> DeleteRegionAsync(int noteId)
        {
            throw new System.NotImplementedException();
        }

    }
}