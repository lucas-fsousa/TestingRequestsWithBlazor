using System;
using System.Collections.Generic;
using System.Linq;
using App.WebApi.Models;
using System.Threading.Tasks;

namespace App.WebApi.Business.UserDefinition {
  public interface IUserService<TEntity> where TEntity : class {
    public ValueTask<TEntity> Login(UserLoginModel loginModel);
    public ValueTask<TEntity> Register(TEntity entity);
  }
}
