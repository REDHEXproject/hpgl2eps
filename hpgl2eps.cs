using System;
using System.IO;
using System.Globalization;

class HPGLToEPSConverter
{
    static void Main()
    {
        string inputPath = "input.hpgl";
        string outputPath = "output.eps";

        ConvertHPGLToEPS(inputPath, outputPath);
    }

    static void ConvertHPGLToEPS(string inputPath, string outputPath)
    {
        using (StreamReader reader = new StreamReader(inputPath))
        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            
            writer.WriteLine("%!PS-Adobe-3.0 EPSF-3.0");
            writer.WriteLine("%%BoundingBox: 0 0 100 100");
            writer.WriteLine("0.2662068576 0.2662068576 scale");

            writer.WriteLine("newpath");

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] commands = line.Split(';');
                foreach (var command in commands)
                {
                    string postScriptCommand = ConvertHPGLCommandToPostScript(command);
                    if (!string.IsNullOrEmpty(postScriptCommand))
                    {
                        writer.WriteLine(postScriptCommand);
                    }
                }
            }

            writer.WriteLine("stroke");
            writer.WriteLine("showpage");
        }
    }

    static string ConvertHPGLCommandToPostScript(string hpglCommand)
    {
        if (hpglCommand.StartsWith("PU"))
        {
            return HandleCoordinates(hpglCommand.Substring(2), "moveto");
        }
        else if (hpglCommand.StartsWith("PD"))
        {
            return HandleCoordinates(hpglCommand.Substring(2), "lineto");
        }
        else if (hpglCommand.StartsWith("PA"))
        {
            return HandleCoordinates(hpglCommand.Substring(2), "moveto");
        }
        return null;
    }

    static string HandleCoordinates(string coordinateString, string command)
    {
        var coordinates = coordinateString.Split(',');
        if (coordinates.Length == 2)
        {
            double scaleFactor = 0.2662068576;
            double x = double.Parse(coordinates[0], CultureInfo.InvariantCulture);
            double y = double.Parse(coordinates[1], CultureInfo.InvariantCulture);
            return $"{x} {y} {command}";
        }
        return null;
    }
}
