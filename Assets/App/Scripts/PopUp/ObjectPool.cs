using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Scripts.PopUp
{
    public class ObjectPool : MonoBehaviour
    {
        public List<GameObject> PrefabsForPool;
        private List<GameObject> _pooledObjects = new List<GameObject>();
        public static ObjectPool Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public GameObject GetObjectFromPool(string objectName)
        {
            var instance = _pooledObjects.FirstOrDefault(obj => obj.name == objectName);
            if (instance != null)
            {
                _pooledObjects.Remove(instance);
                instance.SetActive(true);
                return instance;
            }

            var prefab = PrefabsForPool.FirstOrDefault(obj => obj.name == objectName);
            if (prefab != null)
            {
                var newInstance = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
                newInstance.name = objectName;
                return newInstance;
            }

            return null;
        }

        public void SetObjectToPool(GameObject obj)
        {
            obj.SetActive(false);
            _pooledObjects.Add(obj);
        }
        
    }
}
