using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainerPokedex.Server.Models
{
    public class TypeX
    {
        //Required Properties
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }

        //Collections of strengths and weaknesses to types
        //public ICollection<TypeX> StrongAgainst { get; set; }
        //public ICollection<TypeX> WeakAgainst { get; set; }

        
        // Many to many relationship (many pokemon have many moves)
        public ICollection<Move> Moves { get; set; }
        
        //Many to many relationship
        public ICollection<Pokemon> Pokemon { get; set; }
    }
}