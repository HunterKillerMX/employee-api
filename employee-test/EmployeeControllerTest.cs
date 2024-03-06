using employee_facade.Controllers;
using employee_infrastructure;
using employee_model;
using employee_config;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipes;

namespace employee_test;

public class EmployeeControllerTest
{
    [Fact]
    public async void TestGet()
    {
        var mockLogJson = new Mock<ILogger<EmployeeJsonTool>>();
        var logJson = mockLogJson.Object;
        var mockLogRepo = new Mock<ILogger<EmployeeRepository>>();
        var logRepo = mockLogRepo.Object;

        var config = new EmployeeApiConfig();
        var json = new EmployeeJsonTool(logJson, config);
        var data = new EmployeeDatabase();
        var repo = new EmployeeRepository(logRepo, data, json);
        var controller = new EmployeeController(repo);

        var result = await controller.GetEmployee();
        Assert.IsType<ActionResult<List<EmployeeReadDTO>>>(result);
    }

    [Fact]
    public async void TestGetByID()
    {
        var mockLogJson = new Mock<ILogger<EmployeeJsonTool>>();
        var logJson = mockLogJson.Object;
        var mockLogRepo = new Mock<ILogger<EmployeeRepository>>();
        var logRepo = mockLogRepo.Object;

        var config = new EmployeeApiConfig();
        var json = new EmployeeJsonTool(logJson, config);
        var data = new EmployeeDatabase();
        data.Employees.Append(new Employee(){
            Id = Guid.Parse("aace4191-2f7d-4108-b842-a4fb0326e8c7"),
            Name = "John the Baptist"
        });
        var repo = new EmployeeRepository(logRepo, data, json);

        var result = await repo.ReadEmployeeById(Guid.Parse("aace4191-2f7d-4108-b842-a4fb0326e8c7"));
        Assert.IsType<EmployeeReadDTO>(result);
    }
}