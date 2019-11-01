using System;
using System.IO;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 1) throw new Exception("Please specify a filename for plan.");

                // Check the file exists
                if (!File.Exists(args[0])) throw new Exception("File not found.");

                // Read the file
                using (StreamReader file = new StreamReader(args[0]))
                {
                    try
                    {
                        var lines = file.ReadToEnd().Split('\n');

                        if (lines.Length > 3) throw new Exception("Plan may not consist of more than 3 lines.");

                        var plan = new Plan(lines[0].Trim(),lines[1].Trim(), lines[2].Trim());

                        var rover = new Rover(plan.HorizontalStart, plan.VerticalStart, plan.StartOrientation);

                        foreach (char c in plan.Commands)
                        {
                            var newLocation = rover.ExecuteCommand(c);
                            if (newLocation[0] > plan.BoundaryLimit
                                || newLocation[1] > plan.BoundaryLimit
                                || newLocation[0] < 1
                                || newLocation[1] < 1) 
                                throw new Exception("The rover is now a rather shiny hat.");
                        }

                        Console.WriteLine($"{rover.HorizontalLocation} {rover.VerticalLocation} {rover.Orientation}");
                    }
                    catch(Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        file.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
