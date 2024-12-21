using System.ComponentModel.DataAnnotations;

namespace BoardGameStore.Models.Enums
{
    public enum Condition
    {
        New = 0,
        [Display(Name = "Like New")]
        LikeNew = 10,
        Used = 20
    }
}