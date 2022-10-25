using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Models.Responses.Queries.Authentication {
  public class ResponseLoginModel {
    public int Id { get; set; }
    public string Token { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Quote { get; set; }
    public string? ProfilePicture { get; set; }
    public string? CoverImage { get; set; }
  }
}
