using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppDesigneViews.Entities {
  public class Car {
    public int Id { get; set; }

    [Required]
    public string Model { get; set; }

    [Required]
    public string Color { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [Range(0, 999)]
    public int MaxKm { get; set; }

    [Required]
    public int ModelReleaseYear { get; set; }

    [Required]
    public string Manufacturer { get; set; }
    
    public byte[] Image { get; set; }
    public List<Photo> Photos { get; set; }
  }
}
