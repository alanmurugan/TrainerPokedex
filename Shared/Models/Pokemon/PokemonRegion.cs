using System.ComponentModel.DataAnnotations;

namespace TrainerPokedex.Shared.Models.Pokemon
{
    public class PokemonRegion
    {
        [Required] public int PokemonId { get; set; }
        [Required] public int RegionId { get; set; }
    }
}