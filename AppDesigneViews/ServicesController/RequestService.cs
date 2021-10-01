using AppDesigneViews.Models;
using System;
using AppDesigneViews.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDesigneViews.ServicesController {
  public class RequestService : IRequestService {
    public RequestService() {

    }

    public async ValueTask<User> Create(User user) {
      throw new NotImplementedException();
    }

    public async ValueTask<User> LogOn(UserLoginModel loginModel) {
      return new User();
    }
  }
}
