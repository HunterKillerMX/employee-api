using employee_config;
using employee_model;
using employee_infrastructure;

var corspolicyName = "DefaultCorsPolicy";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corspolicyName,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200");
                      });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var section = builder.Configuration.GetSection("EmployeeApiConfig");
var employeeApiConfig = section.Get<EmployeeApiConfig>();


builder.Services.AddSingleton(employeeApiConfig ?? new EmployeeApiConfig());
builder.Services.AddSingleton<EmployeeDatabase>();
builder.Services.AddTransient<IEmployeeJsonTool, EmployeeJsonTool>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
// builder.Services.AddTransient<EmployeeController>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.Run();
