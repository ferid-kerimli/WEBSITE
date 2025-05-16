using DownTown.Context;
using DownTown.Repository;

namespace DownTown.UnitOfWork;

public interface IUnitOfWork
{
    IImageRepository ImageRepository { get; }
    IDecorRepository DecorRepository { get; }
    IAcilisTedbileriRepository AcilisTedbirleriRepository { get; }
    IAdGunleriRepository AdGunleriRepository { get; }
    IBoyOrGirlRepository BoyOrGirlRepository { get; }
    ICapIsleriRepository CapIsleriRepository { get; }
    IFotoXidmetlerRepository FotoXidmetlerRepository { get; }
    IMasaBezedilmesiRepository MasaBezedilmesiRepository { get; }
    ISokoladCapiRepository SokoladCapiRepository { get; }
    IXestexanaCixisiRepository XestexanaCixisiRepository { get; }
    Task<int> Save();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IImageRepository ImageRepository { get; }
    public IDecorRepository DecorRepository { get; }
    public IAcilisTedbileriRepository AcilisTedbirleriRepository { get; }
    public IAdGunleriRepository AdGunleriRepository { get; }
    public IBoyOrGirlRepository BoyOrGirlRepository { get; }
    public ICapIsleriRepository CapIsleriRepository { get; }
    public IFotoXidmetlerRepository FotoXidmetlerRepository { get; }
    public IMasaBezedilmesiRepository MasaBezedilmesiRepository { get; }
    public ISokoladCapiRepository SokoladCapiRepository { get; }
    public IXestexanaCixisiRepository XestexanaCixisiRepository { get; }

    public UnitOfWork(IImageRepository imageRepository, ApplicationDbContext context, IDecorRepository decorRepository, IAcilisTedbileriRepository acilisTedbileriRepository, IAdGunleriRepository adGunleriRepository, IBoyOrGirlRepository boyOrGirlRepository, ICapIsleriRepository capIsleriRepository, IFotoXidmetlerRepository fotoXidmetlerRepository, IMasaBezedilmesiRepository masaBezedilmesiRepository, IXestexanaCixisiRepository xestexanaCixisiRepository, ISokoladCapiRepository sokoladCapiRepository)
    {
        ImageRepository = imageRepository;
        _context = context;
        DecorRepository = decorRepository;
        AcilisTedbirleriRepository = acilisTedbileriRepository;
        AdGunleriRepository = adGunleriRepository;
        BoyOrGirlRepository = boyOrGirlRepository;
        CapIsleriRepository = capIsleriRepository;
        FotoXidmetlerRepository = fotoXidmetlerRepository;
        MasaBezedilmesiRepository = masaBezedilmesiRepository;
        XestexanaCixisiRepository = xestexanaCixisiRepository;
        SokoladCapiRepository = sokoladCapiRepository;
    }
    public async Task<int> Save()
    {
        return await _context.SaveChangesAsync();
    }
}