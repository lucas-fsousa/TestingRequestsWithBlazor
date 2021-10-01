using App.Entities;
using App.WebApi.Business.UserDefinition;
using App.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure.APIConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.WebApi.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase {
    private readonly IUserService<User> _user;
    public UserController(IUserService<User> user) {
      _user = user;
    }

    [HttpGet, Route("login")]
    public async Task<IActionResult> Login(UserLoginModel model) {
      try {
        var authenticated = await _user.Login(model);

        if(authenticated != null) {
          authenticated.Authenticated = true;
          authenticated.Password = "";
          return StatusCode(200, authenticated);
        }

        return StatusCode(404, HttpCodeMessage.Code404);
      } catch(Exception ex) {
        return StatusCode(500, HttpCodeMessage.Code500);
      }
    }
    
    [HttpPost, Route("register")]
    public async Task<IActionResult> Register([FromBody] User user) {
      try {
        var result = await _user.Register(user);
        if(result != null) {
          return StatusCode(201, HttpCodeMessage.Code201);
        }

        return StatusCode(400, HttpCodeMessage.Code400);
      } catch(Exception ex) {
        return StatusCode(500, HttpCodeMessage.Code500);
      }
    }
  }
}
