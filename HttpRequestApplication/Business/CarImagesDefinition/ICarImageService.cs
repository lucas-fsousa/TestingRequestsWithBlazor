using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.WebApi.Business.CarImagesDefinition {
  public interface ICarImageService<TEntity> where TEntity : class {
    public ValueTask<TEntity> UploadAsync(TEntity entity);
  }
}
