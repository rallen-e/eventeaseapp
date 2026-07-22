namespace EventEaseApp.Services
{
  public class SessionStateService
  {
    private readonly Dictionary<string, string> _sessionValues = new();

    public bool IsAuthenticated => _sessionValues.ContainsKey("userName") && _sessionValues.ContainsKey("userEmail");

    public string? UserName
    {
      get => GetValue("userName");
      set => SetValue("userName", value);
    }

    public string? UserEmail
    {
      get => GetValue("userEmail");
      set => SetValue("userEmail", value);
    }

    public void Clear()
    {
      _sessionValues.Clear();
    }

    public void SetValue(string key, string? value)
    {
      if (string.IsNullOrWhiteSpace(key))
      {
        return;
      }

      if (string.IsNullOrWhiteSpace(value))
      {
        _sessionValues.Remove(key);
        return;
      }

      _sessionValues[key] = value.Trim();
    }

    public string? GetValue(string key)
    {
      if (string.IsNullOrWhiteSpace(key))
      {
        return null;
      }

      return _sessionValues.TryGetValue(key, out var value) ? value : null;
    }
  }
}
