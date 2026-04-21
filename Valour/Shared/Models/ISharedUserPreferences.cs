using static Valour.Shared.Models.TimeSettings;

namespace Valour.Shared.Models;

public interface ISharedUserPreferences : ISharedModel<long>
{
    ErrorReportingState ErrorReportingState { get; set; }
    int NotificationVolume { get; set; }
    long EnabledNotificationSources { get; set; }
    DmPolicy DmPolicy { get; set; }
    DmPolicy CallPolicy { get; set; }
    bool ForceGpuAcceleration { get; set; }
    TimeFormatPreference TimeFormat { get; set; }
    bool AlwaysShowTime { get; set; }
    bool UseRelativeTime { get; set; }
    bool SyncLanguageBetweenDevices { get; set; }
    string Language { get; set; }

    /// <summary>
    /// Personal per-channel cooldown for activity notifications, in seconds.
    /// Null inherits each planet's cadence default.
    /// </summary>
    int? ActivityCooldownSeconds { get; set; }
}
