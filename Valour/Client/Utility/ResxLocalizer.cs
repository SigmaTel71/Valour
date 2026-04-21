using Microsoft.Extensions.Localization;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Valour.Sdk.Services;
using Valour.Shared.Utilities;

namespace Valour.Client.Utility
{
    public class ResxLocalizer<T>: IStringLocalizer<T>
    {
        private readonly ResourceManager _resMan;

        public ResxLocalizer()
        {
            Type typeOfT = typeof(T);

            string fullName = typeOfT.FullName;
            fullName = fullName.Substring("Valour.Client.".Count());

            _resMan = new ResourceManager("Valour.Client.Resources." + fullName, Assembly.GetExecutingAssembly());
        }

        public LocalizedString this[string name]
        {
            get { return this[name, []]; }
        }

        public LocalizedString this[string name, params object[] args]
        {
            get
            {
                var (format, resourceNotFound) = ObtainString(name);
                string value = string.Format(format, args);

                return new LocalizedString(name, value, resourceNotFound);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeAncestorCulture)
        {
            var resSet = _resMan.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            if (resSet != null)
            {
                foreach(DictionaryEntry entry in resSet)
                {
                    yield return new LocalizedString(entry.Key.ToString(), entry.Value?.ToString() ?? string.Empty);
                }
            }
        }
        private Tuple<string, bool> ObtainString(string name)
        {
            string value = null;
            bool resourceNotFound = false;
            try
            {
                value = _resMan.GetString(name, CultureInfo.CurrentUICulture);
            }
            catch (Exception)
            {
                resourceNotFound = CultureInfo.CurrentUICulture.Name != SupportedCultures.Default;
            }

            return new Tuple<string, bool>(value ?? name, resourceNotFound);
        }
    }
}
