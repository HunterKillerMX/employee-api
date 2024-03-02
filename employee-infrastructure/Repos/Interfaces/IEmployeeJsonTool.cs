using employee_model;

namespace employee_infrastructure;

public interface IEmployeeJsonTool
{
    Task<List<Employee>> ReadEmployeesJsonFile();
    Task<bool> WriteEmployeesJsonFile(List<Employee> employees);
}
