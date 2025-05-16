using DownTown.Dto.AcilisTedbirleri;
using DownTown.Entity;
using DownTown.Response;
using DownTown.UnitOfWork;

namespace DownTown.Service;

public interface IAcilisTedbirleriService
{
    Task<ApiResponse<List<AcilisTedbirleriGetDto>>> GetAll();
    Task<ApiResponse<bool>> Create(AcilisTedbirleriDto acilisTedbirleriDto);
    Task<ApiResponse<bool>> Update(AcilisTedbirleriDto acilisTedbirleriDto);
    Task<ApiResponse<bool>> Delete(int id);
}

public class AcilisTedbirleriService : IAcilisTedbirleriService
{
    private readonly IWebHostEnvironment _env;
    private readonly IUnitOfWork _unitOfWork;

    public AcilisTedbirleriService(IWebHostEnvironment env, IUnitOfWork unitOfWork)
    {
        _env = env;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<List<AcilisTedbirleriGetDto>>> GetAll()
    {
        var response = new ApiResponse<List<AcilisTedbirleriGetDto>>();

        try
        {
            var acilisTedbirleri = _unitOfWork.AcilisTedbirleriRepository.GetAll();

            if (acilisTedbirleri == null || !acilisTedbirleri.Any())
            {
                response.Failure("No decors found", 404);
                return response;
            }

            var acilisTedbirleriDtos = acilisTedbirleri.Select(decor => new AcilisTedbirleriGetDto()
            {
                Id = decor.Id,
                Name = decor.Name,
                // Files = new List<string> { decor.ImagePath } // Single image case

                // If multiple images in the future:
                Files = acilisTedbirleri.Select(i => i.FilePath).ToList()
            }).ToList();

            response.Success(acilisTedbirleriDtos, 200);
        }
        catch (Exception e)
        {
            response.Failure(e.Message, 500);
            throw;
        }

        return response;
    }

    public async Task<ApiResponse<bool>> Create(AcilisTedbirleriDto acilisTedbirleriDto)
    {
        var response = new ApiResponse<bool>();

        try
        {
            if (acilisTedbirleriDto == null || acilisTedbirleriDto.File == null || acilisTedbirleriDto.File.Length == 0)
            {
                response.Failure("Invalid input or no file uploaded", 400);
                return response;
            }

            string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
            string uniqueFileName = $"{Guid.NewGuid()}_{acilisTedbirleriDto.File.FileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await acilisTedbirleriDto.File.CopyToAsync(fileStream);
            }

            var acilisTedbirleri = new AcilisTedbirleri
            {
                Name = acilisTedbirleriDto.Name,
                FilePath = $"images/{uniqueFileName}"
            };

            await _unitOfWork.AcilisTedbirleriRepository.Create(acilisTedbirleri);
            await _unitOfWork.Save();

            response.Success(true, 201);
        }
        catch (Exception ex)
        {
            response.Failure(ex.Message, 500);
        }

        return response;
    }


    public async Task<ApiResponse<bool>> Update(AcilisTedbirleriDto acilisTedbirleriDto)
    {
        var response = new ApiResponse<bool>();

        try
        {
            if (acilisTedbirleriDto == null || acilisTedbirleriDto.Id <= 0)
            {
                response.Failure("Invalid AcilisTedbirleri ID", 400);
                return response;
            }

            var existingAcilisTedbirleri = await _unitOfWork.AcilisTedbirleriRepository.GetById(acilisTedbirleriDto.Id);
            if (existingAcilisTedbirleri == null)
            {
                response.Failure("AcilisTedbirleri not found", 404);
                return response;
            }

            existingAcilisTedbirleri.Name = acilisTedbirleriDto.Name;

            if (acilisTedbirleriDto.File != null && acilisTedbirleriDto.File.Length > 0)
            {
                var oldFilePath = Path.Combine(_env.WebRootPath, existingAcilisTedbirleri.FilePath);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                string uniqueFileName = $"{Guid.NewGuid()}_{acilisTedbirleriDto.File.FileName}";
                string newFilePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    await acilisTedbirleriDto.File.CopyToAsync(fileStream);
                }

                existingAcilisTedbirleri.FilePath = $"images/{uniqueFileName}";
            }

            _unitOfWork.AcilisTedbirleriRepository.Update(existingAcilisTedbirleri);
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
            var acilisTedbirleri = await _unitOfWork.AcilisTedbirleriRepository.GetById(id);
            if (acilisTedbirleri == null)
            {
                response.Failure("AcilisTedbirleri not found", 404);
                return response;
            }

            var imagePath = Path.Combine(_env.WebRootPath, acilisTedbirleri.FilePath);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            _unitOfWork.AcilisTedbirleriRepository.Delete(acilisTedbirleri);
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