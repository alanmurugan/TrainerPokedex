using System.Collections.Generic;
using System.Threading.Tasks;
using TrainerPokedex.Shared.Models.Type;

namespace TrainerPokedex.Server.Services.TypeServices
{
    public interface ITypeService
    {
        Task<bool> CreateTypeAsync(TypeCreate model);
        Task<IEnumerable<TypeListItem>> GetAllTypeAsync();
        Task<TypeDetail> GetTypeByIdAsync(int typeId);
        Task<bool> UpdateTypeAsync(TypeEdit model);
        Task<bool> DeleteTypeAsync(int typeId);
        void SetUserId(string userId);
    }
}