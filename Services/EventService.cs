using EventEaseApp.Models;

namespace EventEaseApp.Services
{
  public class EventService
  {
    private readonly List<EventModel> _events = new()
    {
        new EventModel { Id = 1, Name = "Corporate Summit 2026", Date = DateTime.Today.AddDays(14), Location = "Sydney Convention Centre", Description = "Annual leadership summit." },
        new EventModel { Id = 2, Name = "Product Launch Party", Date = DateTime.Today.AddDays(30), Location = "Harbour Rooftop", Description = "Celebrate the new release." },
        new EventModel { Id = 3, Name = "Charity Gala", Date = DateTime.Today.AddDays(45), Location = "Grand Ballroom", Description = "Fundraising gala dinner." },
        new EventModel { Id = 4, Name = "Tech Conference 2026", Date = DateTime.Today.AddDays(7), Location = "International Convention Hall", Description = "Latest innovations in technology and AI." },
        new EventModel { Id = 5, Name = "Networking Breakfast", Date = DateTime.Today.AddDays(3), Location = "Darling Harbour", Description = "Connect with industry professionals over coffee and pastries." },
        new EventModel { Id = 6, Name = "Workshop: Digital Marketing", Date = DateTime.Today.AddDays(21), Location = "Tech Hub Sydney", Description = "Learn the latest digital marketing strategies and tools." },
        new EventModel { Id = 7, Name = "Annual Awards Ceremony", Date = DateTime.Today.AddDays(60), Location = "The Opera House", Description = "Celebrating excellence and achievement in business." },
        new EventModel { Id = 8, Name = "Startup Pitch Night", Date = DateTime.Today.AddDays(12), Location = "Innovation Lab", Description = "Watch promising startups pitch their ideas to investors." },
        new EventModel { Id = 9, Name = "Wine Tasting Evening", Date = DateTime.Today.AddDays(25), Location = "Blue Mountains Lodge", Description = "Enjoy a curated selection of premium wines." },
        new EventModel { Id = 10, Name = "Business Forum: Future of Work", Date = DateTime.Today.AddDays(35), Location = "CBD Tower Level 50", Description = "Industry leaders discuss workplace trends and remote work." },
        new EventModel { Id = 11, Name = "Community Cleanup Drive", Date = DateTime.Today.AddDays(5), Location = "Bondi Beach", Description = "Join us in cleaning up our beautiful beaches." },
        new EventModel { Id = 12, Name = "Leadership Training Seminar", Date = DateTime.Today.AddDays(18), Location = "Executive Centre", Description = "Develop essential leadership and management skills." }
    };

    public Task<List<EventModel>> GetAllAsync()
    {
      var events = _events
        .Select(NormalizeEvent)
        .Where(e => e is not null)
        .Cast<EventModel>()
        .ToList();

      return Task.FromResult(events);
    }

    public Task<EventModel?> GetByIdAsync(int id)
    {
      if (id <= 0)
      {
        return Task.FromResult<EventModel?>(null);
      }

      var eventItem = _events.FirstOrDefault(e => e.Id == id);
      return Task.FromResult<EventModel?>(NormalizeEvent(eventItem));
    }

    private static EventModel? NormalizeEvent(EventModel? eventItem)
    {
      if (eventItem is null)
      {
        return null;
      }

      return new EventModel
      {
        Id = eventItem.Id > 0 ? eventItem.Id : 0,
        Name = string.IsNullOrWhiteSpace(eventItem.Name) ? "Untitled Event" : eventItem.Name.Trim(),
        Date = eventItem.Date == default ? DateTime.Today : eventItem.Date,
        Location = string.IsNullOrWhiteSpace(eventItem.Location) ? "Location TBD" : eventItem.Location.Trim(),
        Description = string.IsNullOrWhiteSpace(eventItem.Description) ? "Details coming soon." : eventItem.Description.Trim()
      };
    }
  }
}
