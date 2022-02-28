using System.ComponentModel.DataAnnotations;

namespace TrainerPokedex.Shared.Models.Region
{
    public class RegionCreate
    {
        [Required] public string Name { get; set; }
        [Required] public int GenIntroduced { get; set; }
    }
}