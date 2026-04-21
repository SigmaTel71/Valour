using System.Globalization;
using Valour.Shared.Utilities;

namespace Valour.Client.Maui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
#if MACCATALYST
        // The app draws its own dark interface, so the title bar, scroll bars,
        // and system pickers match it instead of following the system setting.
        UserAppTheme = AppTheme.Dark;
#endif
        string loadedCulture = Preferences.Default.Get("BlazorCulture", SupportedCultures.Default);

        if (!SupportedCultures.Get().Contains(loadedCulture, StringComparer.InvariantCultureIgnoreCase))
            loadedCulture = SupportedCultures.Default;

        CultureInfo newCulture = new CultureInfo(loadedCulture);
        CultureInfo.DefaultThreadCurrentCulture = newCulture;
        CultureInfo.DefaultThreadCurrentUICulture = newCulture;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
#if MACCATALYST
        // macOS can restore or open extra windows on its own. Popped-out tabs
        // open through MauiNativeWindowService, not here, so another request
        // while the main window is open would start a second copy of the app.
        if (Windows.Any(x => x.Page is MainPage { IsPopout: false }))
        {
            var extra = new Window(new ContentPage());
            Dispatcher.Dispatch(() => CloseWindow(extra));
            return extra;
        }
#endif

        var window = new Window(new MainPage()) { Title = "Valour" };
#if MACCATALYST
        // Below this size the desktop layout's sidebars crowd out the chat.
        window.MinimumWidth = 720;
        window.MinimumHeight = 480;
#endif
        return window;
    }
}
