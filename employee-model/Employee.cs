namespace employee_model;

public class Employee
{
    public Employee()
    {
    }

    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Position { get; set; }
    public DateTime HiringDate { get; set; }
    public double Salary { get; set; }
}

