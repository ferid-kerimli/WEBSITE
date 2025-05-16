using DownTown.Context;
using DownTown.Entity;

namespace DownTown.Repository;

public interface IDecorRepository : IRepository<Decor>
{
    
}

public class DecorRepository : Repository<Decor>, IDecorRepository
{
    public DecorRepository(ApplicationDbContext context) : base(context)
    {
    }
}