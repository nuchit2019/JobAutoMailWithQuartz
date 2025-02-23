using Janawat.EmailService.Application.Jobs;
using Quartz;

namespace Janawat.EmailService.Worker.Quartz;

public static class QuartzConfigurator
{
    public static IServiceCollection AddQuartzServices(this IServiceCollection services, IHostEnvironment env)
    {
        services.AddQuartz(q =>
        {
            // ตั้งค่า DI JobFactory 
            q.UseJobFactory<JobFactory>();

            var jobKey = new JobKey("SendEmailJob");
            q.AddJob<SendEmailJob>(opts => opts.WithIdentity(jobKey));

            // Trigger 1: ทำงานทุกวันเวลา 8:00 น.
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("SendEmailJob-trigger-daily")
                .WithCronSchedule("0 0 8 * * ?")
            );

            // Trigger 2: ทำงานทุกๆ 2 นาที
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("SendEmailJob-trigger-every-2-minutes")
                .WithCronSchedule("0 */2 * * * ?")
            );

            // Trigger 3: รันทันทีเมื่อเริ่มต้น เฉพาะ Debug Mode
            if (env.IsDevelopment())
            {
                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("SendEmailJob-trigger-immediate")
                    .StartNow()
                );
            }
        });

        // Add Quartz hosted service to ensure jobs are executed
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        return services;
    }
}
