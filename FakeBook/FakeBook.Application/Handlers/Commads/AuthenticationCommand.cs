using FakeBook.Application.Common.Interfaces;
using FakeBook.Application.Handlers.Queries.Services;
using FakeBook.Contracts.Commands;
using FakeBook.Contracts.Queries.Services;
using FakeBook.Domain.Entities;
using FakeBook.Domain.Enums;
using FakeBook.Domain.Models.Requests.Commands.Authentication;
using FakeBook.Domain.Models.Responses.Commands.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Application.Handlers.Commads
{
public class AuthenticationCommand : IAuthenticationCommand
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IJwtQueryService _jwtQueryService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AuthenticationCommand(IApplicationDbContext applicationDbContext, IJwtQueryService jwtQueryService,
                                 IWebHostEnvironment webHostEnvironment)
    {
        _applicationDbContext = applicationDbContext;
        _jwtQueryService = jwtQueryService;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<ResponseRegisterModel?> Register(RequestRegisterModel requestRegisterModel)
    {
        try
        {
            var accountAlreadyExists = await _applicationDbContext.Users.AsNoTracking().AnyAsync(
                entity => entity.Name == requestRegisterModel.Name.Trim().ToLower() ||
                          entity.Email == requestRegisterModel.Email.Trim().ToLower());

            if (accountAlreadyExists == true)
            {
                throw new Exception("Username or email already exists!");
            }

            ComputeHashPassword(requestRegisterModel.Password, out byte[] keyPassword, out byte[] hashPassword);

            var accountEntity = new UserEntity() { Name = requestRegisterModel.Name.Trim().ToLower(),
                                                   Email = requestRegisterModel.Email.Trim().ToLower(),
                                                   HashPassword = hashPassword,
                                                   Quote = requestRegisterModel.Quote,
                                                   KeyPassword = keyPassword,
                                                   RoleId = (int)RoleEnum.User,
                                                   CreatedDate = DateTime.Now };

            _applicationDbContext.Users.Add(accountEntity);
            var isSavedSuccessfully = await _applicationDbContext.SaveChangesAsync() > 0;
            ResponseRegisterModel token = new ResponseRegisterModel { Token = _jwtQueryService.GetJwtToken(
                                                                          accountEntity.Email, accountEntity.Id) };
            return isSavedSuccessfully ? token : null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ComputeHashPassword(string password, out byte[] keyPassword, out byte[] hashPassword)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();

        keyPassword = hmac.Key;
        hashPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public string UploadProfilePicture(IFormFile file)
    {
        var fileName =
            Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 6).Replace("\\", "_").Replace("+", "-");
        string filePath = String.Empty;
        if (file != null)
        {
            string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Resources/ProfilePictures");
            filePath = Path.Combine(directoryPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
        return filePath;
    }

    public string UploadCoverImage(IFormFile file)
    {
        var fileName =
            Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 6).Replace("\\", "_").Replace("+", "-");
        string filePath = String.Empty;
        if (file != null)
        {
            string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Resources/CoverImages");
            filePath = Path.Combine(directoryPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
        return filePath;
    }
}
}
