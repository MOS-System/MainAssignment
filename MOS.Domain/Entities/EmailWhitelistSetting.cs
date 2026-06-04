public class EmailWhitelistSetting
{
    public Guid Id { get; private set; }
    public bool IsEnabled { get; private set; }

    public EmailWhitelistSetting()
    {
        IsEnabled = true;
    }

    public void SetEnabled(bool isEnabled)
    {
        IsEnabled = isEnabled;
    }
}