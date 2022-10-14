using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Models.Requests.Commands.Profile
{
public class RequestProfileUpdateModel
{
    public string Name { get; set; }
    public string? Quote { get; set; }
    public IFormFile? ProfilePicture { get; set; }
    public IFormFile? CoverImage { get; set; }
}
}
