using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineApp
{
    public class SimpleSlotMachine : ISlotMachine
    {
        private List<string> combinations;
        private decimal balance;
        private bool isSetBalance = false;
        private ISlotMachineHelper slotMachineHelper;

        public decimal Stake { get; set; }
        public decimal Balance { get { return balance; } set { if (!isSetBalance) balance = value; isSetBalance = true; } }
        public List<string> Combinations { get { return combinations; }  }

        public SimpleSlotMachine(ISlotMachineHelper slotMachineHelper)
        {
            this.slotMachineHelper = slotMachineHelper;
        }

        public void Spin()
        {
            this.combinations = this.slotMachineHelper.GenerateSpinCombinations();
            var winningCoefficient = CountWinningCoefficient();
            this.balance = this.balance - (1 - winningCoefficient) * this.Stake; 
        }

        public decimal CalculateReward()
        {
            var coefficient = CountWinningCoefficient();

            return coefficient * this.Stake;
        }

        public bool HasWinningCombination()
        {
            foreach(var combination in combinations)
            {
                var bRes = this.slotMachineHelper.IsWinningCombination(combination);
                if (bRes)
                {
                    return true;
                }
            }

            return false;
        }

        private decimal CountWinningCoefficient()
        {
            decimal result = 0;
            foreach (var combination in combinations)
            {
                if (this.slotMachineHelper.IsWinningCombination(combination))
                {
                    result += this.slotMachineHelper.GetCoefficient(combination);
                }
            }
            return result;
        }

    }
}
