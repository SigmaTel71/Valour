using Blazor.WebAssembly.DynamicCulture.LocalizationManager;
using Valour.Client.Utility;
using Valour.Shared.Utilities;

namespace Valour.Client.Blazor
{
    public class BlazorStoredCultureService : IStoredCultureService
    {
        private readonly Func<LocalizationLocalStorageManager> _managerFactory;

        public BlazorStoredCultureService(Func<LocalizationLocalStorageManager> managerFactory)
        {
            _managerFactory = managerFactory;
        }

        /// <inheritdoc/>
        public string GetCultureString()
        {
            return Task.Run(async () => await GetCultureStringAsync()).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<string> GetCultureStringAsync()
        {
            string storedCulture = await Task.FromResult(_managerFactory().GetBlazorCultureAsync()).Result;

            if (string.IsNullOrEmpty(storedCulture) || !SupportedCultures.Get().Contains(storedCulture, StringComparer.InvariantCultureIgnoreCase))
                storedCulture = SupportedCultures.Default;

            return storedCulture;
        }

        /// <inheritdoc/>
        public void SetCultureString(string cultureName)
        {
            Task.Run(async () => await SetCultureStringAsync(cultureName)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task SetCultureStringAsync(string cultureName)
        {
            if (!SupportedCultures.Get().Contains(cultureName, StringComparer.InvariantCultureIgnoreCase))
                cultureName = SupportedCultures.Default;

            await _managerFactory().SetBlazorCultureAsync(cultureName);
        }
    }
}
