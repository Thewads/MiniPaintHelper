using Moq;
using Services;
using Services.Providers;

namespace ServicesTests;

public class BrandServiceShould
{
    [Fact]
    public void ReturnAListOfBrandsAsStringsInAlphabeticalOrder()
    {
        var paintProvider = new Mock<IPaintProvider>();
        paintProvider.Setup(x => x.GetBrands()).ReturnsAsync(new List<string>
        {
            "Vallejo Color", "Vallejo Model","Citadel"
        });
        
        var service = new BrandService(paintProvider.Object);
        IList<string> brands = service.GetBrands().Result;
        
        Assert.NotNull(brands);
        Assert.Collection(brands, 
            brand => Assert.Equal("Citadel", brand),
            brand => Assert.Equal("Vallejo Color", brand),
            brand => Assert.Equal("Vallejo Model", brand));
    }
}