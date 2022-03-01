using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainerPokedex.Server.Data;
using TrainerPokedex.Server.Models;
using TrainerPokedex.Shared.Models.Move;
using TrainerPokedex.Shared.Models.Pokemon;

namespace TrainerPokedex.Server.Services.MoveServices
{
    public class MoveService : IMoveService
    {
        private readonly ApplicationDbContext _context;
        public MoveService(ApplicationDbContext context) => _context = context;
        
        private string _userId;
        public void SetUserId(string userId) => _userId = userId;
        
        public async Task<bool> CreateMoveAsync(MoveCreate model)
        {
            var moveEntity = new Move()
            {
                Name = model.Name,
                BaseStrength = model.BaseStrength,
                Description = model.Description,
                TypeId = model.TypeId
            };
            _context.Moves.Add(moveEntity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<MoveListItem>> GetAllMoveAsync()
        {
            var moveQuery = _context.Moves
                .Select(m => new MoveListItem
                {
                    Id = m.Id,
                    Name = m.Name,
                    Type = m.Type.Name
                });
            return await moveQuery.ToListAsync();
        }

        public async Task<MoveDetail> GetMoveByIdAsync(int moveId)
        {
            var moveEntity = await _context
                .Moves
                .Include(nameof(TypeX))
                .Include(p => p.TeachablePokemon)
                .FirstOrDefaultAsync(m => m.Id == moveId);
            return moveEntity is null
                ? null
                : new MoveDetail
                {
                    Id = moveEntity.Id,
                    Name = moveEntity.Name,
                    TypeId = moveEntity.Type.Id,
                    TypeName = moveEntity.Type.Name,
                    BaseStrength = moveEntity.BaseStrength,
                    Description = moveEntity.Description,
                    TeachablePokemon = moveEntity.TeachablePokemon
                        .Select(p => new PokemonListItem
                        {
                            Name = p.Name
                        }).ToList()
                };
        }

        public async Task<bool> UpdateMoveAsync(MoveEdit model)
        {
            if (model == null) return false;
            var entity = await _context.Moves.FindAsync(model.Id);

            entity.Name = (model.Name ?? entity.Name);
            entity.BaseStrength = (model.BaseStrength ?? entity.BaseStrength);
            entity.Description = (model.Description ?? entity.Description);
            entity.TypeId = (model.TypeId ?? entity.TypeId);

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteMoveAsync(int moveId)
        {
            var entity = await _context.Moves.FindAsync(moveId);
            _context.Moves.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }
    }
}