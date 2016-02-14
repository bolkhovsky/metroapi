using ConsolePlate;
using System;
using System.Linq;
using System.Reflection;

namespace MetroApi.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var plate = new Plate();

            if (args.Any())
            {
                plate.Start(args, Assembly.GetExecutingAssembly());
            }
            else
            {
                plate.Start(args, Assembly.GetExecutingAssembly());
                var cmd = System.Console.ReadLine();
                plate.Start(cmd.Split(' '), Assembly.GetExecutingAssembly());
                System.Console.ReadKey();
            }
        }
    }
}
