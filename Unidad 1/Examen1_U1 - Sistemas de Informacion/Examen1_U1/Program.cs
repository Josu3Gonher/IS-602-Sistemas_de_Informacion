using Examen1_U1;
using WatchDog;
using WatchDog.src.Enums;

var builder = WebApplication.CreateBuilder(args);


var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

builder.Services.AddWatchDogServices(options =>
{
    options.IsAutoClear = false;
    options.SetExternalDbConnString = "Server=GONZALEZ;Database=RegistroEstudiantes;User Id=sa;Password=1234;Trusted_Connection=false;TrustServerCertificate=true";
    options.DbDriverOption = WatchDogDbDriverEnum.MSSQL;
});

var app = builder.Build();



app.UseWatchDogExceptionLogger();

app.UseWatchDog(options =>
{
    options.WatchPageUsername = "admin";
    options.WatchPagePassword = "admin";
});
var servicioLogger = (ILogger<Startup>)app.Services.GetService(typeof(ILogger<Startup>));

startup.Configure(app, app.Environment, servicioLogger);

app.Run();