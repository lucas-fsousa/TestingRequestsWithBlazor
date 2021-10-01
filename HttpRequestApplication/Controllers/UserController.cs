using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.WebApi.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase {

    public async Task<IActionResult> Login() {
      await Task.Delay(1200);
      return Ok();
    }
    
    public async Task<IActionResult> Register() {
      await Task.Delay(1200);
      return Ok();
    }
  }
}
