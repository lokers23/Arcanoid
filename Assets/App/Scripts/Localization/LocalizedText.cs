using System.Collections;
using TMPro;
using UnityEngine;

namespace App.Scripts.Localization
{
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string localizationKey;
        [SerializeField] private TextMeshProUGUI textComponent;
        private IEnumerator Start()
        {
            while (!LocalizationManager.Instance._isReady)
            {
                yield return null;
            }

            Attributiontext();
        }

        public void Attributiontext()
        {
            textComponent.text = LocalizationManager.Instance.GetTextForKey(localizationKey).ToUpper();
        }
    }
}
