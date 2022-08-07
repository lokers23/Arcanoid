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
        //[SerializeField] private Image fader;
        [SerializeField] private float duration = 1.0f;
        //private static SceneController _instance;
        // private void Awake()
        // {
        //     if (_instance == null)
        //     {
        //         _instance = this;
        //         DontDestroyOnLoad(gameObject);
        //         
        //         fader.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        //     }
        //     else
        //     {
        //         Destroy(gameObject);
        //     }
        // }

        // public void LoadScene(SceneSO scene)
        // {
        //     var sequence = DOTween.Sequence();
        //     fader.gameObject.SetActive(true);
        //     sequence.Append(fader.DOFade(1f, 1f));
        //     sequence.InsertCallback(duration, () =>
        //     {
        //         SceneManager.LoadScene(scene.SceneName);
        //     });
        //     
        //     sequence.Append(fader.DOFade(0f, 1f));
        //     fader.gameObject.SetActive(false);
        // }

        public void LoadSceneSimple(SceneSO scene)
        {
            SceneManager.LoadScene(scene.SceneName);
        }
    }
}
