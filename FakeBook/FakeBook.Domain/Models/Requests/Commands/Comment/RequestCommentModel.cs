using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Models.Requests.Commands.Comment {
  public class RequestCommentModel {
    [MaxLength(2048)]
    [Required]
    public string Content { get; set; } = string.Empty;
  }
}
