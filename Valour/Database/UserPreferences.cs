using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Valour.Shared.Models;
using Valour.Shared.Utilities;
using static Valour.Shared.Models.TimeSettings;

namespace Valour.Database;

[Table("user_preferences")]
public class UserPreferences : ISharedUserPreferences
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("error_reporting_state")]
    public ErrorReportingState ErrorReportingState { get; set; }

    [Column("notification_volume")]
    public int NotificationVolume { get; set; } = NotificationPreferences.DefaultNotificationVolume;

    [Column("enabled_notification_sources")]
    public long EnabledNotificationSources { get; set; } = NotificationPreferences.AllNotificationSourcesMask;

    [Column("marketing_email_opt_out")]
    public bool MarketingEmailOptOut { get; set; } = false;

    [Column("dm_policy")]
    public DmPolicy DmPolicy { get; set; } = DmPolicy.Everyone;

    [Column("sync_language_between_devices")]
    public bool SyncLanguageBetweenDevices { get; set; } = true;

    [Column("language")]
    public string Language { get; set; } = SupportedCultures.Default;

    [Column("time_format")]
    public TimeFormatPreference TimeFormat { get; set; } = TimeFormatPreference.PreferAuto;
}
