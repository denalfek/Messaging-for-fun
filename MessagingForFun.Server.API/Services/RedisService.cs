using StackExchange.Redis;

namespace MessagingForFun.Server.API.Services;

public class RedisService : IRedisService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public RedisService(string connectionString)
    {
        _connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
    }

    public async Task Publish(string channel, string message)
    {
        var db = _connectionMultiplexer.GetDatabase();
        var d = await db.StringSetAsync(channel, message);
    }

    public async Task Subscribe(string channel, Action<RedisChannel, RedisValue> action)
    {
        var sub = _connectionMultiplexer.GetSubscriber();
        await sub.SubscribeAsync(channel, action);
    }
}