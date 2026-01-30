using InteractionSystem.Runtime.Core;
using InteractionSystem.Runtime.Player;
using UnityEngine;

namespace InteractionSystem.Runtime.Interactables
{
    /// <summary>
    /// Etkileşime geçildiğinde açılıp kapanabilen, kilitlenebilen ve uzaktan tetiklenebilen kapı sınıfı.
    /// </summary>
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
            // Basit animasyon (Slerp ile yumuşak geçiş)
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

        // Kapı 'Toggle' (Aç/Kapa) tipindedir
        public InteractionType InteractionType => InteractionType.Toggle;

        // Basılı tutmaya gerek yok
        public float HoldDuration => 0f;

        public bool CanInteract => true;

        /// <summary>
        /// Kapı etkileşimi: Kilitli mi? Anahtar var mı? Aç/Kapa.
        /// </summary>
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

        /// <summary>
        /// Duruma göre (Kilitli/Açık/Kapalı) kullanıcıya mesaj gösterir.
        /// </summary>
        public string GetInteractionPrompt()
        {
            if (m_IsLocked)
            {
                string keyName = m_RequiredKey != null ? m_RequiredKey.KeyName : "Key";
                return $"Locked ({keyName} Required)";
            }

            return m_IsOpen ? "Close Door" : "Open Door";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Envanter kontrolü yaparak kapıyı açmayı dener.
        /// </summary>
        private void TryUnlock(GameObject interactor)
        {
            var inventory = interactor.GetComponent<PlayerInventory>();

            if (inventory != null && m_RequiredKey != null)
            {
                if (inventory.HasKey(m_RequiredKey))
                {
                    Unlock();
                    // Kilit açıldığında otomatik olarak kapıyı da açmak istersen:
                    // ToggleDoor(); 
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

        /// <summary>
        /// Kapıyı dışarıdan (örn: Şalter ile veya Interact ile) açıp kapatmak için kullanılır.
        /// Public olduğu için UnityEvent ile bağlanabilir.
        /// </summary>
        public void ToggleDoor()
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

        /// <summary>
        /// Kapının kilidini dışarıdan açar (Anahtarsız).
        /// Şalter veya özel eventler için kullanılır.
        /// </summary>
        public void Unlock()
        {
            m_IsLocked = false;
            Debug.Log("Door unlocked!");
        }

        #endregion
    }
}