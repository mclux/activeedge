using ActiveEdge.Core;
using ActiveEdge.Infrastructure.DTO;
using ActiveEdge.Infrastructure.Services;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ActiveEdge.API.Controllers
{
    [Authorize(Roles ="TESTER")]
    public class EmployeeController : ApiController
    {
        private EmployeeService _employeeService;
        public EmployeeController()
        {
            _employeeService = new EmployeeService();
        }

        /// <summary>
        /// Gets all employees
        /// </summary>
        /// <returns></returns>
        /// 
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<EmployeeDto>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(string))]
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            try
            {
                var result = _employeeService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ActiveEdgeConstants.GENERIC_ERROR_MSG);
            }
        }

        /// <summary>
        /// Gets specifit employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// 
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(EmployeeDto))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(string))]
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(string employeeId)
        {
            try
            {
                var result = _employeeService.Get(employeeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ActiveEdgeConstants.GENERIC_ERROR_MSG);
            }
        }

        /// <summary>
        /// Creates new employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// 
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(EmployeeDto))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(string))]
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create(EmployeeDto employee)
        {
            try
            {
                var result = _employeeService.Add(employee);

                if (result == null)
                {
                    return BadRequest($"Employee with ID-{employee.EmployeeId} already exist");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ActiveEdgeConstants.GENERIC_ERROR_MSG);
            }
        }

        /// <summary>
        /// Updates an employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// 
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(EmployeeDto))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(string))]
        [HttpPost]
        [Route("Update")]
        public IHttpActionResult Update(EmployeeDto employee)
        {
            try
            {
                var result = _employeeService.Update(employee);

                if(result==null)
                {
                    return BadRequest($"Employee with ID-{employee.EmployeeId} does not exist");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ActiveEdgeConstants.GENERIC_ERROR_MSG);
            }
        }

        /// <summary>
        /// Removes an employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// 
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(string))]
        [HttpGet]
        [Route("Delete")]
        public IHttpActionResult Delete(string employeeId)
        {
            try
            {
                var result = _employeeService.Delete(employeeId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ActiveEdgeConstants.GENERIC_ERROR_MSG);
            }
        }
    }
}
