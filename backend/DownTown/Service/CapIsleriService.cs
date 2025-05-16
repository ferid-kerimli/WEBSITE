using DownTown.Dto.CapIsleri;
using DownTown.Entity;
using DownTown.Response;
using DownTown.UnitOfWork;

namespace DownTown.Service;

public interface ICapIsleriService
{
    Task<ApiResponse<List<CapIsleriGetDto>>> Get();
    Task<ApiResponse<bool>> Create(CapIsleriDto capIsleriDto);
    Task<ApiResponse<bool>> Update(CapIsleriDto capIsleriDto);
    Task<ApiResponse<bool>> Delete(int id);
}

public class CapIsleriService : ICapIsleriService
{
    private readonly IWebHostEnvironment _env;
    private readonly IUnitOfWork _unitOfWork;

    public CapIsleriService(IWebHostEnvironment env, IUnitOfWork unitOfWork)
    {
        _env = env;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ApiResponse<List<CapIsleriGetDto>>> Get()
    {
        var response = new ApiResponse<List<CapIsleriGetDto>>();

        try
        {
            var capIsleri = _unitOfWork.CapIsleriRepository.GetAll();

            if (capIsleri == null || !capIsleri.Any())
            {
                response.Failure("No CapIsleri found", 404);
                return response;
            }

            var capIsleriDtos = capIsleri.Select(item => new CapIsleriGetDto
            {
                Id = item.Id,
                Name = item.Name,
                Files = capIsleri.Select(i => i.FilePath).ToList()
            }).ToList();

            response.Success(capIsleriDtos, 200);
        }
        catch (Exception e)
        {
            response.Failure(e.Message, 500);
            throw;
        }

        return response;
    }
    public async Task<ApiResponse<bool>> Create(CapIsleriDto capIsleriDto)
    {
        var response = new ApiResponse<bool>();

        try
        {
            if (capIsleriDto == null || capIsleriDto.File == null || capIsleriDto.File.Length == 0)
            {
                response.Failure("Invalid input or no file uploaded", 400);
                return response;
            }

            string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
            string uniqueFileName = $"{Guid.NewGuid()}_{capIsleriDto.File.FileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await capIsleriDto.File.CopyToAsync(fileStream);
            }

            var capIsleri = new CapIsleri
            {
                Name = capIsleriDto.Name,
                FilePath = $"images/{uniqueFileName}"
            };

            await _unitOfWork.CapIsleriRepository.Create(capIsleri);
            await _unitOfWork.Save();

            response.Success(true, 201);
        }
        catch (Exception ex)
        {
            response.Failure(ex.Message, 500);
        }

        return response;
    }
    public async Task<ApiResponse<bool>> Update(CapIsleriDto capIsleriDto)
    {
        var response = new ApiResponse<bool>();

        try
        {
            if (capIsleriDto == null || capIsleriDto.Id <= 0)
            {
                response.Failure("Invalid CapIsleri ID", 400);
                return response;
            }

            var existingCapIsleri = await _unitOfWork.CapIsleriRepository.GetById(capIsleriDto.Id);
            if (existingCapIsleri == null)
            {
                response.Failure("CapIsleri not found", 404);
                return response;
            }

            existingCapIsleri.Name = capIsleriDto.Name;

            if (capIsleriDto.File != null && capIsleriDto.File.Length > 0)
            {
                var oldFilePath = Path.Combine(_env.WebRootPath, existingCapIsleri.FilePath);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                string uniqueFileName = $"{Guid.NewGuid()}_{capIsleriDto.File.FileName}";
                string newFilePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    await capIsleriDto.File.CopyToAsync(fileStream);
                }

                existingCapIsleri.FilePath = $"images/{uniqueFileName}";
            }

            _unitOfWork.CapIsleriRepository.Update(existingCapIsleri);
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
            var capIsleri = await _unitOfWork.CapIsleriRepository.GetById(id);
            if (capIsleri == null)
            {
                response.Failure("CapIsleri not found", 404);
                return response;
            }

            var imagePath = Path.Combine(_env.WebRootPath, capIsleri.FilePath);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            _unitOfWork.CapIsleriRepository.Delete(capIsleri);
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