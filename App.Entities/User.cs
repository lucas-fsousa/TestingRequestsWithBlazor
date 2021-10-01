using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities {
  public class User {
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public bool Authenticated { get; set; }
  }
}
