using System.Collections.Generic;
using InteractionSystem.Runtime.Core;
using UnityEngine;

namespace InteractionSystem.Runtime.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        #region Fields

        [Header("Debug")]
        [SerializeField] private List<KeyItem> m_CollectedKeys = new List<KeyItem>();

        #endregion

        #region Methods

        /// <param name="key">Eklenecek anahtar verisi.</param>
        public void AddKey(KeyItem key)
        {
            if (key != null && !m_CollectedKeys.Contains(key))
            {
                m_CollectedKeys.Add(key);
                Debug.Log($"Inventory: Added key '{key.KeyName}'");
            }
        }

        /// <param name="key">Kontrol edilecek anahtar.</param>
        /// <returns>Varsa true döner.</returns>
        public bool HasKey(KeyItem key)
        {
            return m_CollectedKeys.Contains(key);
        }

        #endregion
    }
}