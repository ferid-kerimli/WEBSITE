using DownTown.Dto;
using DownTown.Entity;
using DownTown.Extensions;
using DownTown.Response;
using Microsoft.AspNetCore.Identity;

namespace DownTown.Service;

public interface IAuthenticationService
{
    Task<ApiResponse<JwtTokenResponse>> Login(LoginDto loginDto, IConfiguration configuration);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AuthenticationService(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    
    public async Task<ApiResponse<JwtTokenResponse>> Login(LoginDto loginDto, IConfiguration configuration)
    {
        var response = new ApiResponse<JwtTokenResponse>()
        {
            Data = new JwtTokenResponse()
        };

        try
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            
            if (user == null)
            {
                response.Failure("User not found", 404);
                return response;
            }
            
            var roles = await _userManager.GetRolesAsync(user);
    
            var signInResult =
                await _signInManager.PasswordSignInAsync(user.UserName ?? throw new InvalidOperationException(), loginDto.Password, loginDto.IsPersist, false);

            
            if (signInResult.Succeeded)
            {
                var generatedToken =
                    await TokenGenerator.GenerateToken(new TokenRequest { Email = loginDto.Email }, configuration, roles);

                response.Data.Token = generatedToken.Token;
                response.Data.ExpireDate = generatedToken.ExpireDate;
                
                response.Success(response.Data, 200);
            }
            else
            {
                response.Failure("Username or password is not correct", 401);
                return response;
            }
        }
        catch (Exception e)
        {
            response.Failure(e.Message, 500);
            throw;
        }

        return response;
    }
}