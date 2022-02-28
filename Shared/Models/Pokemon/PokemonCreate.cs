using System.ComponentModel.DataAnnotations;

namespace TrainerPokedex.Shared.Models.Pokemon
{
    public class PokemonCreate
    {
        [Required] public int DexNumber { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string ImgUrl { get; set; }
        [Required] public int Generation { get; set; }
        [Required] public string DexEntry { get; set; }
        
        
    }
}