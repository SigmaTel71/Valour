using System.ComponentModel;

namespace Valour.Shared.Models;

public static class TimeSettings
{
    public enum TimeFormatPreference : int
    {
        [Description("Auto")]
        PreferAuto = 0,
        [Description("12 Hours")]
        Prefer12Hours = 12,
        [Description("24 Hours")]
        Prefer24Hours = 24
    };
}