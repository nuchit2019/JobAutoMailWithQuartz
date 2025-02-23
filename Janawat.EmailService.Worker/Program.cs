using Janawat.EmailService.Domain.Interfaces; 
using Janawat.EmailService.Infrastructure.Services;
using Janawat.EmailService.Application.Jobs;
using Janawat.EmailService.Worker;
using Quartz.Spi;
using Janawat.EmailService.Worker.Quartz;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var env = hostContext.HostingEnvironment;

        // Register Application Services
        services.AddSingleton<IEmailService, EmailService>();

        // Register Custom Job Factory
        services.AddSingleton<IJobFactory, JobFactory>();

        // Register SendEmailJob ให้ DI Container รู้จัก
        services.AddTransient<SendEmailJob>();

        // Register Quartz Services พร้อมส่ง Environment เข้าไป
        services.AddQuartzServices(env);

        // Register Worker
        services.AddHostedService<Worker>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
        logging.SetMinimumLevel(LogLevel.Debug);
    })
    .Build();

await host.RunAsync();
