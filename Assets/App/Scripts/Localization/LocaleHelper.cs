using UnityEngine;

namespace App.Scripts.Localization
{
    public static class LocaleHelper
    {
        public static string GetSupportedLanguageCode()
        {
            var language = Application.systemLanguage;

            switch (language)
            {
                case SystemLanguage.Russian:
                    return LocaleApplication.RU;
                case SystemLanguage.English:
                    return LocaleApplication.EN;
                default:
                    return GetDefaultSupportedLanguage();
            }
        }

        private static string GetDefaultSupportedLanguage()
        {
            return LocaleApplication.RU;
        }
    }
}
