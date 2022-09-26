using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Models.Requests.Commands.Authentication {
  public class RequestRegisterModel {
    public string Name { get; set; }
    public int Email { get; set; }
    public int Password { get; set; }
    public int ConfirmPassword { get; set; }
  }
}
