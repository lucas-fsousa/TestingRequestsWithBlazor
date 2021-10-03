using App.Entities;
using App.WebApi.Business.CarDefinition;
using App.WebApi.Business.CarImagesDefinition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure.APIConfiguration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace App.WebApi.Controllers {
  [Route("api/v1/[controller]")]
  [ApiController]
  public class CarImagesController : ControllerBase {
    private readonly ICarImageService<Photo> _service;
    private readonly ICarService<Car> _carService;
    public CarImagesController(ICarImageService<Photo> service, ICarService<Car> carService) {
      _service = service;
      _carService = carService;
    }

    [HttpPost, Route("upload/{id}")]
    public async Task<IActionResult> Upload([FromRoute] int id, List<IFormFile> images) {
      try {
        var carExist = await _carService.GetAsyncByKey(key: id);
        if(images == null || images.Count < 1 || carExist == null) {
          return StatusCode(400, HttpCodeMessage.Code400);
        }

        // checks the format of the files entered.
        foreach(var image in images) {
          string[] contentTypes = { "image/jpeg", "image/jpg", "image/png" };
          if(!contentTypes.Contains(image.ContentType)) {
            return StatusCode(400, "there are files with an invalid format. Valid formats (jpeg, jpg, png)");
          }
        }

        foreach(var image in images) {
          var pathForDatabase = Path.Combine($"{DateTime.Now.Ticks.ToString() + id}-{image.FileName}");
          var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CarImages", pathForDatabase);
          var newPhoto = new Photo {
            CarId = id,
            Url = pathForDatabase
          };
          var result = await _service.UploadAsync(newPhoto);
          if(result == null) {
            return StatusCode(400, HttpCodeMessage.Code400);
          }
          using(var stream = new FileStream(path, FileMode.Create)) {
            await image.CopyToAsync(stream);
          }
        }
        return StatusCode(201, HttpCodeMessage.Code201);
      } catch(Exception ex) {
        return StatusCode(500, HttpCodeMessage.Code500);
      }
    }
  }
}
