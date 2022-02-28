using System.ComponentModel.DataAnnotations;

namespace TrainerPokedex.Shared.Models.Move
{
    public class MoveCreate
    {
        [Required] public string Name { get; set; }
        [Required] public int BaseStrength { get; set; }
        [Required] public string Description { get; set; }
        public int TypeId { get; set; }
    }
}