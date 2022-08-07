using System;
using TMPro;
using UnityEngine;

namespace App.Scripts.Localization
{
    public class LocatizedText : MonoBehaviour
    {
        [SerializeField] private string localizationKey;
        [SerializeField] private TextMeshProUGUI textComponent;
        
        private void Start()
        {
            SetText();
        }

        private void OnEnable()
        {
            LocalizationManager.OnChangeLanguage += SetText;
        }

        private void OnDisable()
        {
            LocalizationManager.OnChangeLanguage -= SetText;
        }

        private void SetText()
        {
            textComponent.text = LocalizationManager.GetTextByKey(localizationKey).ToUpper();
        }
    }
}