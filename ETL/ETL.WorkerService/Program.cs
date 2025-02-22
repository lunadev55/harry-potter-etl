//using ETL.Jobs.Services;
//using ETL.WorkerService;
//using Hangfire;
//using Microsoft.Extensions.Logging;

//var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddHostedService<Worker>();

//builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddHangfireServer();

//RecurringJobManager.AddOrUpdate("BookUpsertJob", () => job.RunAsync(), Cron.Hourly, new RecurringJobOptions { TimeZone = TimeZoneInfo.Utc });
//var recurringJobManager = new RecurringJobManager();
////recurringJobManager.AddOrUpdate(
////"BookUpsertJob",
////    () => new BookUpsertJob(bookService, logger).RunAsync(),
////    Cron.Hourly,
////    new RecurringJobOptions { TimeZone = TimeZoneInfo.Utc }
////    );

//var host = builder.Build();
//host.Run();

using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ETL.Jobs.Services;
using ETL.Application.Interfaces;
using ETL.Jobs.Services;
using ETL.WorkerService;
using ETL.Infrastructure.Services;

var builder = Host.CreateDefaultBuilder(args);


builder.ConfigureServices((hostContext, services) =>
{
    var configuration = hostContext.Configuration;

    // Hangfire Configuration
    services.AddHangfire(config =>
        config.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));

    services.AddHangfireServer();

    // Register Application Services
    //services.AddScoped<IBookService, BookService>();
    services.AddScoped<BookUpsertJob>();

    services.AddHostedService<Worker>();

    //services.AddScoped<BookUpsertJob>();

});

var app = builder.Build();

// Configure Hangfire Jobs
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();
    var bookUpsertJob = serviceProvider.GetRequiredService<BookUpsertJob>();

    // Schedule the Book Upsert Job to run hourly
    recurringJobManager.AddOrUpdate(
        "BookUpsertJob",
        () => bookUpsertJob.RunAsync(),
        Cron.Hourly
    );
}

app.Run();

