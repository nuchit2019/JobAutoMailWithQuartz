using Quartz;

namespace Janawat.EmailService.Worker;

public class Worker : BackgroundService
{
    private readonly ISchedulerFactory _schedulerFactory;

    public Worker(ISchedulerFactory schedulerFactory)
    {
        _schedulerFactory = schedulerFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Get the scheduler instance from the factory
        var scheduler = await _schedulerFactory.GetScheduler(stoppingToken);

        // Start the scheduler
        await scheduler.Start(stoppingToken);
    }
}
