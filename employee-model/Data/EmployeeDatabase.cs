namespace employee_model;

public class EmployeeDatabase
{
    public EmployeeDatabase()
    {
        Employees = new List<Employee>();
    }
    public List<Employee> Employees { get; set; }
}
