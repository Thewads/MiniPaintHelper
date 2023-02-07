using System.Net.Http.Json;
using Services.Models;

namespace Services.Providers;

public class JsonProvider : IPaintProvider
{
    private readonly List<Paint> _paints = new();
    private List<string> _brands = new();
    private readonly HttpClient _httpClient;
    
    public JsonProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<IList<Paint>> RetrieveAllPaints()
    {
        if (!_paints.Any()) await PopulatePaintList();
        return _paints;
    }

    public async Task<IList<string>> GetBrands()
    {
        if (!_brands.Any()) await PopulatePaintList();
        return _brands;
    }

    private async Task PopulatePaintList()
    {
        var paintBrands = await _httpClient.GetFromJsonAsync<List<PaintBrandFile>>("Data/paints.json") ?? new List<PaintBrandFile>();
        _brands = paintBrands.Select(x => x.Brand).ToList();
        
        foreach (var paintBrand in paintBrands)
        {
            var paintsInBrand = await _httpClient.GetFromJsonAsync<List<Paint>>("Data/" + paintBrand.File);
            if(paintsInBrand != null)
                _paints.AddRange(paintsInBrand);
        }
    }
}