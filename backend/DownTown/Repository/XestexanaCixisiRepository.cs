using DownTown.Context;
using DownTown.Entity;

namespace DownTown.Repository;

public interface IXestexanaCixisiRepository : IRepository<XestexanaCixisi>
{
    
}

public class XestexanaCixisiRepository : Repository<XestexanaCixisi>, IXestexanaCixisiRepository
{
    public XestexanaCixisiRepository(ApplicationDbContext context) : base(context)
    {
    }
}