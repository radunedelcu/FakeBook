using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Contracts.Commands
{
public interface IProfileCommand
{
        Task<bool> EditProfile(int userId, string name, string quote, IFormFile profilePicture, IFormFile coverImage);
}
}
