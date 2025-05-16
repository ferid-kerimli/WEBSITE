using DownTown.Context;
using DownTown.Entity;

namespace DownTown.Repository;

public interface ICapIsleriRepository : IRepository<CapIsleri>
{
    
}

public class CapIsleriRepository : Repository<CapIsleri>, ICapIsleriRepository
{
    public CapIsleriRepository(ApplicationDbContext context) : base(context)
    {
    }
}