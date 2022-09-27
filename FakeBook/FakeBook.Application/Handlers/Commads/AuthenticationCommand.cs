using FakeBook.Application.Common.Interfaces;
using FakeBook.Application.Handlers.Queries.Services;
using FakeBook.Contracts.Commands;
using FakeBook.Contracts.Queries.Services;
using FakeBook.Domain.Entities;
using FakeBook.Domain.Enums;
using FakeBook.Domain.Models.Requests.Commands.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Application.Handlers.Commads {
  public class AuthenticationCommand : IAuthenticationCommand {
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IJwtQueryService _jwtQueryService;

    public AuthenticationCommand(IApplicationDbContext applicationDbContext,
                                 IJwtQueryService jwtQueryService) {
      _applicationDbContext = applicationDbContext;
      _jwtQueryService = jwtQueryService;
    }

    public async Task<string> Register(RequestRegisterModel requestRegisterModel) {
      try {
        var accountAlreadyExists = await _applicationDbContext.Users.AsNoTracking().AnyAsync(
            entity => entity.Name == requestRegisterModel.Name.Trim().ToLower() ||
                      entity.Email == requestRegisterModel.Email.Trim().ToLower());

        if (accountAlreadyExists == true) {
          return "Username or email already exists!";
        }

        ComputeHashPassword(requestRegisterModel.Password, out byte[] keyPassword,
                            out byte[] hashPassword);

        var accountEntity = new UserEntity() { Name = requestRegisterModel.Name.Trim().ToLower(),
                                               Email = requestRegisterModel.Email.Trim().ToLower(),
                                               HashPassword = hashPassword,
                                               KeyPassword = keyPassword,
                                               RoleId = (int)RoleEnum.User,
                                               CreatedDate = DateTime.Now };

        _applicationDbContext.Users.Add(accountEntity);
        var isSavedSuccessfully = await _applicationDbContext.SaveChangesAsync() > 0;

        return isSavedSuccessfully
                   ? _jwtQueryService.GetJwtToken(accountEntity.Email, accountEntity.Id)
                   : string.Empty;
      } catch (Exception) {
        throw;
      }
    }

    private void ComputeHashPassword(string password,
                                     out byte[] keyPassword,
                                     out byte[] hashPassword) {
      using var hmac = new System.Security.Cryptography.HMACSHA512();

      keyPassword = hmac.Key;
      hashPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
  }
}
