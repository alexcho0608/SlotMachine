using Newtonsoft.Json;
using SlotMachineConsoleApp.DataAccessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineApp
{
    public class SlotMachineHelper : ISlotMachineHelper
    {
        private SlotMachineConfigurationModel config;
        private Random rand = new Random(Guid.NewGuid().GetHashCode());
        private Dictionary<char, decimal> SymbolToCoefficientDict = new Dictionary<char, decimal>();
        List<(char symbol, decimal startInterval, decimal endInterval)> SymbolsInterval = new List<(char symbol, decimal startInterval, decimal endInterval)>();
        public SlotMachineHelper(IDataAccessor dataAccessor)
        {
            var configStr = dataAccessor.GetConfigData();
            config = JsonConvert.DeserializeObject<SlotMachineConfigurationModel>(configStr);
            LoadIntervalsForSymbols();
            LoadSymbolCoefficientDict();
        }

        public List<string> GenerateSpinCombinations()
        {
            var result = new List<string>();
            for(int i = 0; i < config.NumberOfSpins; i++)
            {
                result.Add(GenerateSpinCombination());
            }
            return result;
        }

        public decimal GetCoefficient(string combination)
        {
            decimal result = 0;

            foreach(var symbol in combination)
            {
                result += SymbolToCoefficientDict[symbol];
            }

            return result;
        }

        public bool IsWinningCombination(string combination)
        {
            var distinctSymbols = combination.Distinct();
            var bRes = distinctSymbols.Count() == 1 || (distinctSymbols.Count() == 2 && distinctSymbols.Any(e => config.SpecialSymbols.Contains(e)));
            return bRes;
        }

        private string GenerateSpinCombination()
        {
            StringBuilder result = new StringBuilder();
            for(int i = 0; i < config.NumberOfSlots; i++)
            {
                char symbol = GenerateSymbol();
                result.Append(symbol);
            }

            return result.ToString();
        }

        private char GenerateSymbol()
        {
            decimal number = rand.Next(1, 100);
            var symbolInterval = SymbolsInterval.First(e => e.startInterval <= number && number < e.endInterval);
            return symbolInterval.symbol;
        }

        private void LoadIntervalsForSymbols()
        {
            decimal startInterval = 1;
            foreach (var symbolConfig in config.SymbolsConfig)
            {
                SymbolsInterval.Add((symbolConfig.Symbol, startInterval, startInterval + symbolConfig.Probability));
                startInterval += symbolConfig.Probability;
            }
        }

        private void LoadSymbolCoefficientDict()
        {
            foreach (var symbolConfig in config.SymbolsConfig)
            {
                SymbolToCoefficientDict.Add(symbolConfig.Symbol, symbolConfig.Coefficient);
            }
        }
    }
}
