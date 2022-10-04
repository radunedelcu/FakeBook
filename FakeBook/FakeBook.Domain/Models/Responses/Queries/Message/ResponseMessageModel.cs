using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Models.Responses.Queries.Message {
  public class ResponseMessageModel {
    public string Message { get; set; }
    public string FileType { get; set; }
    public byte[] Image { get; set; }
    public DateTime CreatedDate { get; set; }
  }
}
