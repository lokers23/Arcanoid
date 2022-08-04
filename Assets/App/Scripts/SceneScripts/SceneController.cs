using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

namespace App.Scripts.SceneScripts
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private Image fader;
        private static SceneController instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                
                fader.rectTransform.sizeDelta = new Vector2(Screen.width +20, Screen.height + 20);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LoadScene(SceneSO scene)
        {
            float duration = 1;
            var sequence = DOTween.Sequence();
            sequence.Append(fader.DOFade(1f, 1f));
            sequence.InsertCallback(duration, () =>
            {
                SceneManager.LoadScene(scene.sceneName);
            });

            sequence.Append(fader.DOFade(0f, 1f));
        }
    }
}
