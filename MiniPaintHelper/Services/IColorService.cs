using Services.Models;

namespace Services;

public interface IColorService
{
    Task<IList<Paint>> GetClosestPaints(int r, int g, int b, List<string> selectedBrands = null!);
    Task<IList<Paint>> GetContrastingPaints(int r, int g, int b, List<string> brandFilters);
}