using DownTown.Context;
using DownTown.Entity;

namespace DownTown.Repository;

public interface IBoyOrGirlRepository : IRepository<BoyOrGirl>
{
    
}

public class BoyOrGirlRepository : Repository<BoyOrGirl>, IBoyOrGirlRepository
{
    public BoyOrGirlRepository(ApplicationDbContext context) : base(context)
    {
    }
}