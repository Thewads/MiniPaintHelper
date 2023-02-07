using Services.Providers;

namespace Services;

public class BrandService : IBrandService
{
    private readonly IPaintProvider _paintProvider;

    public BrandService(IPaintProvider paintProvider)
    {
        _paintProvider = paintProvider;
    }
    
    public async Task<IList<string>> GetBrands()
    {
        var brands = await _paintProvider.GetBrands();

        return brands.OrderBy(x => x).ToList();
    }
}