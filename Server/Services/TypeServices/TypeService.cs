using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainerPokedex.Server.Data;
using TrainerPokedex.Server.Models;
using TrainerPokedex.Shared.Models.Move;
using TrainerPokedex.Shared.Models.Pokemon;
using TrainerPokedex.Shared.Models.Type;

namespace TrainerPokedex.Server.Services.TypeServices
{
    public class TypeService : ITypeService
    {
        private readonly ApplicationDbContext _context;
        public TypeService(ApplicationDbContext context) => _context = context;
        
        private string _userId;
        public void SetUserId(string userId) => _userId = userId;
        
        public async Task<bool> CreateTypeAsync(TypeCreate model)
        {
            var typeEntity = new TypeX
            {
                Name = model.Name
            };
            _context.Types.Add(typeEntity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<TypeListItem>> GetAllTypeAsync()
        {
            var typeQuery = _context
                .Types
                .Select(t => new TypeListItem
                {
                    Id = t.Id,
                    Name = t.Name
                });
            return await typeQuery.ToListAsync();
        }

        public async Task<TypeDetail> GetTypeByIdAsync(int typeId)
        {
            var typeEntity = await _context
                .Types
                .Include(m => m.Moves)
                .Include(p => p.Pokemon)
                .FirstOrDefaultAsync(t => t.Id == typeId);
            return typeEntity is null
                ? null
                : new TypeDetail
                {
                    Id = typeEntity.Id,
                    Name = typeEntity.Name,
                    Moves = typeEntity
                        .Moves
                        .Select(m => new MoveListItem
                        {
                            Name = m.Name
                        }).ToList(),
                    Pokemon = typeEntity
                        .Pokemon
                        .Select(p => new PokemonListItem
                        {
                            Name = p.Name
                        }).ToList()
                };
        }

        public async Task<bool> UpdateTypeAsync(TypeEdit model)
        {
            if (model == null) return false;
            var entity = await _context.Types.FindAsync(model.Id);

            entity.Name = model.Name;
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteTypeAsync(int typeId)
        {
            var entity = await _context.Types.FindAsync(typeId);
            _context.Types.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }
    }
}