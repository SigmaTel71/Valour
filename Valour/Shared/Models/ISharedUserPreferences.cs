namespace Valour.Shared.Models;

public interface ISharedUserPreferences : ISharedModel<long>
{
    ErrorReportingState ErrorReportingState { get; set; }
    int NotificationVolume { get; set; }
    long EnabledNotificationSources { get; set; }
    DmPolicy DmPolicy { get; set; }
    bool SyncLanguageBetweenDevices { get; set; }
    string Language { get; set; }
}
