namespace SlotMachineApp
{
    public interface ISlotMachineHelper
    {
        List<string> GenerateSpinCombinations();

        decimal GetCoefficient(string combination);

        bool IsWinningCombination(string combination);
    }
}