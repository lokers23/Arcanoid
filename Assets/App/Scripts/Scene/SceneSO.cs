using UnityEditor;
using UnityEngine;

namespace App.Scripts.SceneScripts
{
    [CreateAssetMenu(fileName = "SceneData", menuName = "ScriptableObject/SceneData")]
    public class SceneSO : ScriptableObject
    {
        public string SceneName => sceneAsset.name;
        public SceneAsset sceneAsset;
    }
}
