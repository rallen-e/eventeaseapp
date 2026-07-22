using Microsoft.JSInterop;
using System.Text.Json;

namespace EventEaseApp.Services
{
  public class AttendanceService
  {
    private readonly IJSRuntime _js;

    public AttendanceService(IJSRuntime js)
    {
      _js = js;
    }

    public async Task<IReadOnlyCollection<string>> GetAttendees(int eventId)
    {
      try
      {
        var key = $"attendance_{eventId}";
        var existing = await _js.InvokeAsync<string>("localStorage.getItem", key);

        if (string.IsNullOrEmpty(existing))
        {
          return Array.Empty<string>();
        }

        var attendees = JsonSerializer.Deserialize<List<string>>(existing) ?? new List<string>();
        return attendees.AsReadOnly();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error getting attendees: {ex.Message}");
        return Array.Empty<string>();
      }
    }

    public async Task<int> GetAttendanceCount(int eventId)
    {
      try
      {
        var attendees = await GetAttendees(eventId);
        return attendees.Count;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error getting attendance count: {ex.Message}");
        return 0;
      }
    }

    public async Task RegisterAttendance(int eventId, string email)
    {
      try
      {
        if (eventId <= 0 || string.IsNullOrWhiteSpace(email))
        {
          return;
        }

        var key = $"attendance_{eventId}";
        var existing = await _js.InvokeAsync<string>("localStorage.getItem", key);

        var attendees = string.IsNullOrEmpty(existing)
          ? new List<string>()
          : JsonSerializer.Deserialize<List<string>>(existing) ?? new List<string>();

        var trimmedEmail = email.Trim();
        if (!attendees.Contains(trimmedEmail, StringComparer.OrdinalIgnoreCase))
        {
          attendees.Add(trimmedEmail);
          var json = JsonSerializer.Serialize(attendees);
          await _js.InvokeVoidAsync("localStorage.setItem", key, json);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error registering attendance: {ex.Message}");
      }
    }
  }
}