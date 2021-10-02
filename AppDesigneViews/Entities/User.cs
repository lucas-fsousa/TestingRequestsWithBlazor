using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppDesigneViews.Entities {
  public class User {
    public int Id { get; set; }

    [Required(ErrorMessage = "Necessary Field!")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Necessary Field!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public bool Authenticated { get; set; }
  }
}
