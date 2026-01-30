using System.Collections.Generic;
using System.Text; // StringBuilder için gerekli
using InteractionSystem.Runtime.Core;
using UnityEngine;
using TMPro; // TextMeshPro

namespace InteractionSystem.Runtime.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        #region Fields

        [Header("UI References")]
        [Tooltip("Envanterdeki eşyaların listeleneceği Text alanı.")]
        [SerializeField] private TextMeshProUGUI m_InventoryListText;

        [Header("Data")]
        [SerializeField] private List<KeyItem> m_CollectedKeys = new List<KeyItem>();

        #endregion

        #region Unity Methods

        private void Start()
        {
            UpdateUI(); // Başlangıçta listeyi temizle/güncelle
        }

        #endregion

        #region Methods

        public void AddKey(KeyItem key)
        {
            if (key != null && !m_CollectedKeys.Contains(key))
            {
                m_CollectedKeys.Add(key);
                Debug.Log($"Inventory: Added key '{key.KeyName}'");

                // UI'ı güncelle
                UpdateUI();
            }
        }

        public bool HasKey(KeyItem key)
        {
            return m_CollectedKeys.Contains(key);
        }

        private void UpdateUI()
        {
            if (m_InventoryListText == null) return;

            if (m_CollectedKeys.Count == 0)
            {
                m_InventoryListText.text = "Inventory: Empty";
                return;
            }

            // String birleştirme işlemi (Performans için StringBuilder)
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Inventory:");

            foreach (var key in m_CollectedKeys)
            {
                sb.AppendLine($"- {key.KeyName}");
            }

            m_InventoryListText.text = sb.ToString();
        }

        #endregion
    }
}