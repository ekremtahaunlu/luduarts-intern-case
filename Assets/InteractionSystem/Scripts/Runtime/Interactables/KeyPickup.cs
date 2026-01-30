using InteractionSystem.Runtime.Core;
using InteractionSystem.Runtime.Player;
using UnityEngine;

namespace InteractionSystem.Runtime.Interactables
{
    public class KeyPickup : MonoBehaviour, IInteractable
    {
        #region Fields

        [Header("Key Settings")]
        [SerializeField] private KeyItem m_KeyData;

        #endregion

        #region IInteractable Implementation

        public void Interact(GameObject interactor)
        {
            if (m_KeyData == null)
            {
                Debug.LogError($"KeyPickup: KeyData is missing on {gameObject.name}!");
                return;
            }

            var inventory = interactor.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddKey(m_KeyData);
                Destroy(gameObject);
            }
        }

        public string GetInteractionPrompt()
        {
            return $"Pick up {m_KeyData.KeyName}";
        }

        public bool CanInteract => true;

        #endregion
    }
}