using App.Entities;
using App.WebApi.Business.Util;
using App.WebApi.Models;
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
    public async ValueTask<User> Login(UserLoginModel loginModel) {
      if(loginModel.UserLogin == null || loginModel.Password == null) {
        return null;
      }
      if(loginModel.UserLogin.Equals("") || loginModel.Password.Equals("")) {
        return null;
      }
      loginModel.Password = loginModel.UserLogin + loginModel.Password.EncodePassword();

      var userAuthenticated = await _repository.GetAllAsync(
        filter: x => x.Login == loginModel.UserLogin && x.Password == loginModel.Password,
        noTracking: false
        );
      return userAuthenticated.FirstOrDefault();
    }

    public async ValueTask<User> Register(User entity) {
      if(entity == null) {
        return null;
      }
      var password = entity.Login + entity.Password.EncodePassword();
      entity.Password = password;

      await _repository.AddAsync(entity);
      await _repository.CommitAsync();
      return entity;
    }
  }
}
