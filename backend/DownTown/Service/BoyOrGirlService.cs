using DownTown.Dto.BoyOrGirl;
using DownTown.Entity;
using DownTown.Response;
using DownTown.UnitOfWork;

namespace DownTown.Service;

public interface IBoyOrGirlService
{
    Task<ApiResponse<List<BoyOrGirlGetDto>>> Get();
    Task<ApiResponse<bool>> Create(BoyOrGirlDto boyOrGirlDto);
    Task<ApiResponse<bool>> Update(BoyOrGirlDto boyOrGirlDto);
    Task<ApiResponse<bool>> Delete(int id);
}

public class BoyOrGirlService : IBoyOrGirlService
{
    private readonly IWebHostEnvironment _env;
    private readonly IUnitOfWork _unitOfWork;

    public BoyOrGirlService(IWebHostEnvironment env, IUnitOfWork unitOfWork)
    {
        _env = env;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ApiResponse<List<BoyOrGirlGetDto>>> Get()
    {
        var response = new ApiResponse<List<BoyOrGirlGetDto>>();

        try
        {
            var boyOrGirls = _unitOfWork.BoyOrGirlRepository.GetAll();

            if (boyOrGirls == null || !boyOrGirls.Any())
            {
                response.Failure("No BoyOrGirl found", 404);
                return response;
            }

            var boyOrGirlDtos = boyOrGirls.Select(item => new BoyOrGirlGetDto
            {
                Id = item.Id,
                Name = item.Name,
                Files = boyOrGirls.Select(i => i.FilePath).ToList()
            }).ToList();

            response.Success(boyOrGirlDtos, 200);
        }
        catch (Exception e)
        {
            response.Failure(e.Message, 500);
            throw;
        }

        return response;
    }
    
    public async Task<ApiResponse<bool>> Create(BoyOrGirlDto boyOrGirlDto)
    {
        var response = new ApiResponse<bool>();

        try
        {
            if (boyOrGirlDto == null || boyOrGirlDto.File == null || boyOrGirlDto.File.Length == 0)
            {
                response.Failure("Invalid input or no file uploaded", 400);
                return response;
            }

            string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
            string uniqueFileName = $"{Guid.NewGuid()}_{boyOrGirlDto.File.FileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await boyOrGirlDto.File.CopyToAsync(fileStream);
            }

            var boyOrGirl = new BoyOrGirl
            {
                Name = boyOrGirlDto.Name,
                FilePath = $"images/{uniqueFileName}"
            };

            await _unitOfWork.BoyOrGirlRepository.Create(boyOrGirl);
            await _unitOfWork.Save();

            response.Success(true, 201);
        }
        catch (Exception ex)
        {
            response.Failure(ex.Message, 500);
        }

        return response;
    }

    public async Task<ApiResponse<bool>> Update(BoyOrGirlDto boyOrGirlDto)
    {
        var response = new ApiResponse<bool>();

        try
        {
            if (boyOrGirlDto == null || boyOrGirlDto.Id <= 0)
            {
                response.Failure("Invalid BoyOrGirl ID", 400);
                return response;
            }

            var existingBoyOrGirl = await _unitOfWork.BoyOrGirlRepository.GetById(boyOrGirlDto.Id);
            if (existingBoyOrGirl == null)
            {
                response.Failure("BoyOrGirl not found", 404);
                return response;
            }

            existingBoyOrGirl.Name = boyOrGirlDto.Name;

            if (boyOrGirlDto.File != null && boyOrGirlDto.File.Length > 0)
            {
                var oldFilePath = Path.Combine(_env.WebRootPath, existingBoyOrGirl.FilePath);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                string uniqueFileName = $"{Guid.NewGuid()}_{boyOrGirlDto.File.FileName}";
                string newFilePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    await boyOrGirlDto.File.CopyToAsync(fileStream);
                }

                existingBoyOrGirl.FilePath = $"images/{uniqueFileName}";
            }

            _unitOfWork.BoyOrGirlRepository.Update(existingBoyOrGirl);
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
            var boyOrGirl = await _unitOfWork.BoyOrGirlRepository.GetById(id);
            if (boyOrGirl == null)
            {
                response.Failure("BoyOrGirl not found", 404);
                return response;
            }

            var imagePath = Path.Combine(_env.WebRootPath, boyOrGirl.FilePath);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            _unitOfWork.BoyOrGirlRepository.Delete(boyOrGirl);
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