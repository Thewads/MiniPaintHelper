using Services.Exceptions;
using Services.Models;

namespace Services.Converters;

public class RgbToLabConverter : IRgbToLabConverter
{
    public Lab Convert(Rgb inputRgb)
    {
        if (!inputRgb.IsValid()) throw new InvalidRgbException("Invalid RGB values provided");

        var rgbColor = new ColorMine.ColorSpaces.Rgb { R = inputRgb.R, G = inputRgb.G, B = inputRgb.B };
        var labColor = rgbColor.To<ColorMine.ColorSpaces.Lab>();
        
        return new Lab
        {
            L = Math.Round(labColor.L, 2),
            A = Math.Round(labColor.A, 2),
            B = Math.Round(labColor.B, 2)
        };
    }
}

