using System.Collections.Generic;
using TrainerPokedex.Shared.Models.Move;
using TrainerPokedex.Shared.Models.Region;
using TrainerPokedex.Shared.Models.Type;

namespace TrainerPokedex.Shared.Models.Pokemon
{
    public class PokemonDetail
    {
        public int Id { get; set; }
        public int DexNumber { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public int Generation { get; set; }
        public string DexEntry { get; set; }
        
        public ICollection<TypeListItem> Types { get; set; }
        public ICollection<RegionListItem> RegionsFound { get; set; }
        public ICollection<MoveListItem> Moves { get; set; }
    }
}