using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainerPokedex.Server.Models
{
    public class Pokemon
    {
        //Required properties
        [Key] public int Id { get; set; }
        [Required] public int DexNumber { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string ImgUrl { get; set; }
        [Required] public int Generation { get; set; }
        [Required] public string DexEntry { get; set; }
        
        //How to handle dual typing
        public ICollection<TypeX> Types { get; set; }

        //Many to Many (Many pokemon in many regions, many pokemon have many moves)
        public ICollection<Region> RegionsFound { get; set; }
        public ICollection<Move> Moves { get; set; }
    }
}