using System.ComponentModel.DataAnnotations;

namespace TrainerPokedex.Shared.Models.Pokemon
{
    public class PokemonType
    {
        [Required] public int PokemonId { get; set; }
        [Required] public int TypeId { get; set; }
    }
}