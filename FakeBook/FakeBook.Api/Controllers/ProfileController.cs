using FakeBook.Contracts.Commands;
using FakeBook.Domain.Models.Requests.Commands.Profile;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FakeBook.Api.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase

{
    private readonly IProfileCommand _profileCommand;
    public ProfileController(IProfileCommand profileCommand)
    {
        _profileCommand = profileCommand;
    }
    [HttpPost("EditProfile")]
    public async Task<IActionResult> EditProfile([FromForm] RequestProfileUpdateModel updatedProfile)
    {
        var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (data == null)
        {
            return NotFound();
        }
        var newData = Convert.ToInt32(data.Value);

        await _profileCommand.EditProfile(newData, updatedProfile.Name, updatedProfile.Quote,
                                          updatedProfile.ProfilePicture, updatedProfile.CoverImage);
        return NoContent();
    }
}
}
