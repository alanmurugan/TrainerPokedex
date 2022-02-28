using System.ComponentModel.DataAnnotations;

namespace TrainerPokedex.Shared.Models.Type
{
    public class TypeEdit
    {
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; }
    }
}