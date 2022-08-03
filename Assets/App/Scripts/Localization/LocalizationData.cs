using System.Collections.Generic;

namespace App.Scripts.Localization
{
    public class LocalizationData
    {
        public List<LocalizationItem> items;
    }

    [System.Serializable]
    public class LocalizationItem
    {
        public string key;
        public string value;
    }
}