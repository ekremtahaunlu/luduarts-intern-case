using InteractionSystem.Runtime.Core;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Image (Progress Bar) için

namespace InteractionSystem.Runtime.Player
{
    public class InteractionDetector : MonoBehaviour
    {
        #region Fields

        [Header("Detection Settings")]
        [SerializeField] private float m_InteractionRange = 3f;
        [SerializeField] private LayerMask m_InteractableLayer;
        [SerializeField] private KeyCode m_InteractionKey = KeyCode.E;

        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI m_PromptText;
        [SerializeField] private GameObject m_PromptPanel;
        [SerializeField] private Image m_ProgressBar;

        private Camera m_MainCamera;
        private IInteractable m_CurrentInteractable;
        private float m_LastCheckTime;
        private const float k_CheckInterval = 0.1f;

        private float m_CurrentHoldTime;
        private bool m_IsHolding;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_MainCamera = Camera.main;
            if (m_ProgressBar != null) m_ProgressBar.fillAmount = 0;
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

            if (Physics.Raycast(ray, out hit, m_InteractionRange, m_InteractableLayer))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null && interactable != m_CurrentInteractable)
                {
                    if (interactable.CanInteract)
                    {
                        m_CurrentInteractable = interactable;
                        ShowPrompt(m_CurrentInteractable.GetInteractionPrompt());
                        ResetHold();
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
            if (m_CurrentInteractable == null) return;

            switch (m_CurrentInteractable.InteractionType)
            {
                case InteractionType.Instant:
                case InteractionType.Toggle:
                    if (Input.GetKeyDown(m_InteractionKey))
                    {
                        ExecuteInteraction();
                    }
                    break;

                case InteractionType.Hold:
                    if (Input.GetKey(m_InteractionKey))
                    {
                        m_CurrentHoldTime += Time.deltaTime;
                        float progress = m_CurrentHoldTime / m_CurrentInteractable.HoldDuration;

                        if (m_ProgressBar != null) m_ProgressBar.fillAmount = progress;

                        if (m_CurrentHoldTime >= m_CurrentInteractable.HoldDuration)
                        {
                            ExecuteInteraction();
                            ResetHold();
                        }
                    }
                    else
                    {
                        ResetHold();
                    }
                    break;
            }
        }

        private void ExecuteInteraction()
        {
            m_CurrentInteractable.Interact(gameObject);
            ShowPrompt(m_CurrentInteractable.GetInteractionPrompt());
        }

        private void ResetHold()
        {
            m_CurrentHoldTime = 0f;
            if (m_ProgressBar != null) m_ProgressBar.fillAmount = 0f;
        }

        private void ShowPrompt(string message)
        {
            if (m_PromptPanel != null) m_PromptPanel.SetActive(true);
            if (m_PromptText != null) m_PromptText.text = message;
        }

        private void ClearInteraction()
        {
            m_CurrentInteractable = null;
            ResetHold();
            if (m_PromptPanel != null) m_PromptPanel.SetActive(false);
        }

        #endregion
    }
}