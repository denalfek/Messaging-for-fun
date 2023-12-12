using System.Net;
using MessagingForFun.Server.MainWorker;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:6379";
            options.InstanceName = "MessagingForFun";
        });

        var redLockFactory = RedLockFactory.Create(new List<RedLockEndPoint>
        {
            new DnsEndPoint("localhost", 6379),
        });
        
        services.AddSingleton(redLockFactory);
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
