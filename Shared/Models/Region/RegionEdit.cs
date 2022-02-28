using System.ComponentModel.DataAnnotations;

namespace TrainerPokedex.Shared.Models.Region
{
    public class RegionEdit
    {
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int GenIntroduced { get; set; }
    }
}