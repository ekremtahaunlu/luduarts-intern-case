using InteractionSystem.Runtime.Core;
using UnityEngine;
using UnityEngine.Events; // UnityEvent için gerekli

namespace InteractionSystem.Runtime.Interactables
{
    /// <summary>
    /// Etkileşime geçildiğinde başka nesneleri (Kapı, Işık vb.) tetikleyen şalter.
    /// </summary>
    public class Switch : MonoBehaviour, IInteractable
    {
        #region Fields

        [Header("Interaction Settings")]
        [SerializeField] private string m_PromptMessage = "Use Lever";

        [Header("Events")]
        [Tooltip("Şalter açıldığında tetiklenecek olaylar.")]
        [SerializeField] private UnityEvent m_OnActivate;

        [Tooltip("Şalter kapandığında tetiklenecek olaylar.")]
        [SerializeField] private UnityEvent m_OnDeactivate;

        [Header("Animation")]
        [SerializeField] private Transform m_HandleVisual;
        [SerializeField] private float m_HandleAngle = 45f;
        [SerializeField] private float m_AnimSpeed = 5f;

        // Private State
        private bool m_IsActive;
        private Quaternion m_InitialRotation;
        private Quaternion m_TargetRotation;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (m_HandleVisual != null)
            {
                m_InitialRotation = m_HandleVisual.localRotation;
                m_TargetRotation = m_InitialRotation;
            }
        }

        private void Update()
        {
            if (m_HandleVisual != null)
            {
                m_HandleVisual.localRotation = Quaternion.Slerp(
                    m_HandleVisual.localRotation,
                    m_TargetRotation,
                    Time.deltaTime * m_AnimSpeed
                );
            }
        }

        #endregion

        #region IInteractable Implementation

        public InteractionType InteractionType => InteractionType.Toggle;
        public float HoldDuration => 0f;
        public bool CanInteract => true;

        public void Interact(GameObject interactor)
        {
            ToggleSwitch();
        }

        public string GetInteractionPrompt()
        {
            return m_IsActive ? "Deactivate" : "Activate";
        }

        #endregion

        #region Methods

        private void ToggleSwitch()
        {
            m_IsActive = !m_IsActive;

            if (m_IsActive)
            {
                // Kolu aşağı indir
                m_TargetRotation = m_InitialRotation * Quaternion.Euler(m_HandleAngle, 0, 0);
                m_OnActivate?.Invoke(); // Eventi tetikle
            }
            else
            {
                // Kolu yukarı kaldır
                m_TargetRotation = m_InitialRotation;
                m_OnDeactivate?.Invoke(); // Eventi tetikle
            }
        }

        #endregion
    }
}