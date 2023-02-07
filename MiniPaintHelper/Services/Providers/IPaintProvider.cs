using Services.Models;

namespace Services.Providers;

public interface IPaintProvider
{
    Task<IList<Paint>> RetrieveAllPaints();
    Task<IList<string>> GetBrands();
}