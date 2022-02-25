using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        //2 One to Many Relationships (Many pokemon with one type1 and one type2)
        public int? TypeId1 { get; set; }
        public TypeX Type1 { get; set; }
        public int? TypeId2 { get; set; }
        public TypeX Type2 { get; set; }
        //OR 1 Many to Many Relationship
        //public ICollection<Type> Types { get; set; }
        
        //Many to Many (Many pokemon in many regions, many pokemon have many moves)
        public ICollection<Region> RegionsFound { get; set; }
        public ICollection<Move> Moves { get; set; }
    }
}