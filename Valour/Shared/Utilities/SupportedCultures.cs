namespace Valour.Shared.Utilities;

public class SupportedCultures
{
    private static readonly string[] languages =
    {
        "en-US",
        "ru-RU"
    };

    public static readonly string Default = languages[0];

    public static string[] Get()
    {
        return languages;
    }
}
