namespace Services;

public interface IBrandService
{
    Task<IList<string>> GetBrands();
}