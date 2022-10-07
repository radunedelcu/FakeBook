using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Models.Requests.Commands.Message {
  public class RequestMessageUpdateModel {
    public int MessageId { get; set; }
    public string? Message { get; set; }
    public IFormFile? Image { get; set; }
  }
}
