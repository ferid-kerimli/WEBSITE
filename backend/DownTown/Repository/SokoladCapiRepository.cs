using DownTown.Context;
using DownTown.Entity;

namespace DownTown.Repository;

public interface ISokoladCapiRepository : IRepository<SokoladCapi>
{
    
}

public class SokoladCapiRepository : Repository<SokoladCapi>, ISokoladCapiRepository
{
    public SokoladCapiRepository(ApplicationDbContext context) : base(context)
    {
    }
}