using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Entities {
  public class MessageEntity : TrackableEntity {
    [MaxLength(2048)]
    [Column("message")]
    public string Message { get; set; }

    [Column("image")]
    public ICollection<ImageEntity> Images {
      get; set;
    }
    public UserEntity User { get; set; }
    [Column("userId")]
    public int UserId {
      get; set;
    }
  }
}
