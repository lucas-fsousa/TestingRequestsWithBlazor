using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.WebApi.Business.UserDefinition {
  public interface IUserService<TEntity> where TEntity : class {
    public ValueTask<TEntity> Login(string user, string password);
    public Task Register(TEntity entity);
  }
}
