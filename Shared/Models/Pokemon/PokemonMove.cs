using System.ComponentModel.DataAnnotations;

namespace TrainerPokedex.Shared.Models.Pokemon
{
    public class PokemonMove
    {
        [Required] public int PokemonId { get; set; }
        [Required] public int MoveId { get; set; }
    }
}