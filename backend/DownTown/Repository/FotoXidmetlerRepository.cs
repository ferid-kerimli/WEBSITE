using DownTown.Context;
using DownTown.Entity;

namespace DownTown.Repository;

public interface IFotoXidmetlerRepository : IRepository<FotoXidmetler>
{
    
}

public class FotoXidmetlerRepository : Repository<FotoXidmetler>, IFotoXidmetlerRepository
{
    public FotoXidmetlerRepository(ApplicationDbContext context) : base(context)
    {
    }
}