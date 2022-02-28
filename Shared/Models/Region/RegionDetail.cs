using System.Collections.Generic;
using TrainerPokedex.Shared.Models.Pokemon;

namespace TrainerPokedex.Shared.Models.Region
{
    public class RegionDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GenIntroduced { get; set; }
        public ICollection<PokemonListItem> LocalPokemon { get; set; }
    }
}