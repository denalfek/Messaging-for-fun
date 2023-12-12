namespace MessagingForFun.Infrastructure.Data;

public class Message
{
    public Message(string content, string channel)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        Content = content;
        Channel = channel;
    }
    
    public Guid Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string Content { get; set; }
    
    public string Channel { get; set; }
    
    public DateTime? AnalyzedAt { get; set; }
    
    public bool Suitable { get; set; }
    
    public DateTime? ExtendedAt { get; set; }
}