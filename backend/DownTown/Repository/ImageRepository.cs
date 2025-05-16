using DownTown.Context;
using DownTown.Entity;

namespace DownTown.Repository;

public interface IImageRepository : IRepository<Image>
{
    
}

public class ImageRepository : Repository<Image>, IImageRepository
{
    public ImageRepository(ApplicationDbContext context) : base(context)
    {
    }
}