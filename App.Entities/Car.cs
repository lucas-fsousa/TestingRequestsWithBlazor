using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities {
  public class Car {
    public int Id { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public string Name { get; set; }
    public int MaxKm { get; set; }
    public int ModelReleaseYear { get; set; }
    public string Manufacturer { get; set; }

    public int PhotoId { get; set; }
    public byte[] Image { get; set; }
    public List<Photo> Photos { get; set; }
  }
}
