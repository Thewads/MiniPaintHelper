using Moq;
using Services.Converters;
using Services.Models;
using Services.Providers;

namespace ServicesTests;

public class ColorServiceShould
{
    [Fact]
    public void ReturnListOfClosePaintsWhenPaintsArePopulated()
    {
        var paintProvider = new Mock<IPaintProvider>();
        paintProvider.Setup(x => x.RetrieveAllPaints()).ReturnsAsync(new List<Paint>
        {
            new() { Brand = "Brand 1", Name = "Name 1", Lab = new() { L = 0, A = 0, B = 0 } },
            new() { Brand = "Brand 1", Name = "Name 2", Lab = new() { L = 0, A = 0, B = 0 } },
            new() { Brand = "Brand 2", Name = "Name 1", Lab = new() { L = 0, A = 0, B = 0 } }
        });
        var converter = new RgbToLabConverter();
        
        var colorService = new Services.ColorService(paintProvider.Object, converter);

        int r = 0;
        int g = 0;
        int b = 0;
        var brandFilters = new List<string> { "Brand 1", "Brand 2" };

        var result = colorService.GetClosestPaints(r, g, b, brandFilters).Result;
        
        Assert.NotNull(result);
        Assert.Equal(3,result.Count);
    }
    
    [Fact]
    public void ReturnMaxOf5ClosestPaints()
    {
        var paintProvider = new Mock<IPaintProvider>();
        paintProvider.Setup(x => x.RetrieveAllPaints()).ReturnsAsync(new List<Paint>
        {
            new() { Brand = "Brand 1", Name = "Name 1", Lab = new() { L = 0, A = 0, B = 0 } },
            new() { Brand = "Brand 1", Name = "Name 2", Lab = new() { L = 0, A = 0, B = 0 } },
            new() { Brand = "Brand 2", Name = "Name 1", Lab = new() { L = 0, A = 0, B = 0 } },
            new() { Brand = "Brand 2", Name = "Name 2", Lab = new() { L = 0, A = 0, B = 0 } },
            new() { Brand = "Brand 2", Name = "Name 3", Lab = new() { L = 0, A = 0, B = 0 } },
            new() { Brand = "Brand 2", Name = "Name 4", Lab = new() { L = 0, A = 0, B = 0 } },
            new() { Brand = "Brand 2", Name = "Name 5", Lab = new() { L = 0, A = 0, B = 0 } }
        });
        var converter = new RgbToLabConverter();
        
        var colorService = new Services.ColorService(paintProvider.Object, converter);

        int r = 0;
        int g = 0;
        int b = 0;
        var brandFilters = new List<string> { "Brand 1", "Brand 2" };

        var result = colorService.GetClosestPaints(r, g, b, brandFilters).Result;
        
        Assert.NotNull(result);
        Assert.Equal(5,result.Count);
    }

    [Fact]
    public void FilterOutBrands()
    {
        var paintProvider = new Mock<IPaintProvider>();
        paintProvider.Setup(x => x.RetrieveAllPaints()).ReturnsAsync(new List<Paint>
        {
            new() { Brand = "Brand 1", Name = "Name 1", Lab = new() { L = 0, A = 0, B = 0 } },
            new() { Brand = "Brand 2", Name = "Name 2", Lab = new() { L = 0, A = 0, B = 0 } },
            new() { Brand = "Brand 3", Name = "Name 1", Lab = new() { L = 0, A = 0, B = 0 } },
        });
        var converter = new RgbToLabConverter();
        
        var colorService = new Services.ColorService(paintProvider.Object, converter);
        
        int r = 0;
        int g = 0;
        int b = 0;
        var brandFilters = new List<string> { "Brand 1", "Brand 3" };

        var result = colorService.GetClosestPaints(r, g, b, brandFilters).Result;
        
        Assert.NotNull(result);
        Assert.Equal(2,result.Count);
        Assert.True(result.All(x => brandFilters.Contains(x.Brand)));
    }

    [Fact]
    public void ReturnContrastingColorsCorrectly()
    {
        var paintProvider = new Mock<IPaintProvider>();
        paintProvider.Setup(x => x.RetrieveAllPaints()).ReturnsAsync(new List<Paint>
        {
            new() { Brand = "Brand 1", Name = "Red",  Lab = new() { L = 53.23, A = 80.11, B = 67.22 }, Rgb = new() {R = 255, G = 0, B = 0} },
            new() { Brand = "Brand 1", Name = "Red2", Lab = new() { L = 53.23, A = 80.11, B = 67.22 }, Rgb = new() {R = 255, G = 0, B = 0} },
            new() { Brand = "Brand 1", Name = "Red3", Lab = new() { L = 53.23, A = 80.11, B = 67.22 }, Rgb = new() {R = 255, G = 0, B = 0} },
            new() { Brand = "Brand 1", Name = "Red4", Lab = new() { L = 53.23, A = 80.11, B = 67.22 }, Rgb = new() {R = 255, G = 0, B = 0} },
            new() { Brand = "Brand 1", Name = "Red5", Lab = new()  { L = 32.3, A = 79.2, B = -107.86 }, Rgb = new() {R = 255, G = 0, B = 0} },
            new() { Brand = "Brand 1", Name = "Blue1", Lab = new() { L = 32.3, A = 79.2, B = -107.86 }, Rgb = new() {R = 255, G = 0, B = 0} },
            new() { Brand = "Brand 1", Name = "Blue2", Lab = new() { L = 32.3, A = 79.2, B = -107.86 }, Rgb = new() {R = 255, G = 0, B = 0} },
            new() { Brand = "Brand 1", Name = "Blue3", Lab = new() { L = 32.3, A = 79.2, B = -107.86 }, Rgb = new() {R = 255, G = 0, B = 0} }
        });
        var converter = new RgbToLabConverter();
        
        var colorService = new Services.ColorService(paintProvider.Object, converter);
        int r = 0;
        int g = 0;
        int b = 255;
        var brandFilters = new List<string> { "Brand 1" };

        IList<Paint> result = colorService.GetContrastingPaints(r, g, b, brandFilters).Result;
        Assert.NotNull(result);
        Assert.Equal(5, result.Count);
        Assert.DoesNotContain(result, x => x.Name.Contains("Blue"));
    }
}