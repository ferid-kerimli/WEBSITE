using DownTown.Dto;
using DownTown.Entity;
using DownTown.Response;
using DownTown.UnitOfWork;

namespace DownTown.Service;

public interface IDecorService
{
    Task<ApiResponse<List<DecorGetDto>>> GetAll();
    Task<ApiResponse<bool>> Create(DecorDto decorDto);
    Task<ApiResponse<bool>> Update(DecorDto decorDto);
    Task<ApiResponse<bool>> Delete(int id);
}

public class DecorService : IDecorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _env;

    public DecorService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }


    public async Task<ApiResponse<List<DecorGetDto>>> GetAll()
    {
        var response = new ApiResponse<List<DecorGetDto>>();

        try
        {
            var decors = _unitOfWork.DecorRepository.GetAll();

            if (decors == null || !decors.Any())
            {
                response.Failure("No decors found", 404);
                return response;
            }

            var decorDtos = decors.Select(decor => new DecorGetDto
            {
                Id = decor.Id,
                Name = decor.Name,
                // Files = new List<string> { decor.ImagePath } // Single image case

                // If multiple images in the future:
                Files = decors.Select(i => i.FilePath).ToList()
            }).ToList();

            response.Success(decorDtos, 200);
        }
        catch (Exception e)
        {
            response.Failure(e.Message, 500);
            throw;
        }

        return response;
    }


    public async Task<ApiResponse<bool>> Create(DecorDto decorDto)
    {
        var response = new ApiResponse<bool>();

        try
        {
            if (decorDto == null || decorDto.File == null || decorDto.File.Length == 0)
            {
                response.Failure("Invalid input or no file uploaded", 400);
                return response;
            }

            string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
            string uniqueFileName = $"{Guid.NewGuid()}_{decorDto.File.FileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await decorDto.File.CopyToAsync(fileStream);
            }

            var decor = new Decor
            {
                Name = decorDto.Name,
                FilePath = $"images/{uniqueFileName}"
            };

            await _unitOfWork.DecorRepository.Create(decor);
            await _unitOfWork.Save();

            response.Success(true, 201);
        }
        catch (Exception ex)
        {
            response.Failure(ex.Message, 500);
        }

        return response;
    }


    public async Task<ApiResponse<bool>> Update(DecorDto decorDto)
    {
        var response = new ApiResponse<bool>();

        try
        {
            if (decorDto == null || decorDto.Id <= 0)
            {
                response.Failure("Invalid decor ID", 400);
                return response;
            }

            var existingDecor = await _unitOfWork.DecorRepository.GetById(decorDto.Id);
            if (existingDecor == null)
            {
                response.Failure("Decor not found", 404);
                return response;
            }

            existingDecor.Name = decorDto.Name;

            if (decorDto.File != null && decorDto.File.Length > 0)
            {
                var oldFilePath = Path.Combine(_env.WebRootPath, existingDecor.FilePath);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                string uniqueFileName = $"{Guid.NewGuid()}_{decorDto.File.FileName}";
                string newFilePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    await decorDto.File.CopyToAsync(fileStream);
                }

                existingDecor.FilePath = $"images/{uniqueFileName}";
            }

            _unitOfWork.DecorRepository.Update(existingDecor);
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
            var decor = await _unitOfWork.DecorRepository.GetById(id);
            if (decor == null)
            {
                response.Failure("Decor not found", 404);
                return response;
            }

            var imagePath = Path.Combine(_env.WebRootPath, decor.FilePath);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            _unitOfWork.DecorRepository.Delete(decor);
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