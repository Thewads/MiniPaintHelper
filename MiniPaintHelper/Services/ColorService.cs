using ColorMine.ColorSpaces.Comparisons;
using Services.Converters;
using Services.Models;
using Services.Providers;
using Rgb = Services.Models.Rgb;

namespace Services;

public class ColorService : IColorService
{
    private readonly IPaintProvider _paintProvider;
    private readonly IRgbToLabConverter _rgbToLabConverter;
    
    public ColorService(IPaintProvider paintProvider, IRgbToLabConverter rgbToLabConverter)
    {
        _paintProvider = paintProvider;
        _rgbToLabConverter = rgbToLabConverter;
    }
    
    public async Task<IList<Paint>> GetClosestPaints(int r, int g, int b, List<string> selectedBrands)
    {
        var labColor = _rgbToLabConverter.Convert(new Rgb { R = r, G = g, B = b });

        IList<Paint> allPaints = await _paintProvider.RetrieveAllPaints();
        IList<Tuple<Paint, double>> closestPaints = new List<Tuple<Paint, double>>();
        
        foreach (var paint in allPaints.Where(x => selectedBrands.Contains(x.Brand)))
        {
            var distance = new ColorMine.ColorSpaces.Lab {L = labColor.L, A = labColor.A, B = labColor.B}
                .Compare(new ColorMine.ColorSpaces.Lab {L = paint.Lab.L, A = paint.Lab.A, B = paint.Lab.B}, new CieDe2000Comparison());
            
            closestPaints.Add(new Tuple<Paint, double>(paint, distance));
        }

        return closestPaints.OrderBy(x => x.Item2).Take(5).Select(x => x.Item1).ToList();
    }

    public async Task<IList<Paint>> GetContrastingPaints(int r, int g, int b, List<string> brandFilters)
    {
        return await GetClosestPaints(255 - r, 255 - g, 255 - b, brandFilters);
    }
}

