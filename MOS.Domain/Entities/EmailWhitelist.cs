public class EmailWhitelist
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public DateTime AddedAt { get; private set; }

    public EmailWhitelist(string email)
    {
        Email = email;
        AddedAt = DateTime.UtcNow;
    }

    private EmailWhitelist() { }
}