using System.Collections.Generic;
using System.Threading.Tasks;
using TrainerPokedex.Shared.Models.Region;

namespace TrainerPokedex.Server.Services.RegionServices
{
    public interface IRegionService
    {
        //Create Region
        Task<bool> CreateRegionAsync(RegionCreate model);
        //Get all Regions
        Task<IEnumerable<RegionListItem>> GetAllRegionsAsync();
        //Get region by Id
        Task<RegionDetail> GetRegionByIdAsync(int regionId);
        //Update Region
        Task<bool> UpdateRegionAsync(RegionEdit model);
        //Delete Region
        Task<bool> DeleteRegionAsync(int regionId);
        void SetUserId(string userId);
    }
}