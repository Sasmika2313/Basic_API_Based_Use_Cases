using FoodMenuConsole.Data;
using FoodMenuConsole.Menus;
using FoodMenuConsole.Repositories;
using FoodMenuConsole.Services;
using Microsoft.EntityFrameworkCore;

var connectionString =
    @"Server=.\SQLEXPRESS;Database=FoodMenuConsoleDb;Trusted_Connection=True;TrustServerCertificate=True;";

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer(connectionString)
    .Options;

using var context = new AppDbContext(options);

try
{
    context.Database.Migrate();

    IFoodRepository repository = new FoodRepository(context);

    IAuditLogRepository auditRepository =
        new AuditLogRepository(context);

    FoodService service = new FoodService(
        repository,
        auditRepository,
        context);

    FoodMenu menu = new FoodMenu(service);

    menu.Run();
}
catch (Exception ex)
{
    Console.WriteLine("Could not start the application.");
    Console.WriteLine(ex.Message);
    Console.WriteLine("Check your SQL Server connection and database.");
}