using AppDesigneViews.Entities;
using AppDesigneViews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDesigneViews.ServicesController.UserController {
  public interface IUserRequestService {
    public Task Create(User user);
    public ValueTask<User> LogOn(UserLoginModel loginModel);
  }
}
