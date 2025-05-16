using DownTown.Dto.AdGunleri;
using DownTown.Entity;
using DownTown.Response;
using DownTown.UnitOfWork;

namespace DownTown.Service;

public interface IAdGunleriService
{
    Task<ApiResponse<List<AdGunleriGetDto>>> Get();
    Task<ApiResponse<bool>> Create(AdGunleriDto adGunleriDto);
    Task<ApiResponse<bool>> Update(AdGunleriDto adGunleriDto);
    Task<ApiResponse<bool>> Delete(int id);
}

public class AdGunleriService : IAdGunleriService
{
    private readonly IWebHostEnvironment _env;
    private readonly IUnitOfWork _unitOfWork;

    public AdGunleriService(IWebHostEnvironment env, IUnitOfWork unitOfWork)
    {
        _env = env;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ApiResponse<List<AdGunleriGetDto>>> Get()
    {
        var response = new ApiResponse<List<AdGunleriGetDto>>();

        try
        {
            var adGunleri = _unitOfWork.AdGunleriRepository.GetAll();

            if (adGunleri == null || !adGunleri.Any())
            {
                response.Failure("No AdGunleri found", 404);
                return response;
            }

            var adGunleriDtos = adGunleri.Select(item => new AdGunleriGetDto
            {
                Id = item.Id,
                Name = item.Name,
                Files = adGunleri.Select(i => i.FilePath).ToList()
            }).ToList();

            response.Success(adGunleriDtos, 200);
        }
        catch (Exception e)
        {
            response.Failure(e.Message, 500);
            throw;
        }

        return response;
    }

    public async Task<ApiResponse<bool>> Create(AdGunleriDto adGunleriDto)
    {
        var response = new ApiResponse<bool>();

        try
        {
            if (adGunleriDto == null || adGunleriDto.File == null || adGunleriDto.File.Length == 0)
            {
                response.Failure("Invalid input or no file uploaded", 400);
                return response;
            }

            string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
            string uniqueFileName = $"{Guid.NewGuid()}_{adGunleriDto.File.FileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await adGunleriDto.File.CopyToAsync(fileStream);
            }

            var adGunleri = new AdGunleri
            {
                Name = adGunleriDto.Name,
                FilePath = $"images/{uniqueFileName}"
            };

            await _unitOfWork.AdGunleriRepository.Create(adGunleri);
            await _unitOfWork.Save();

            response.Success(true, 201);
        }
        catch (Exception ex)
        {
            response.Failure(ex.Message, 500);
        }

        return response;
    }

    public async Task<ApiResponse<bool>> Update(AdGunleriDto adGunleriDto)
    {
        var response = new ApiResponse<bool>();

        try
        {
            if (adGunleriDto == null || adGunleriDto.Id <= 0)
            {
                response.Failure("Invalid AdGunleri ID", 400);
                return response;
            }

            var existingAdGunleri = await _unitOfWork.AdGunleriRepository.GetById(adGunleriDto.Id);
            if (existingAdGunleri == null)
            {
                response.Failure("AdGunleri not found", 404);
                return response;
            }

            existingAdGunleri.Name = adGunleriDto.Name;

            if (adGunleriDto.File != null && adGunleriDto.File.Length > 0)
            {
                var oldFilePath = Path.Combine(_env.WebRootPath, existingAdGunleri.FilePath);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                string uniqueFileName = $"{Guid.NewGuid()}_{adGunleriDto.File.FileName}";
                string newFilePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    await adGunleriDto.File.CopyToAsync(fileStream);
                }

                existingAdGunleri.FilePath = $"images/{uniqueFileName}";
            }

            _unitOfWork.AdGunleriRepository.Update(existingAdGunleri);
            await _unitOfWork.Save();

            response.Success(true, 200);
        }
        catch (Exception ex)
        {
            response.Failure(ex.Message, 500);
        }

        return response;
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        var response = new ApiResponse<bool>();

        try
        {
            var adGunleri = await _unitOfWork.AdGunleriRepository.GetById(id);
            if (adGunleri == null)
            {
                response.Failure("AdGunleri not found", 404);
                return response;
            }

            var imagePath = Path.Combine(_env.WebRootPath, adGunleri.FilePath);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            _unitOfWork.AdGunleriRepository.Delete(adGunleri);
            await _unitOfWork.Save();

            response.Success(true, 200);
        }
        catch (Exception ex)
        {
            response.Failure(ex.Message, 500);
        }

        return response;
    }
}