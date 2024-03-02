using Microsoft.Extensions.Logging;

using employee_model;

namespace employee_infrastructure;

public class EmployeeRepository : IEmployeeRepository
{
    private ILogger<IEmployeeRepository> _logger;
    private EmployeeDatabase _employeeDatabase;
    private IEmployeeJsonTool _employeeJsonTool;
    public EmployeeRepository(ILogger<IEmployeeRepository> logger, EmployeeDatabase employeeDatabase, IEmployeeJsonTool employeeJsonTool)
    {
        _logger = logger;
        _employeeDatabase = employeeDatabase;
        _employeeJsonTool = employeeJsonTool;
    }

    public async Task<bool> CreateEmployee(EmployeeWriteDTO employeeDTO)
    {
        _employeeDatabase.Employees.Add(new Employee(){
            Id = Guid.NewGuid(),
            Name = employeeDTO.Name,
            Position = employeeDTO.Position,
            HiringDate = employeeDTO.HiringDate,
            Salary = employeeDTO.Salary
        });
        var response = await _employeeJsonTool.WriteEmployeesJsonFile(_employeeDatabase.Employees);
        return response;
    }

    public async Task<bool> DeleteEmployeeById(Guid id)
    {   
        var item =  _employeeDatabase.Employees.Where(x => x.Id == id).FirstOrDefault();
        _employeeDatabase.Employees.Remove(item ?? new Employee());
        var response = await _employeeJsonTool.WriteEmployeesJsonFile(_employeeDatabase.Employees);
        return response;
    }

    public async Task<EmployeeReadDTO> ReadEmployeeById(Guid id)
    {
        _employeeDatabase.Employees = await _employeeJsonTool.ReadEmployeesJsonFile();
        var item = _employeeDatabase.Employees.Where(x => x.Id == id).FirstOrDefault() ?? new Employee();
        return new EmployeeReadDTO(item.Id, item.Name, item.Position, item.HiringDate, item.Salary);
    }


    public async Task<List<EmployeeReadDTO>> ReadEmployees()
    {
        _employeeDatabase.Employees = await _employeeJsonTool.ReadEmployeesJsonFile();
        var items = _employeeDatabase.Employees.Select(x => new EmployeeReadDTO(x.Id, x.Name, x.Position, x.HiringDate, x.Salary)).ToList();
        return items;
    }

    public async Task<bool> UpdateEmployeeById(Guid id, EmployeeWriteDTO employeeDTO)
    {
        if(_employeeDatabase.Employees.Where(x => x.Id == id).Any())
        {
            _employeeDatabase.Employees.Where(x => x.Id == id).FirstOrDefault()!.Name = employeeDTO.Name;
            _employeeDatabase.Employees.Where(x => x.Id == id).FirstOrDefault()!.Position = employeeDTO.Position;
            _employeeDatabase.Employees.Where(x => x.Id == id).FirstOrDefault()!.HiringDate = employeeDTO.HiringDate;
            _employeeDatabase.Employees.Where(x => x.Id == id).FirstOrDefault()!.Salary = employeeDTO.Salary;
        }
        var response = await _employeeJsonTool.WriteEmployeesJsonFile(_employeeDatabase.Employees);
        return response;
    }
}
