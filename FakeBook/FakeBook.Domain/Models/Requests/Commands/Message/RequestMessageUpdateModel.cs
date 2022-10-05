using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Models.Requests.Commands.Message {
  public class RequestMessageUpdateModel {
    [Required(ErrorMessage = "Please type a message.")]
    [MaxLength(2048)]
    public string Message { get; set; }
    public string ImagePath { get; set; }
  }
}
