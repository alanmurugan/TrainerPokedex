using System.Collections.Generic;
using System.Threading.Tasks;
using TrainerPokedex.Shared.Models.Move;

namespace TrainerPokedex.Server.Services.MoveServices
{
    public interface IMoveService
    {
        Task<bool> CreateMoveAsync(MoveCreate model);
        Task<IEnumerable<MoveListItem>> GetAllMoveAsync();
        Task<MoveDetail> GetMoveByIdAsync(int moveId);
        Task<bool> UpdateMoveAsync(MoveEdit model);
        Task<bool> DeleteMoveAsync(int moveId);
        void SetUserId(string userId);
    }
}