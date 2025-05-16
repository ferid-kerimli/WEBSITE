using DownTown.Dto;
using DownTown.Entity;
using DownTown.Response;
using DownTown.UnitOfWork;

namespace DownTown.Service;

public interface IImageService
{
    Task<ApiResponse<bool>> GetImage(int id);
    Task<ApiResponse<bool>> UploadImage(ImageDto imageDto);
    Task<ApiResponse<bool>> UpdateImage(int imageId, ImageDto imageDto);
}

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _env;
    private readonly IUnitOfWork _unitOfWork;

    public ImageService(IWebHostEnvironment env, IUnitOfWork unitOfWork)
    {
        _env = env;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<bool>> GetImage(int id)
    {
        var response = new ApiResponse<bool>();

        try
        {
            var image = await _unitOfWork.ImageRepository.GetById(id);
            if (image == null)
            {
                response.Failure("Image not found", 404);
                return response;
            }

            var fullPath = Path.Combine(_env.WebRootPath, image.FilePath);
            if (!File.Exists(fullPath))
            {
                response.Failure("Image file not found on server", 404);
                return response;
            }

            response.Success(true, 200);
        }
        catch (Exception ex)
        {
            response.Failure(ex.Message, 500);
        }

        return response;
    }


    public async Task<ApiResponse<bool>> UploadImage(ImageDto imageDto)
    {
        var response = new ApiResponse<bool>();

        try
        {
            if (imageDto?.File == null || imageDto.File.Length == 0)
            {
                response.Failure("No file uploaded", 400);
                return response;
            }
            
            string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
            string uniqueFileName = $"{Guid.NewGuid()}_{imageDto.File.FileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageDto.File.CopyToAsync(fileStream);
            }

            var image = new Image
            {
                FilePath = $"images/{uniqueFileName}" 
            };

            await _unitOfWork.ImageRepository.Create(image);
            await _unitOfWork.Save();

            response.Success(true, 201);
        }
        catch (Exception e)
        {
            response.Failure(e.Message, 500);
            throw;
        }
        
        return response;
    }
    
    public async Task<ApiResponse<bool>> UpdateImage(int imageId, ImageDto imageDto)
    {
        var response = new ApiResponse<bool>();

        try
        {
            if (imageDto?.File == null || imageDto.File.Length == 0)
            {
                response.Failure("No file uploaded", 400);
                return response;
            }

            var existingImage = await _unitOfWork.ImageRepository.GetById(imageId);
            if (existingImage == null)
            {
                response.Failure("Image not found", 404);
                return response;
            }

            var oldFilePath = Path.Combine(_env.WebRootPath, existingImage.FilePath);
            if (File.Exists(oldFilePath))
            {
                File.Delete(oldFilePath);
            }

            string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
            string uniqueFileName = $"{Guid.NewGuid()}_{imageDto.File.FileName}";
            string newFilePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(newFilePath, FileMode.Create))
            {
                await imageDto.File.CopyToAsync(fileStream);
            }

            existingImage.FilePath = $"images/{uniqueFileName}";
            _unitOfWork.ImageRepository.Update(existingImage);
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