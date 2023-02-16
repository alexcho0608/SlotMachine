namespace SlotMachineApp
{
    public interface ISlotMachine
    {
        void Spin();
        decimal CalculateReward();
        bool HasWinningCombination();
    }
}