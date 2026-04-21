using Valour.Client.Utility;
using Valour.Shared.Utilities;

namespace Valour.Client.Maui
{
    public sealed class MauiStoredCultureService: IStoredCultureService
    {
        /// <inheritdoc/>
        public string GetCultureString()
        {
            string loadedCulture = Preferences.Default.Get("BlazorCulture", SupportedCultures.Default);

            if (!SupportedCultures.Get().Contains(loadedCulture, StringComparer.InvariantCultureIgnoreCase))
                loadedCulture = SupportedCultures.Default;

            return loadedCulture;
        }

        /// <inheritdoc/>
        public async Task<string> GetCultureStringAsync()
        {
            return await Task.Run(() => GetCultureString());
        }

        /// <inheritdoc/>
        public void SetCultureString(string cultureName)
        {
            if (!SupportedCultures.Get().Contains(cultureName, StringComparer.InvariantCultureIgnoreCase))
                cultureName = SupportedCultures.Default;

            Preferences.Default.Set("BlazorCulture", cultureName);
        }

        /// <inheritdoc/>
        public async Task SetCultureStringAsync(string cultureName)
        {
            await Task.Run(() => SetCultureString(cultureName));
        }
    }
}
