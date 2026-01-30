using InteractionSystem.Runtime.Core;
using InteractionSystem.Runtime.Player;
using UnityEngine;

namespace InteractionSystem.Runtime.Interactables
{
    /// <summary>
    /// Basılı tutarak (Hold) açılan ve içinden eşya veren sandık sınıfı.
    /// </summary>
    public class Chest : MonoBehaviour, IInteractable
    {
        #region Fields

        [Header("Interaction Settings")]
        [SerializeField] private float m_HoldDuration = 2.0f;
        [SerializeField] private string m_PromptMessage = "Open Chest";

        [Header("Loot Settings")]
        [Tooltip("Sandık açılınca verilecek anahtar (Opsiyonel).")]
        [SerializeField] private KeyItem m_KeyToGive;

        [Header("Animation")]
        [SerializeField] private Transform m_LidVisual;
        [SerializeField] private float m_OpenAngle = -110f;

        // Private State
        private bool m_IsOpened;
        private Quaternion m_ClosedRotation;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (m_LidVisual != null)
                m_ClosedRotation = m_LidVisual.localRotation;
        }

        private void Update()
        {
            if (m_IsOpened && m_LidVisual != null)
            {
                Quaternion targetRot = m_ClosedRotation * Quaternion.Euler(m_OpenAngle, 0, 0);
                m_LidVisual.localRotation = Quaternion.Slerp(m_LidVisual.localRotation, targetRot, Time.deltaTime * 5f);
            }
        }

        #endregion

        #region IInteractable Implementation

        public InteractionType InteractionType => InteractionType.Hold;
        public float HoldDuration => m_HoldDuration;
        public bool CanInteract => !m_IsOpened;

        public void Interact(GameObject interactor)
        {
            if (m_IsOpened) return;

            OpenChest(interactor);
        }

        public string GetInteractionPrompt()
        {
            return m_IsOpened ? "Empty" : m_PromptMessage;
        }

        #endregion

        #region Methods

        private void OpenChest(GameObject interactor)
        {
            m_IsOpened = true;
            Debug.Log("Chest Opened!");

            // Eşya verme mantığı
            if (m_KeyToGive != null)
            {
                var inventory = interactor.GetComponent<PlayerInventory>();
                if (inventory != null)
                {
                    inventory.AddKey(m_KeyToGive);
                }
            }
        }

        #endregion
    }
}