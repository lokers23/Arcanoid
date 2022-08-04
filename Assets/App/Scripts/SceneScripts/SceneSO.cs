using UnityEditor;
using UnityEngine;

namespace App.Scripts.SceneScripts
{
    [CreateAssetMenu(fileName = "SceneData", menuName = "ScriptableObject/SceneData")]
    public class SceneSO : ScriptableObject, ISerializationCallbackReceiver
    {
        public string sceneName;
        public SceneAsset sceneAsset;
        
        public void OnBeforeSerialize()
        {
            if (sceneAsset != null)
            {
                sceneName = sceneAsset.name;
            }
            else
            {
                sceneName = "";
            }
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}
