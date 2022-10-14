using FakeBook.Application.Common.Interfaces;
using FakeBook.Contracts.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Application.Handlers.Commads
{
public class ProfileCommand : IProfileCommand
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IAuthenticationCommand _authenticationCommand;

    public ProfileCommand(IApplicationDbContext applicationDbContext, IAuthenticationCommand authenticationCommand)
    {
        _applicationDbContext = applicationDbContext;
        _authenticationCommand = authenticationCommand;
    }
    public async Task<bool> EditProfile(int userId, string name, string quote, IFormFile profilePicture,
                                        IFormFile coverImage)
    {
        var user = await _applicationDbContext.Users.FindAsync(userId);

        if (user == null)
        {
            throw new Exception($"User {userId} was not found.");
        }

        user.Quote = quote ?? user.Quote;
        user.Name = name ?? user.Name;
        user.ProfilePicture = _authenticationCommand.UploadProfilePicture(profilePicture) ?? user.ProfilePicture;
        user.CoverImage = _authenticationCommand.UploadCoverImage(coverImage) ?? user.CoverImage;

        return await _applicationDbContext.SaveChangesAsync() > 0;
    }
}
}
