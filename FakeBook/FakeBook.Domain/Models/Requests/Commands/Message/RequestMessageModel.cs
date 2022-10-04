using FakeBook.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Models.Requests.Commands.Message {
  public class RequestMessageModel {
    public int UserId { get; set; }
    public string Message { get; set; }
    public IFormFile? Image { get; set; }
  }
}
