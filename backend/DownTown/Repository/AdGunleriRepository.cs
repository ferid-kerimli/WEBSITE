using DownTown.Context;
using DownTown.Entity;

namespace DownTown.Repository;

public interface IAdGunleriRepository : IRepository<AdGunleri>
{
    
}

public class AdGunleriRepository : Repository<AdGunleri>, IAdGunleriRepository
{
    public AdGunleriRepository(ApplicationDbContext context) : base(context)
    {
    }
}