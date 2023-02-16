using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineConsoleApp.DataAccessLogic
{
    public class TextFileAccessor : IDataAccessor
    {
        private string filePath;
        public TextFileAccessor(IConfiguration config)
        {
            filePath = config.GetValue<string>("SLOT_CONFIG_FILEPATH");
        }

        public string GetConfigData()
        {
            var result = File.ReadAllText(filePath);
            return result;
        }
    }
}
