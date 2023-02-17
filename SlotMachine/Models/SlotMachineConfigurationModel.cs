using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineApp
{
    public class SlotMachineConfigurationModel
    {
        public int NumberOfSpins { get; set; }
        public int NumberOfSlots { get; set; }
        public List<char> SpecialSymbols { get; set; }
        public List<SymbolConfigurationModel> SymbolsConfig { get; set; }
    }
}
