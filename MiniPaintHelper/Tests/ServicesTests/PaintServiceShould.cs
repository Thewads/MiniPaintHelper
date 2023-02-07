using Moq;
using Services;
using Services.Models;
using Services.Providers;

namespace ServicesTests;

public class PaintServiceShould
{
    [Theory]
    [InlineData("Paint 1", 1)]
    [InlineData("2", 1)]
    [InlineData("Paint", 5)]
    public void ReturnListOfPaintsWhichMatchSearchCriteria(string searchTerm, int expectedNumber)
    {
        var paintProvider = new Mock<IPaintProvider>();
        paintProvider.Setup(x => x.RetrieveAllPaints()).ReturnsAsync(new List<Paint>
        {
            new() { Brand = "Brand", Name = "Paint 1", Lab = new() { L = 0, A = 0, B = 0 } },
            new() { Brand = "Brand", Name = "Paint 2", Lab = new() { L = 0, A = 0, B = 0 } },
            new() { Brand = "Brand", Name = "Paint 3", Lab = new() { L = 0, A = 0, B = 0 } },
            new() { Brand = "Brand", Name = "Paint 4", Lab = new() { L = 0, A = 0, B = 0 } },
            new() { Brand = "Brand", Name = "Paint 5", Lab = new() { L = 0, A = 0, B = 0 } },
        });

        var paintService = new PaintService(paintProvider.Object);

        IList<Paint> result = paintService.GetPaint(searchTerm).Result;
        Assert.NotNull(result);
        Assert.Equal(expectedNumber, result.Count);
    }
}