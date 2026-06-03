public class EmailWhitelistSetting
{
    public int Id { get; private set; }
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