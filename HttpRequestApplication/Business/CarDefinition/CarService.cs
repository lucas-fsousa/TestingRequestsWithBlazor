using App.Entities;
using Server.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace App.WebApi.Business.CarDefinition {
  public class CarService<TEntity>: ICarService<Car> {
    private readonly IGenericRepository<Car> _generic;
    private readonly IGenericRepository<Photo> _photo;
    private string Host { get; } = "localhost:44333";


    public CarService(IGenericRepository<Car> car, IGenericRepository<Photo> photo) {
      _generic = car;
      _photo = photo;
    }

    public async ValueTask<Car> AddAsync(Car entity) {
      entity.Manufacturer = entity.Manufacturer.ToUpper();
      entity.Color = entity.Color.ToUpper();
      entity.Model = entity.Model.ToUpper();
      entity.Name = entity.Name.ToUpper();
      var requestResult = await _generic.AddAsync(entity);
      await _generic.CommitAsync();


      // it checks if there is an image item to be saved, if true, the instruction block will save the image on the server and then will save the path referring to the image in the database.
      if(entity.Image != null) {
        using(var ms = new MemoryStream(entity.Image)) {
          var imageName = ($"{entity.Manufacturer}-{entity.Name}-{DateTime.Now.Ticks}.jpg");
          var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CarImages", imageName);

          var newPhoto = new Photo {
            CarId = requestResult.Id,
            Url = imageName
          };
          await _photo.AddAsync(newPhoto);
          await _photo.CommitAsync();
          Image.FromStream(ms).Save($"{physicalPath}");
        }
      }

      return requestResult;
    }

    public void Delete(int? key = 0, string? unique = null) {
      if(key > 0) {
        var entity = _generic.GetAllAsync(filter: x => x.Id == key, includeProperties: "Photos").Result.FirstOrDefault();
        if(entity != null) {
          _generic.Delete(entity);
          _generic.CommitAsync();
        }

        // It will be responsible for deleting all images from the server that are linked to the entity
        foreach(var image in entity.Photos) {
          var physicalImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CarImages", image.Url);
          File.Delete(physicalImage);
        }
      }
    }

    public async Task<IEnumerable<Car>> GetAllAsync(int currentPage) {
      var MaxPerPage = 10;
      var skipCurrent = (currentPage - 1) * MaxPerPage;
      if(currentPage < 1) {
        currentPage = 1;
      }

      var list = await _generic.GetAllAsync(
        noTracking: false,
        take: MaxPerPage,
        skip: skipCurrent,
        includeProperties: "Photos"
        ).ConfigureAwait(false);
      foreach(var item in list) {
        foreach(var photo in item.Photos) {
          photo.Url = $"https://{Host}/carimages/{photo.Url}";
        }
      }
      return list;
    }

    public async ValueTask<Car> GetAsyncByKey(int? key = 0, string? unique = "") {
      if(key > 0) {
        return await _generic.GetByKeysAsync(keys: key);
      }
      return null;
    }

    public async Task Update(Car entity) {
      if(entity != null) {
        _generic.Update(entity);
        await _generic.CommitAsync();
      }
    }
  }
}
