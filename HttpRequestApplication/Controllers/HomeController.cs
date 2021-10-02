using App.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.WebApi.Business.CarDefinition;
using Server.Infrastructure.APIConfiguration;
using Swashbuckle.AspNetCore.Annotations;

namespace App.WebApi.Controllers {
  [Route("api/v1/[controller]")]
  [ApiController]
  public class HomeController : ControllerBase {
    private readonly ICarService<Car> _definition;

    public HomeController(ICarService<Car> carService) {
      _definition = carService;
    }

    /// <summary>
    /// Search a user for the input key
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [SwaggerResponse(statusCode: 200, description: "Request completed successfully.", Type = typeof(Car))]
    [SwaggerResponse(statusCode: 500, description: "The request was not completed due to an internal error on the server side.")]
    [SwaggerResponse(statusCode: 204, description: "Item not found. Possible reasons: Does not exist, is blocked or is in use.")]

    [HttpGet, Route("getById/{id}")]
    public async Task<IActionResult> GetById([FromRoute]int id) {
      try {
        var result = await _definition.GetAsyncByKey(id);
        if(result == null) {
          return StatusCode(204, HttpCodeMessage.Code204);
        }
        return StatusCode(200, result);
      } catch(Exception) {
        return StatusCode(500, HttpCodeMessage.Code500);
      }
    }

    /// <summary>
    /// Search the server and return a users page corresponding to the input value.
    /// </summary>
    /// <param name="currentPage"></param>
    /// <returns></returns>
    [SwaggerResponse(statusCode: 200, description: "Request completed successfully.", Type = typeof(List<Car>))]
    [SwaggerResponse(statusCode: 500, description: "The request was not completed due to an internal error on the server side.")]
    [SwaggerResponse(statusCode: 204, description: "Item not found. Possible reasons: Does not exist, is blocked or is in use.")]
    [HttpGet, Route("getAll/{currentPage}")]
    public async Task<IActionResult> GetAll(int currentPage) {
      try {
       var result = await _definition.GetAllAsync(currentPage);
        if(result.Count() == 0) {
          return StatusCode(204, HttpCodeMessage.Code204);
        }
        return StatusCode(200, result);
      } catch(Exception ex) {
        return StatusCode(500, HttpCodeMessage.Code500);
      }
    }

    /// <summary>
    /// Add a new user
    /// </summary>
    /// <param name="car"></param>
    /// <returns></returns>
    [SwaggerResponse(statusCode: 200, description: "Request completed successfully.", Type = typeof(List<Car>))]
    [SwaggerResponse(statusCode: 500, description: "The request was not completed due to an internal error on the server side.")]
    [HttpPost, Route("insert")]
    public async Task<IActionResult> Insert([FromBody]Car car) {
      try {
        var result = await _definition.AddAsync(car);
        return StatusCode(201, HttpCodeMessage.Code201);
      } catch(Exception ex) {
        return StatusCode(500, HttpCodeMessage.Code500);
      }
    }

    /// <summary>
    /// Update an existing item
    /// </summary>
    /// <param name="car"></param>
    /// <returns></returns>
    [SwaggerResponse(statusCode: 200, description: "Request completed successfully.", Type = typeof(List<Car>))]
    [SwaggerResponse(statusCode: 500, description: "The request was not completed due to an internal error on the server side.")]
    [SwaggerResponse(statusCode: 400, description: "The request to the server was not terminated properly. Try again.")]
    [HttpPut, Route("update")]
    public async Task<IActionResult> Update([FromBody]Car car) {
      try {
        var exist = _definition.GetAsyncByKey(car.Id);
        if(exist.Result == null) {
          return StatusCode(400, HttpCodeMessage.Code400);
        }
        await _definition.Update(car);
        return StatusCode(200, HttpCodeMessage.Code200);
      } catch(Exception ex) {
        return StatusCode(500, HttpCodeMessage.Code500);
      }
    }

    /// <summary>
    /// Deletes an existing item linked to the given identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [SwaggerResponse(statusCode: 200, description: "Request completed successfully.")]
    [SwaggerResponse(statusCode: 500, description: "The request was not completed due to an internal error on the server side.")]
    [SwaggerResponse(statusCode: 204, description: "Item not found. Possible reasons: Does not exist, is blocked or is in use.")]
    [HttpDelete, Route("delete/{id}")]
    public async Task<IActionResult> Delete(int id) {
      try {
        var exist = await _definition.GetAsyncByKey(id);
        if(exist == null) {
          return StatusCode(204, HttpCodeMessage.Code204);
        }
        _definition.Delete(id);
        return StatusCode(200, HttpCodeMessage.Code200);
      } catch(Exception ex) {
        return StatusCode(500, HttpCodeMessage.Code500);
      }
    }


  }
}
