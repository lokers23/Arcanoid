using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Localization
{
    public class LocalizationDropdown : MonoBehaviour
    {
        [SerializeField] private Dropdown dropdown;

        private void Start()
        {
            var languages = LocalizationManager.GetLanguages();
            var names = new List<string>();
            foreach (var language in languages)
            {
                var languageString = Enum.GetName(typeof(SystemLanguage), language);
                names.Add(languageString);
            }
            
            dropdown.AddOptions(names);
        }

        public void ChangeValue()
        {
            var value = dropdown.value;
            var selectedString = dropdown.options[value].text;
            var tryParse = Enum.TryParse<SystemLanguage>(selectedString, out var language);
            if (tryParse)
            {
                LocalizationManager.ChangeLanguage(language);
            }
        }
    }
}