using API.Tools;
using DataAccess.Models;
using DataTransfer.DTO.Common;
using DataTransfer.DTO.Employees;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;

namespace API.Controllers
{
    public class EmployeesController : BaseController
    {
        private const string _controller = "Employees";
        private readonly IEmployeeService _employees;
        private readonly LinkGenerator _linkGenerator;

        public EmployeesController(IEmployeeService employees, LinkGenerator linkGenerator)
        {
            _employees = employees;
            _linkGenerator = linkGenerator;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmployeeCreationDTO employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var response = new Response
                    {
                        Status = ResponseStatus.Error,
                        Messages = ValidationManager.ErrorMessages(ModelState),
                        Data = employee
                    };

                    return BadRequest(response);
                }

                var id = await _employees.CreateAsync(employee);
                var createdEmployee = await _employees.GetAsync(id);

                var uri = _linkGenerator.GetUriByAction(
                    HttpContext,
                    action: nameof(Get),
                    controller: _controller,
                    values: new { id }
                );

                return Created(uri, createdEmployee);
            }
            catch(Exception ex)
            {
                var response = DataTransfer.DTO.Common.Response.FromException(ex, employee);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] EmployeeUpdateDTO employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var response = new Response
                    {
                        Status = ResponseStatus.Error,
                        Messages = ValidationManager.ErrorMessages(ModelState),
                        Data = employee
                    };

                    return BadRequest(response);
                }

                if (!_employees.Exists(id))
                {
                    var response = new Response
                    {
                        Status = ResponseStatus.Error,
                        Messages = new List<string> { "Employee not found!" }
                    };

                    return NotFound(response);
                }

                await _employees.UpdateAsync(id, employee);
                var updatedEmployee = await _employees.GetAsync(id);

                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                var response = DataTransfer.DTO.Common.Response.FromException(ex, employee);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            try
            {
                var employee = await _employees.GetAsync(id);

                if (employee == null)
                {
                    var response = new Response
                    {
                        Status = ResponseStatus.Error,
                        Messages = new List<string> { "Employee not found!" }
                    };

                    return NotFound(response);
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                var response = DataTransfer.DTO.Common.Response.FromException(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] EmployeeFilterDTO filter)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    var response = new Response
                    {
                        Status = ResponseStatus.Error,
                        Messages = ValidationManager.ErrorMessages(ModelState),
                        Data = filter
                    };

                    return BadRequest(response);
                }

                var employees = await _employees.GetAsync(filter);

                return Ok(employees);
            }
            catch (Exception ex)
            {
                var response = DataTransfer.DTO.Common.Response.FromException(ex, filter);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                if (!_employees.Exists(id))
                {
                    var response = new Response
                    {
                        Status = ResponseStatus.Error,
                        Messages = new List<string> { "Employee not found!" }
                    };

                    return NotFound(response);
                }

                _employees.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                var response = DataTransfer.DTO.Common.Response.FromException(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
