using DownTown.Context;
using DownTown.Entity;

namespace DownTown.Repository;

public interface IAcilisTedbileriRepository : IRepository<AcilisTedbirleri>
{
    
}

public class AcilisTedbirleriRepository : Repository<AcilisTedbirleri>, IAcilisTedbileriRepository
{
    public AcilisTedbirleriRepository(ApplicationDbContext context) : base(context)
    {
    }
}