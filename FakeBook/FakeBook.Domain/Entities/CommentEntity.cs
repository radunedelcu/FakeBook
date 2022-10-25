using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Entities {
  public class CommentEntity : TrackableEntity {
    public CommentEntity() {
      CreatedDate = DateTime.Now;
    }
    [Required]
    [MaxLength(2048)]
    [Column("content")]
    public string Content { get; set; }
    [Column("userId")]
    public int UserId {
      get; set;
    }
    [Column("messageId")]
    public int MessageId {
      get; set;
    }
    public MessageEntity Message { get; set; }
  }
}
