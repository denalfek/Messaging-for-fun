using StackExchange.Redis;

namespace MessagingForFun.Server.API.Services;

public interface IRedisService
{
    Task Publish(string channel, string message);
    Task Subscribe(string channel, Action<RedisChannel, RedisValue> action);
}