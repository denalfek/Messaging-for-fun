using Microsoft.Extensions.Caching.Distributed;
using RedLockNet.SERedis;
using StackExchange.Redis;

namespace MessagingForFun.Server.MainWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly RedLockFactory _redLockFactory;
    private readonly ConnectionMultiplexer _connectionMultiplexer;

    public Worker(
        ILogger<Worker> logger,
        RedLockFactory redLockFactory)
    {
        _logger = logger;
        _redLockFactory = redLockFactory;
        _connectionMultiplexer = ConnectionMultiplexer.Connect("localhost:6379");;
        
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await using var redLock = await _redLockFactory
                .CreateLockAsync(
                    "test_lock",
                    TimeSpan.FromSeconds(30));
            
            if (redLock.IsAcquired)
            {
                var subscriber = _connectionMultiplexer.GetSubscriber();
                (await subscriber
                        .SubscribeAsync("test_channel"))
                    .OnMessage(
                        async channelMessage => await HandleMessage(
                            channelMessage.Channel, channelMessage.Message, stoppingToken));
                
                _logger.LogInformation(
                    "Lock acquired\n Worker running at: {Time}\n on host: {Host}\n domain: {Domain}",
                    DateTimeOffset.Now,
                    Environment.MachineName,
                    AppDomain.CurrentDomain.FriendlyName);
                
                await Task.Delay(10000, stoppingToken);
                
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
    
    private async Task HandleMessage(RedisChannel channel, RedisValue message, CancellationToken ct = default)
    {
        _logger.LogInformation(
            "Message received\n Channel: {Channel}\n Message: {Message}",
            channel,
            message);
        
        await Task.Delay(10000, ct);
        await Task.CompletedTask;
    }
}
