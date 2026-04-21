using Microsoft.Extensions.Localization;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Valour.Sdk.Services;

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
                string format;
                
                try
                {
                    format = _resMan.GetString(name, CultureInfo.CurrentUICulture);
                }
                catch (MissingManifestResourceException)
                {
                    return new LocalizedString(name, name);
                }

                string value = string.Format(format ?? name, args);

                return new LocalizedString(name, value);
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
    }
}
