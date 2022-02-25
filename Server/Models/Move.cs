using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainerPokedex.Server.Models
{
    public class Move
    {
        //Required properties
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int BaseStrength { get; set; }
        [Required] public string Description { get; set; }
        
        //One to many relationship with Type (Many moves, one type for each)
        public int? TypeId { get; set; }
        public TypeX Type { get; set; }
        public ICollection<Pokemon> TeachablePokemon { get; set; }
    }
}