using Valour.Shared.Models;
using static Valour.Shared.Models.TimeSettings;

namespace Valour.Server.Models;

public class UserPreferences : ISharedUserPreferences
{
    public long Id { get; set; }
    public ErrorReportingState ErrorReportingState { get; set; }
    public int NotificationVolume { get; set; }
    public long EnabledNotificationSources { get; set; }
    public DmPolicy DmPolicy { get; set; }
    public TimeFormatPreference TimeFormat { get; set;  }
    public bool SyncLanguageBetweenDevices { get; set; }
    public string Language { get; set; }
}
