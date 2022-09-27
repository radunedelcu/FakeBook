using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Entities {
  [Table("users")]
  public class UserEntity : TrackableEntity {
    [MaxLength(64)]
    [Column("name")]
    public string Name { get; set; }

    [MaxLength(128)]
    [Column("username")]
    public string Email {
      get; set;
    }

    [Column("key_password")]
    public byte[] KeyPassword {
      get; set;
    }

    [Column("hash_password")]
    public byte[] HashPassword {
      get; set;
    }
    public RoleEntity Role { get; set; }

    [Column("roleId")]
    public int RoleId {
      get; set;
    }
  }
}
