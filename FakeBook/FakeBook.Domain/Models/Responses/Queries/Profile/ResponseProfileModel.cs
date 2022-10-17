using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Domain.Models.Responses.Queries.Profile
{
public class ResponseProfileModel
{
    public string Name { get; set; }
    public string? Quote { get; set; }
    public string? ProfilePicture { get; set; }
    public string? CoverImage { get; set; }
}
}
