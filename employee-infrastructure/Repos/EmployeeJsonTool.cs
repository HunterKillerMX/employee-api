using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using employee_model;
using employee_config;

namespace employee_infrastructure;

public class EmployeeJsonTool : IEmployeeJsonTool
{
    private string _dataJsonFilePath;
    private ILogger<IEmployeeJsonTool> _logger;
    public EmployeeJsonTool(ILogger<IEmployeeJsonTool> logger, EmployeeApiConfig config)
    {
        _logger = logger;
        _dataJsonFilePath = config.DataJsonFilePath;
    }
    public async Task<List<Employee>> ReadEmployeesJsonFile()
    {
        List<Employee> employees = new List<Employee>();
        try
        {
            string jsonText = await File.ReadAllTextAsync(_dataJsonFilePath);

            employees = JsonConvert.DeserializeObject<List<Employee>>(jsonText) ?? new List<Employee>();
            _logger.LogInformation(@"JSON File {0} read succesfully. {1} items found", _dataJsonFilePath, employees.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(@"JSON File {0}, could not be read. Error message: {1}", _dataJsonFilePath, ex.Message);
        }

        return employees;
    }

    public async Task<bool> WriteEmployeesJsonFile(List<Employee> employees)
    {
        bool response = true;

        try
        {
            string jsonText = JsonConvert.SerializeObject(employees);
            await File.WriteAllTextAsync(_dataJsonFilePath, jsonText);
            _logger.LogInformation(@"JSON File {0} succesfully written. {1} items saved", _dataJsonFilePath, employees.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(@"Could not write in JSON file {0}. Error message: {1}", _dataJsonFilePath, ex.Message);
            response = false;
        }

        return response;
    }
}
