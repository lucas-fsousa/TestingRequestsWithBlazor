using App.Entities;
using App.WebApi.Business.Util;
using Server.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.WebApi.Business.UserDefinition {
  public class UserService<TEntity> : IUserService<User> {
    private readonly IGenericRepository<User> _repository;
    public UserService(IGenericRepository<User> generic) {
      _repository = generic;
    }
    public async ValueTask<User> Login(string user, string password) {
      if(user.Equals("") || password.Equals("")) {
        return null;
      }
      password = user + password.EncodePassword();

      var userAuthenticated = await _repository.GetAllAsync(
        filter: x=> x.Login == user && x.Password == password,
        noTracking: false
        );
      return userAuthenticated.FirstOrDefault();
    }

    public async Task Register(User entity) {
      if(entity == null) {
        return;
      }
      var password = entity.Login + entity.Password.EncodePassword();
      entity.Password = password;

      await _repository.AddAsync(entity);
      await _repository.CommitAsync();
    }
  }
}
