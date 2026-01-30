using System;
using InteractionSystem.Runtime.Core;
using UnityEngine;
using TMPro; // TextMeshPro kullanımı için

namespace InteractionSystem.Runtime.Player
{
    public class InteractionDetector : MonoBehaviour
    {
        #region Fields

        // Serialized private fields (m_ prefix)
        [Header("Detection Settings")]
        [SerializeField] private float m_InteractionRange = 3f;
        [SerializeField] private LayerMask m_InteractableLayer;
        [SerializeField] private KeyCode m_InteractionKey = KeyCode.E;

        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI m_PromptText;
        [SerializeField] private GameObject m_PromptPanel;

        // Private instance fields
        private Camera m_MainCamera;
        private IInteractable m_CurrentInteractable;
        private float m_LastCheckTime;
        private const float k_CheckInterval = 0.1f;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_MainCamera = Camera.main;

            if (m_MainCamera == null)
            {
                Debug.LogError("InteractionDetector: MainCamera not found via Camera.main!");
            }
        }

        private void Update()
        {
            HandleDetection();
            HandleInput();
        }

        #endregion

        #region Methods
        private void HandleDetection()
        {
            if (Time.time - m_LastCheckTime < k_CheckInterval) return;
            m_LastCheckTime = Time.time;

            Ray ray = m_MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            // Raycast işlemi
            if (Physics.Raycast(ray, out hit, m_InteractionRange, m_InteractableLayer))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                // Yeni bir etkileşim nesnesi mi?
                if (interactable != null && interactable != m_CurrentInteractable)
                {
                    if (interactable.CanInteract)
                    {
                        m_CurrentInteractable = interactable;
                        ShowPrompt(m_CurrentInteractable.GetInteractionPrompt());
                    }
                    else
                    {
                        ClearInteraction();
                    }
                }
                else if (interactable == null)
                {
                    ClearInteraction();
                }
            }
            else
            {
                ClearInteraction();
            }
        }
        private void HandleInput()
        {
            if (m_CurrentInteractable != null && Input.GetKeyDown(m_InteractionKey))
            {
                m_CurrentInteractable.Interact(gameObject);
                ShowPrompt(m_CurrentInteractable.GetInteractionPrompt());
            }
        }

        private void ShowPrompt(string message)
        {
            if (m_PromptPanel != null) m_PromptPanel.SetActive(true);
            if (m_PromptText != null) m_PromptText.text = message;
        }

        private void ClearInteraction()
        {
            m_CurrentInteractable = null;
            if (m_PromptPanel != null) m_PromptPanel.SetActive(false);
        }

        #endregion
    }
}