using FakeBook.Contracts.Queries.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Application.Handlers.Queries.Services {
  public class JwtQueryService : IJwtQueryService {
    private readonly IConfiguration _configuration;

    public JwtQueryService(IConfiguration configuration) { _configuration = configuration; }

    public string GetJwtToken(string username, long id) {
      try {
        Claim[] claims = new[] {
          new Claim(ClaimTypes.NameIdentifier, id.ToString()),
          new Claim(ClaimTypes.Name, username),
        };

        var symmetricSecurityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));
        var signingCredentials =
            new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        var securityTokenDescriptor = new SecurityTokenDescriptor {
          Subject = new ClaimsIdentity(claims),
          Expires = DateTime.Now.AddDays(1),


          SigningCredentials = signingCredentials,
        };

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

        return jwtSecurityTokenHandler.WriteToken(securityToken);
      } catch (Exception) {
        throw;
      }
    }
  }
}
