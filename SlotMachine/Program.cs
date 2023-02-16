// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using SlotMachineApp;
using SlotMachineConsoleApp.DataAccessLogic;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .SetBasePath(Directory.GetCurrentDirectory())
    .Build();
var slotMachine = new SimpleSlotMachine(new SlotMachineHelper(new TextFileAccessor(config)));


Console.WriteLine("Please deposit money you would like to play with:");
slotMachine.Balance = decimal.Parse(Console.ReadLine());

Console.WriteLine("Enter stake amount:");
slotMachine.Stake = decimal.Parse(Console.ReadLine());

while(slotMachine.Stake > 0 && slotMachine.Balance > 0)
{
    if(slotMachine.Stake <= slotMachine.Balance)
    {
        slotMachine.Spin();
        var combinations = slotMachine.Combinations;
        foreach (var combination in combinations)
        {
            Console.WriteLine(combination);
        }

        if (slotMachine.HasWinningCombination())
        {
            Console.WriteLine($"Congratulations, your reward is {slotMachine.CalculateReward()}!");
        }
        else
        {
            Console.WriteLine("No winning lines!");
        }

        Console.WriteLine($"Current balance is {slotMachine.Balance}");
        Console.WriteLine("Game concluded. Try again?");
    }
    else
    {
        Console.WriteLine("Stake can't be higher than the balance");
    }

    Console.WriteLine("Enter stake amount:");
    slotMachine.Stake = decimal.Parse(Console.ReadLine());
}

if(slotMachine.Balance == 0)
{
    Console.WriteLine("You lost, balance is zero");
}
Console.WriteLine("Exiting Application");