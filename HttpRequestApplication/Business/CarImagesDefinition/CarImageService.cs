using App.Entities;
using Server.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.WebApi.Business.CarImagesDefinition {
  public class CarImageService<TEntity> : ICarImageService<Photo> {
    private readonly IGenericRepository<Photo> _repository;
    public CarImageService(IGenericRepository<Photo> repository) {
      _repository = repository;
    }
    public async ValueTask<Photo> UploadAsync(Photo entity) {
      try {
        var result = await _repository.AddAsync(entity);
        await _repository.CommitAsync();
        return result;
      } catch(Exception ex) {
        return null;
      }
    }
  }
}
