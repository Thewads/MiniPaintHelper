using Services.Models;

namespace Services;

public interface IPaintService
{
    Task<IList<Paint>> GetPaint(string searchTerm);
    Task<IList<Paint>> GetAllPaints();
}