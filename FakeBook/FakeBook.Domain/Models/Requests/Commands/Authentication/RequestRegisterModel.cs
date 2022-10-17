using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Models.Requests.Commands.Authentication {
  public class RequestRegisterModel {
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public IFormFile? ProfilePicture { get; set; }
    public IFormFile? CoverImage { get; set; }
    public string? Quote { get; set; }
  }
}
