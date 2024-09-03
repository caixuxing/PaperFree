using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Trasen.PaperFree.Infrastructure.HangfireTasks
{
    internal class FirstStartService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // await Task.Run(() => { Console.WriteLine("FirstStartService......StartAsync"); }, cancellationToken);

             Hangfire.RecurringJob.AddOrUpdate("11360847",() => LogPring(), "0/20 * * * * ?");
        }

        public async Task LogPring()
        {
            await Console.Out.WriteLineAsync($"循环执行的任务{DateTime.Now}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
           
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// 后台周期任务Demo
    /// </summary>
    internal class FirstStartService2 : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ILogger<FirstStartService> logger;

        public FirstStartService2(IServiceScopeFactory scopeFactory, ILogger<FirstStartService> logger)
        {
            this.scopeFactory = scopeFactory;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("running");
                //周期性任务，于上次任务执行完成后，等待5秒，执行下一次任务
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}