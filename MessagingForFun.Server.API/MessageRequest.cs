using System.ComponentModel.DataAnnotations;

namespace MessagingForFun.Server.API;

public class MessageRequest
{
    public MessageRequest(
        string content,
        string channel)
    {
        Content = content;
        Channel = channel;
    }

    [Required]
    [MaxLength(1024)]
    public string Content { get; set; }
    
    [Required]
    [MaxLength(128)]
    public string Channel { get; set; }
}