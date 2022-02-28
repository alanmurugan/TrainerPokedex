using System.Collections.Generic;
using TrainerPokedex.Shared.Models.Pokemon;
using TrainerPokedex.Shared.Models.Type;

namespace TrainerPokedex.Shared.Models.Move
{
    public class MoveDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BaseStrength { get; set; }
        public string Description { get; set; }
        public int? TypeId { get; set; }
        public TypeListItem Type { get; set; }
        public ICollection<PokemonListItem> TeachablePokemon { get; set; }
    }
}