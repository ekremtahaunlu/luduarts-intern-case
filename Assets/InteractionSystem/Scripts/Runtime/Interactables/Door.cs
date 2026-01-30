using InteractionSystem.Runtime.Core;
using InteractionSystem.Runtime.Player;
using UnityEngine;

namespace InteractionSystem.Runtime.Interactables
{
    public class Door : MonoBehaviour, IInteractable
    {
        #region Fields

        [Header("Door Settings")]
        [Tooltip("Kapının kilitli olup olmadığı.")]
        [SerializeField] private bool m_IsLocked;

        [Tooltip("Eğer kilitliyse açmak için gereken anahtar (Boş bırakılabilir).")]
        [SerializeField] private KeyItem m_RequiredKey;

        [Header("Animation")]
        [Tooltip("Kapı görseli (Dönme hareketi yapacak obje).")]
        [SerializeField] private Transform m_DoorVisual;

        [SerializeField] private float m_OpenAngle = 90f;
        [SerializeField] private float m_AnimationSpeed = 2f;

        // Private logic states
        private bool m_IsOpen;
        private Quaternion m_ClosedRotation;
        private Quaternion m_TargetRotation;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (m_DoorVisual != null)
            {
                m_ClosedRotation = m_DoorVisual.localRotation;
                m_TargetRotation = m_ClosedRotation;
            }
            else
            {
                Debug.LogError($"Door: Visual transform is missing on {gameObject.name}!");
            }
        }

        private void Update()
        {
            if (m_DoorVisual != null)
            {
                m_DoorVisual.localRotation = Quaternion.Slerp(
                    m_DoorVisual.localRotation,
                    m_TargetRotation,
                    Time.deltaTime * m_AnimationSpeed
                );
            }
        }

        #endregion

        #region IInteractable Implementation

        public void Interact(GameObject interactor)
        {
            if (m_IsLocked)
            {
                TryUnlock(interactor);
            }
            else
            {
                ToggleDoor();
            }
        }

        public string GetInteractionPrompt()
        {
            if (m_IsLocked)
            {
                string keyName = m_RequiredKey != null ? m_RequiredKey.KeyName : "Key";
                return $"Locked ({keyName} Required)";
            }

            return m_IsOpen ? "Close Door" : "Open Door";
        }

        public bool CanInteract => true;

        #endregion

        #region Private Methods

        private void TryUnlock(GameObject interactor)
        {
            var inventory = interactor.GetComponent<PlayerInventory>();

            if (inventory != null && m_RequiredKey != null)
            {
                if (inventory.HasKey(m_RequiredKey))
                {
                    m_IsLocked = false;
                    Debug.Log("Door unlocked!");
                }
                else
                {
                    Debug.Log("Door is locked. You need the key.");
                }
            }
            else
            {
                Debug.Log("No inventory found or no key assigned to door.");
            }
        }

        private void ToggleDoor()
        {
            m_IsOpen = !m_IsOpen;

            if (m_IsOpen)
            {
                // Mevcut rotasyon * Açılma açısı
                m_TargetRotation = m_ClosedRotation * Quaternion.Euler(0, m_OpenAngle, 0);
            }
            else
            {
                m_TargetRotation = m_ClosedRotation;
            }
        }

        #endregion
    }
}