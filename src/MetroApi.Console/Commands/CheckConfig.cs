using ConsolePlate.Abstract;
using System.Collections.Generic;
using NDesk.Options;
using System.ComponentModel.Composition;
using MetroApi.Core.Configuration;

namespace MetroApi.Console.Commands
{
    [Export(typeof(ICommand))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [ConsolePlate.Concrete.CommandInfo(
        CommandName = "check-config",
        CommandDescription = "Reads configuration file and outputs cities")]
    class CheckConfig : ICommand
    {
        public OptionSet Parameters
        {
            get { return new OptionSet(); }
        }

        public void Execute(IEnumerable<string> args)
        {
            var config = MetroApiConfig.GetConfig();
            foreach (City item in config.Cities)
            {
                System.Console.WriteLine(string.Format("id: {0}, filepath: {1}",
                    item.Id, item.Filepath));
            }
        }
    }
}
