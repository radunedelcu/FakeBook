using FakeBook.Contracts.Commands;
using FakeBook.Contracts.Queries;
using FakeBook.Domain.Models.Requests.Commands.Authentication;
using FakeBook.Domain.Models.Requests.Queries.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakeBook.Api.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class AuthenticationController : ControllerBase {
    private readonly IAuthenticationCommand _authenticationCommand;
    private readonly IAuthenticationQuery _authenticationQuery;

    public AuthenticationController(IAuthenticationCommand authenticationCommand,
                                    IAuthenticationQuery authenticationQuery) {
      _authenticationCommand = authenticationCommand;
      _authenticationQuery = authenticationQuery;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RequestRegisterModel requestRegisterModel) {
      var result = await _authenticationCommand.Register(requestRegisterModel);
      if (result == null) {
        return BadRequest();
      }

      return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] RequestLoginModel requestLoginModel) {
      var loginModel = await _authenticationQuery.LoginIfUserExists(requestLoginModel);

      if (loginModel == null) {
        return NotFound();
      }

      return Ok(loginModel);
    }
  }
}
