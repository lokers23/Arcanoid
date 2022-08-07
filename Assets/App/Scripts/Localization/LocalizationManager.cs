using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace App.Scripts.Localization
{
    public static class LocalizationManager
    {
        public static event Action OnChangeLanguage;
        
        private const string KeyPref = "Localization";
        private const string Folder = "Localization";
        private const SystemLanguage DefaultLanguage = SystemLanguage.Russian;
        
        private static string _fullNameTextFile;
        private static string _fullPathTextFile;

        private static Dictionary<string, string> _localizeDictionary;

        static LocalizationManager()
        {
            if (!PlayerPrefs.HasKey(KeyPref))
            {
                PlayerPrefs.SetInt(KeyPref, (int)DefaultLanguage);
                LoadJsonFile((SystemLanguage)PlayerPrefs.GetInt(KeyPref));
            }
            else
            {
                LoadJsonFile((SystemLanguage)PlayerPrefs.GetInt(KeyPref));
            }
        }

        private static void LoadJsonFile(SystemLanguage language)
        {
            _fullNameTextFile = Enum.GetName(typeof(SystemLanguage),language);
            _fullPathTextFile = $"{Folder}/{_fullNameTextFile}";
            var textAsset = Resources.Load<TextAsset>(_fullPathTextFile);
            if (textAsset == null)
            {
                throw new ArgumentNullException(nameof(_fullPathTextFile), "Localization file is missing");
            }
                
            _localizeDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(textAsset.ToString());
        }
        
        public static string GetTextByKey(string localizationKey)
        {
            if (_localizeDictionary.ContainsKey(localizationKey))
            {
                return _localizeDictionary[localizationKey];
            }

            return "";
        }
        
        public static void ChangeLanguage(SystemLanguage language)
        {
            LoadJsonFile(language);
            PlayerPrefs.SetInt(KeyPref, (int)language);
            OnChangeLanguage?.Invoke();
        }

        public static List<SystemLanguage> GetLanguages()
        {
            var allFiles = Resources.LoadAll<TextAsset>(Folder).ToList();
            if (allFiles.Count == 0)
            {
                throw new ArgumentException("Localization files are missing");
            }
            
            var languages = new List<SystemLanguage>(allFiles.Count);
            foreach (var file in allFiles)
            {
                var name = file.name;
                Enum.TryParse<SystemLanguage>(name, out var language);
                languages.Add(language);
            }
            
            return languages;
        }
    }
}