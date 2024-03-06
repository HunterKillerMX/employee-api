using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using employee_infrastructure;
using employee_model;
using Microsoft.AspNetCore.Cors;

namespace employee_facade.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [EnableCors("DefaultCorsPolicy")]
        [HttpGet]
        public async Task<ActionResult<List<EmployeeReadDTO>>> GetEmployee ()
        {
            var response = await _employeeRepository.ReadEmployees();
            return response;
        }

        [EnableCors("DefaultCorsPolicy")]
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeReadDTO>> GetEmployeeById (Guid id)
        {
            var response = await _employeeRepository.ReadEmployeeById(id);
            return response;
        }

        [EnableCors("DefaultCorsPolicy")]
        [HttpPost]
        public async Task<ActionResult<bool>> PostEmployee (EmployeeWriteDTO employeeDto)
        {
            var response = await _employeeRepository.CreateEmployee(employeeDto);
            return response;
        }

        [EnableCors("DefaultCorsPolicy")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutEmployee (Guid id, EmployeeWriteDTO employeeDto)
        {
            var response = await _employeeRepository.UpdateEmployeeById(id, employeeDto);
            return response;
        }

        [EnableCors("DefaultCorsPolicy")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteEmployee (Guid id)
        {
            var response = await _employeeRepository.DeleteEmployeeById(id);
            return response;
        }
    }
}