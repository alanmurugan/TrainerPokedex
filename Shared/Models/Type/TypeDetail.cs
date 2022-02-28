using System.Collections;
using System.Collections.Generic;
using TrainerPokedex.Shared.Models.Move;
using TrainerPokedex.Shared.Models.Pokemon;

namespace TrainerPokedex.Shared.Models.Type
{
    public class TypeDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PokemonListItem> Pokemon { get; set; }
        public ICollection<MoveListItem> Moves { get; set; }
    }
}