using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.WebApi.Models {
  public class CarInputModel {
    public string Model { get; set; }
    public string Color { get; set; }
    public string Name { get; set; }
    public int MaxKm { get; set; }
    public int ModelReleaseYear { get; set; }
    public string Manufacturer { get; set; }
    public byte[] Image { get; set; }
  }
}
