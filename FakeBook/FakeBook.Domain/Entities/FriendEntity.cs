using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Entities {
  public class FriendEntity : TrackableEntity {
    public int User1Id { get; set; }

    public int User2Id { get; set; }
    public bool IsAccepted { get; set; }
    public UserEntity User { get; set; }
  }
}
