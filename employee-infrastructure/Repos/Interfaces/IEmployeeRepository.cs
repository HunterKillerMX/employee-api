using employee_model;

namespace employee_infrastructure;

public interface IEmployeeRepository
{
    Task<bool> CreateEmployee(EmployeeWriteDTO employeeDTO);
    Task<List<EmployeeReadDTO>> ReadEmployees();
    Task<EmployeeReadDTO> ReadEmployeeById(Guid id);
    Task<bool> UpdateEmployeeById(Guid id, EmployeeWriteDTO employeeDTO);
    Task<bool> DeleteEmployeeById(Guid id);
}
