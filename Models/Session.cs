namespace webUserLoginTest.Models;

public class Session
{
    public int userId { get; }
    public DateTime expiration { get; }
    public DateTime acquisition { get; }
    
    public Session(int userId, DateTime expiration, DateTime acquisition)
    {
        this.userId = userId;
        this.expiration = expiration;
        this.acquisition = acquisition;
    }
}