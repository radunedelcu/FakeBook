using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Entities {
  public class BaseEntity {
    [Column("id")]
    public int Id { get; set; }

    [Column("deleted")]
    public bool IsDeleted { get; set; }
  }

  public class TrackableEntity : BaseEntity {
    [Column("created_date")]
    public DateTime CreatedDate { get; set; }

    [Column("updated_date")]
    public DateTime? UpdatedDate {
      get; set;
    }
  }
}
