using DownTown.Context;
using DownTown.Entity;

namespace DownTown.Repository;

public interface IMasaBezedilmesiRepository : IRepository<MasaBezedilmesi>
{
    
}

public class MasaBezedilmesiRepository : Repository<MasaBezedilmesi>, IMasaBezedilmesiRepository
{
    public MasaBezedilmesiRepository(ApplicationDbContext context) : base(context)
    {
    }
}