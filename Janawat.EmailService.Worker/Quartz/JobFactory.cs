using Quartz;
using Quartz.Spi;

namespace Janawat.EmailService.Worker.Quartz
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<JobFactory> _logger;

        public JobFactory(IServiceProvider serviceProvider, ILogger<JobFactory> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        // Creates a new job instance using the service provider
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob ?? throw new Exception($"Failed to create instance of job {bundle.JobDetail.JobType.FullName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating job instance.");
                throw;
            }
        }

        // Disposes of the job if it implements IDisposable
        public void ReturnJob(IJob job)
        {
            (job as IDisposable)?.Dispose();
        }
    }
} 
