using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Models.Responses.Queries.Comment {
  public class ResponseCommentModel {
    public string Content { get; set; }
    public string UserName { get; set; }
    public DateTime CreatedDate { get; set; }
  }
}
