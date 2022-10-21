using FakeBook.Application.Common.Interfaces;
using FakeBook.Application.Handlers.Queries.Services;
using FakeBook.Contracts.Queries;
using FakeBook.Contracts.Queries.Services;
using FakeBook.Domain.Models.Requests.Queries.Authentication;
using FakeBook.Domain.Models.Responses.Queries.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Application.Handlers.Queries {
  public class AuthenticationQuery : IAuthenticationQuery {
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IJwtQueryService _jwtQueryService;

    public AuthenticationQuery(IApplicationDbContext applicationDbContext,
                               IJwtQueryService jwtQueryService) {
      _applicationDbContext = applicationDbContext;
      _jwtQueryService = jwtQueryService;
    }

    public async Task<ResponseLoginModel> LoginIfUserExists(RequestLoginModel requestLoginModel) {
      try {
        var accountEntity = await _applicationDbContext.Users.AsNoTracking().FirstOrDefaultAsync(
            entity => entity.Email == requestLoginModel.Email.Trim().ToLower());

        if (accountEntity != null &&
            IsPasswordCorrect(requestLoginModel.Password, accountEntity.KeyPassword,
                              accountEntity.HashPassword)) {
          return new ResponseLoginModel() { Token = _jwtQueryService.GetJwtToken(
                                                accountEntity.Email, accountEntity.Id),
                                            Name = accountEntity.Name,
                                            Email = accountEntity.Email,
                                            Quote = accountEntity.Quote,
                                            ProfilePicture = accountEntity.ProfilePicture,
                                            CoverImage = accountEntity.CoverImage };
        }

        return null;
      } catch (Exception) {
        throw;
      }
    }

    private bool IsPasswordCorrect(string password, byte[] keyPassword, byte[] hashedPassword) {
      using (var hmac = new System.Security.Cryptography.HMACSHA512(keyPassword)) {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        for (int i = 0; i < computedHash.Length; i++) {
          if (computedHash[i] != hashedPassword[i]) {
            return false;
          }
        }

        return true;
      }
    }
  }
}
