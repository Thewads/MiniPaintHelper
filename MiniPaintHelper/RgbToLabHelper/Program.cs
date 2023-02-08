using Newtonsoft.Json;
using Services.Models;

Console.WriteLine("Tool for helping generate paint files");
Console.WriteLine("Give a path to a paint file with RGB populated to automatically generate the Lab values");

var filePath = Console.ReadLine();

if (filePath != null)
{
    var fileData = File.ReadAllText(filePath);

    var paints = JsonConvert.DeserializeObject<List<Paint>>(fileData);

    if (paints != null)
        foreach (var paint in paints)
        {
            var rgbColor = new ColorMine.ColorSpaces.Rgb { R = paint.Rgb.R, G = paint.Rgb.G, B = paint.Rgb.B };
            var labColor = rgbColor.To<ColorMine.ColorSpaces.Lab>();

            paint.Lab.L = Math.Round(labColor.L, 2);
            paint.Lab.A = Math.Round(labColor.A, 2);
            paint.Lab.B = Math.Round(labColor.B, 2);
        }
    
    File.WriteAllText(filePath, JsonConvert.SerializeObject(paints, Formatting.Indented));
}