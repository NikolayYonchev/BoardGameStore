using System.ComponentModel.DataAnnotations;

namespace BoardGameStore.Models.Enums
{
    public enum Category
    {
        [Display(Name = "Family Game")]
        FamilyGame = 0,
        [Display(Name = "Strategy Game")]
        StrategyGame = 10,
        [Display(Name = "Party Game")]
        PartyGame = 20
    }
}