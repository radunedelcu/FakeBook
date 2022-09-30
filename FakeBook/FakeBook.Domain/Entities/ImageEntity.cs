using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Entities {
  public class ImageEntity {
    public int Id { get; set; }
    public string Name { get; set; }
    public int MessageId { get; set; }
    public string FileType { get; set; }
    public byte[] DataFile { get; set; }
  }
}
