using RedLockNet.SERedis;

namespace MessagingForFun.Server.Worker1;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly RedLockFactory _redLockFactory;
    
    public Worker(
        ILogger<Worker> logger,
        RedLockFactory redLockFactory)
    {
        _logger = logger;
        _redLockFactory = redLockFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await using var redLock = await _redLockFactory.CreateLockAsync("test_lock", TimeSpan.FromSeconds(30));
            if (redLock.IsAcquired)
            {
                _logger.LogInformation(
                    "Lock acquired\n Worker running at: {Time}\n on host: {Host}\n domain: {Domain}",
                    DateTimeOffset.Now,
                    Environment.MachineName,
                    AppDomain.CurrentDomain.FriendlyName);

                await Task.Delay(20000, stoppingToken);
                await redLock.DisposeAsync();
            }
            else
            {
                _logger.LogInformation("Lock not acquired");
            }
            
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(100, stoppingToken);
        }
    }
}
