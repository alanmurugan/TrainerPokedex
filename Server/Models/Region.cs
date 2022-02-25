using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainerPokedex.Server.Models
{
    public class Region
    {
        //Required properties
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        
        //Many to many relationship (many pokemon in many regions)
        public ICollection<Pokemon> LocalPokemon { get; set; }
    }
}