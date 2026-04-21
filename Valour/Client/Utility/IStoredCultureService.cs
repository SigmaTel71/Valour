using Valour.Shared.Utilities;

namespace Valour.Client.Utility
{
    public interface IStoredCultureService
    {
        /// <summary>
        /// Retrieves currently used culture from preferences storage. If the chosed culture code
        /// is not listed in <see cref="SupportedCultures.Get"/>, it is reset to <see cref="SupportedCultures.Default"/>
        /// </summary>
        /// <returns>Culture code in form used by <see cref="CultureInfo.Name"/></returns>
        public string GetCultureString();

        /// <summary>
        /// Writes currently used culture to preferences storage. If the chosen culture code
        /// is not listed in <see cref="SupportedCultures.Get"/>, it is reset to <see cref="SupportedCultures.Default"/>
        /// </summary>
        /// <param name="cultureName">Subject culture code as in <see cref="CultureInfo.Name"/> field</param>
        public void SetCultureString(string cultureName);

        
        /// <summary>
        /// Asynchronous version of <see cref="GetCultureString"/>
        /// </summary>
        /// <returns>Culture code in form used by <see cref="CultureInfo.Name"/></returns>
        public Task<string> GetCultureStringAsync();

        /// <summary>
        /// Asynchronous version of <see cref="SetCultureString(string)"/>
        /// </summary>
        /// <param name="cultureName"></param>
        /// <returns>Culture code in form used by <see cref="CultureInfo.Name"/></returns>
        public Task SetCultureStringAsync(string cultureName);
    }
}
