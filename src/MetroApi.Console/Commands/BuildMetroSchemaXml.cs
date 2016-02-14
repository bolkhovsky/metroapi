using ConsolePlate.Abstract;
using System.Collections.Generic;
using NDesk.Options;
using System.ComponentModel.Composition;
using MetroApi.Core;
using MetroApi.Core.Models;
using MetroApi.Core.Exceptions;
using System.Xml.Serialization;
using System.Xml;

namespace MetroApi.Console.Commands
{
    [Export(typeof(ICommand))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [ConsolePlate.Concrete.CommandInfo(
        CommandName = "build-xml", 
        CommandDescription = "Builds metro schema xml for given city")]
    class BuildMetroSchemaXml : ICommand
    {
        private string _city;
        private string _filepath;

        public OptionSet Parameters
        {
            get
            {
                return new OptionSet
                {
                    {"c|city=", "City ID with metro (required)", x => _city = x},
                    {"f|file=", "Output file path (required)", x => _filepath = x},
                };
            }
        }

        public void Execute(IEnumerable<string> args)
        {
            var wikiParser = new WikiParser();
            City city;

            switch (_city)
            {
                case Constants.CityIds.Moscow:
                    city = City.Moscow();
                    city.MetroLines = wikiParser.GetMoscowMetroSchema();
                    break;

                case Constants.CityIds.SaintPetersburg:
                    city = City.SaintPetersburg();
                    city.MetroLines = wikiParser.GetSpbMetroSchema();
                    break;

                default:
                    throw new CityNotFound(_city);
            }

            var xmlSerializer = new XmlSerializer(typeof(City));
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };

            using (XmlWriter writer = XmlWriter.Create(_filepath, settings))
            {
                xmlSerializer.Serialize(writer, city);
            }
        }
    }
}
