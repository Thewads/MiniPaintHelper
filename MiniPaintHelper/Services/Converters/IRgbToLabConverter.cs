using Services.Models;

namespace Services.Converters;

public interface IRgbToLabConverter
{
    Lab Convert(Rgb inputRgb);
}