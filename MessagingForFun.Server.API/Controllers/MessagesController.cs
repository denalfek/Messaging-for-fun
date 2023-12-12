using System.ComponentModel.DataAnnotations;
using MessagingForFun.Infrastructure;
using MessagingForFun.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace MessagingForFun.Server.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MessagesController : ControllerBase
{
    private readonly ILogger<MessagesController> _logger;
    private readonly IDistributedCache _cache;

    private readonly IDatabase _database;
    
    public MessagesController(
        ILogger<MessagesController> logger,
        IDistributedCache cache)
    {
        _logger = logger;
        var connectionMultiplexer = ConnectionMultiplexer.Connect("localhost:6379");
        _database = connectionMultiplexer.GetDatabase();
        _cache = cache;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post(
        [Required][FromBody]MessageRequest message)
    {
        var msg = new Message(message.Content, message.Channel);
        await _database.HashSetAsync(
            new RedisKey("messages_hash"),
            new [] { new HashEntry(message.Channel, message.Content) });
        
        // await _cache.SetRecordAsync(msg.Channel, msg);
        return Ok(msg);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([Required][FromQuery]string key)
    {
        var result = await _cache.GetRecordAsync<Message>(key);
        return Ok(result);
    }
}