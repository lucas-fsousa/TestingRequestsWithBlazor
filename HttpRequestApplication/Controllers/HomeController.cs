using App.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.WebApi.Business.CarDefinition;

namespace App.WebApi.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class HomeController : ControllerBase {
    private readonly ICarService<Car> _definition;
    private string InternalError { get; } = "The request was not completed due to an internal error on the server side.";
    public HomeController(ICarService<Car> carService) {
      _definition = carService;
    }
    [HttpGet, Route("getById/{id}")]
    public async Task<IActionResult> GetById(int id) {
      try {
        var result = await _definition.GetAsyncByKey(id);
        return StatusCode(200, result);
      } catch(Exception) {
        return StatusCode(500, "");
      }
    }

    [HttpGet, Route("getAll/{currentPage}")]
    public async Task<IActionResult> GetAll(int currentPage) {
      try {
       var result = await _definition.GetAllAsync(currentPage);
        return StatusCode(200, result);
      } catch(Exception) {
        return StatusCode(500, InternalError);
      }
    }

    [HttpPost, Route("insert")]
    public async Task<IActionResult> Insert([FromBody] Car car) {
      try {
        var result = await _definition.AddAsync(car);
        return StatusCode(201, "A new resource was successfully created.");
      } catch(Exception ex) {
        return StatusCode(500, InternalError);
      }
    }


  }
}
