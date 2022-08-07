using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.PopUp
{
    public class PopUpManager : MonoBehaviour
    {
        private List<Entities.PopUp> Popups = new List<Entities.PopUp>();

        private ObjectPool _objectPool;
        public static PopUpManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _objectPool = ObjectPool.Instance;
        }
        

        public void ShowPopUp(string popUpId)
        {
            GameObject popup = Instance._objectPool.GetObjectFromPool(popUpId);
            if (popup != null)
            {
                Instance.Popups.Add(new Entities.PopUp
                {
                    popUpId = popUpId,
                    popUpPrefab = popup
                });
            }
        }

        public void HidePopUp()
        {
            var popup = Instance.Popups[Instance.Popups.Count - 1];
            Instance.Popups.Remove(popup);
            Instance._objectPool.SetObjectToPool(popup.popUpPrefab);
        }
    }
}