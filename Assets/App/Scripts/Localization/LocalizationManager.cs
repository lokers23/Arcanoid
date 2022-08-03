using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Path = System.IO.Path;

namespace App.Scripts.Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        private const string FileNamePrefix = "text_";
        private const string FileExtension = ".json";
        private string _fullNameTextFile;
        private string _fullPathTextFile;
        private string _languageChoose = "RU";
        private string _loadedJsonText = "";

        [HideInInspector]
        public bool _isReady;
        private bool _isFileFound;
        private bool _isTryChangeLanguageRunTime;

        private Dictionary<string, string> _localizeDictionary;
        private LocalizationData _loadedData;

        private static LocalizationManager LocalizationManagerInstance;

        public static LocalizationManager Instance
        {
            get
            {
                if (LocalizationManagerInstance == null)
                {
                    LocalizationManagerInstance = FindObjectOfType(typeof(LocalizationManager)) as LocalizationManager;
                }

                return LocalizationManagerInstance;
            }
        }

        private IEnumerator Start()
        {
            _languageChoose = LocaleHelper.GetSupportedLanguageCode();
            _fullNameTextFile = $"{FileNamePrefix}{_languageChoose.ToLower()}{FileExtension}";

            _fullPathTextFile = Path.Combine(Application.streamingAssetsPath, _fullNameTextFile);

            yield return StartCoroutine(LoadJsonLanguageData());
            _isReady = true;
        }

        private IEnumerator LoadJsonLanguageData()
        {
            CheckFileExist();
        
            yield return new WaitUntil(() => _isFileFound);
        
            _loadedData = JsonUtility.FromJson<LocalizationData>(_loadedJsonText);
            _localizeDictionary = new Dictionary<string, string>(_loadedData.items.Count);
            _loadedData.items.ForEach(item =>
            {
                _localizeDictionary.Add(item.key, item.value);
            });
            yield return null;
        }

        private void CheckFileExist()
        {
            if (!File.Exists(_fullPathTextFile))
            {
                CopyFileFromRessource();
            }
            else
            {
                LoadFileContents();
            }
        }

        private void LoadFileContents()
        {
            _loadedJsonText = File.ReadAllText(_fullPathTextFile);
            _isFileFound = true;
        }
        private void CopyFileFromRessource()
        {
            var myFile = Resources.Load($"{FileNamePrefix}{_languageChoose}") as TextAsset;
            if (myFile == null)
            {
                return;
            }

            _loadedJsonText = myFile.ToString();
            File.WriteAllText(_fullPathTextFile, _loadedJsonText);
            StartCoroutine(WaitCreationFile());
        }

        private IEnumerator WaitCreationFile()
        {
            FileInfo myFile = new FileInfo(_fullNameTextFile);
            float timeOut = 0.0f;
            while (timeOut < 5.0f && !IsFileFinishCreate(myFile))
            {
                timeOut += Time.deltaTime;
                yield return null;
            }
        }

        private bool IsFileFinishCreate(FileInfo fileInfo)
        {
            FileStream stream = null;
            try
            {
                stream = fileInfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                _isFileFound = true;
                return true;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            _isFileFound = false;
            return false;
        }

        public string GetTextForKey(string localizationKey)
        {
            if (_localizeDictionary.ContainsKey(localizationKey))
            {
                return _localizeDictionary[localizationKey];
            }

            return "";
        }
        
        private IEnumerator SwitchLanguage(string language)
        {
            if (!_isTryChangeLanguageRunTime)
            {
                _isTryChangeLanguageRunTime = true;
                _isFileFound = false;
                _isReady = false;
                _languageChoose = language;

                _fullNameTextFile = $"{FileNamePrefix}{_languageChoose.ToLower()}{FileExtension}";
                _fullPathTextFile = Path.Combine(Application.streamingAssetsPath, _fullNameTextFile);

                yield return StartCoroutine(LoadJsonLanguageData());
                _isReady = true;

                LocalizedText[] arrayText = FindObjectsOfType<LocalizedText>();

                for (int i = 0; i < arrayText.Length; i++)
                {
                    arrayText[i].Attributiontext();
                }

                _isTryChangeLanguageRunTime = false;
            }
        }

        public void ChangeLanguage(string language)
        {
            StartCoroutine(SwitchLanguage(language));
        }
    }
}
