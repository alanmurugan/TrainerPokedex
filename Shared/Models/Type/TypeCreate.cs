using System.ComponentModel.DataAnnotations;

namespace TrainerPokedex.Shared.Models.Type
{
    public class TypeCreate
    {
        [Required] public string Name { get; set; }
    }
}