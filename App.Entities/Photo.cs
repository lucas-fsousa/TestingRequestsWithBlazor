using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities {
  public class Photo {
    public int Id { get; set; }
    public int CarId { get; set; }
    public string Url { get; set; }
  }
}
