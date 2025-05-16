using DownTown.Dto.MasaBezedilmesi;
using DownTown.Entity;
using DownTown.Response;
using DownTown.UnitOfWork;

namespace DownTown.Service;

public interface IMasaBezedilmesiService
{
    Task<ApiResponse<List<MasaBezedilmesiGetDto>>> Get();
    Task<ApiResponse<bool>> Create(MasaBezedilmesiDto masaBezedilmesiDto);
    Task<ApiResponse<bool>> Update(MasaBezedilmesiDto masaBezedilmesiDto);
    Task<ApiResponse<bool>> Delete(int id);
}

public class MasaBezedilmesiService : IMasaBezedilmesiService
{
    private readonly IWebHostEnvironment _env;
    private readonly IUnitOfWork _unitOfWork;

    public MasaBezedilmesiService(IWebHostEnvironment env, IUnitOfWork unitOfWork)
    {
        _env = env;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ApiResponse<List<MasaBezedilmesiGetDto>>> Get()
    {
        var response = new ApiResponse<List<MasaBezedilmesiGetDto>>();

        try
        {
            var items = _unitOfWork.MasaBezedilmesiRepository.GetAll();

            if (items == null || !items.Any())
            {
                response.Failure("No MasaBezedilmesi found", 404);
                return response;
            }

            var dtoList = items.Select(item => new MasaBezedilmesiGetDto
            {
                Id = item.Id,
                Name = item.Name,
                Files = items.Select(i => i.FilePath).ToList()
            }).ToList();

            response.Success(dtoList, 200);
        }
        catch (Exception e)
        {
            response.Failure(e.Message, 500);
            throw;
        }

        return response;
    }
    
    public async Task<ApiResponse<bool>> Create(MasaBezedilmesiDto dto)
    {
        var response = new ApiResponse<bool>();

        try
        {
            if (dto == null || dto.File == null || dto.File.Length == 0)
            {
                response.Failure("Invalid input or no file uploaded", 400);
                return response;
            }

            string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
            string uniqueFileName = $"{Guid.NewGuid()}_{dto.File.FileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(fileStream);
            }

            var item = new MasaBezedilmesi
            {
                Name = dto.Name,
                FilePath = $"images/{uniqueFileName}"
            };

            await _unitOfWork.MasaBezedilmesiRepository.Create(item);
            await _unitOfWork.Save();

            response.Success(true, 201);
        }
        catch (Exception ex)
        {
            response.Failure(ex.Message, 500);
        }

        return response;
    }
    
    public async Task<ApiResponse<bool>> Update(MasaBezedilmesiDto dto)
    {
        var response = new ApiResponse<bool>();

        try
        {
            if (dto == null || dto.Id <= 0)
            {
                response.Failure("Invalid MasaBezedilmesi ID", 400);
                return response;
            }

            var existingItem = await _unitOfWork.MasaBezedilmesiRepository.GetById(dto.Id);
            if (existingItem == null)
            {
                response.Failure("MasaBezedilmesi not found", 404);
                return response;
            }

            existingItem.Name = dto.Name;

            if (dto.File != null && dto.File.Length > 0)
            {
                var oldFilePath = Path.Combine(_env.WebRootPath, existingItem.FilePath);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                string uniqueFileName = $"{Guid.NewGuid()}_{dto.File.FileName}";
                string newFilePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    await dto.File.CopyToAsync(fileStream);
                }

                existingItem.FilePath = $"images/{uniqueFileName}";
            }

            _unitOfWork.MasaBezedilmesiRepository.Update(existingItem);
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
            var item = await _unitOfWork.MasaBezedilmesiRepository.GetById(id);
            if (item == null)
            {
                response.Failure("MasaBezedilmesi not found", 404);
                return response;
            }

            var imagePath = Path.Combine(_env.WebRootPath, item.FilePath);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            _unitOfWork.MasaBezedilmesiRepository.Delete(item);
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