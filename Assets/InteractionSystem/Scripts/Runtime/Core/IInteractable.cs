using UnityEngine;

namespace InteractionSystem.Runtime.Core
{
    public enum InteractionType
    {
        Instant,
        Hold,
        Toggle
    }

    public interface IInteractable
    {
        void Interact(GameObject interactor);
        string GetInteractionPrompt();
        bool CanInteract { get; }

        InteractionType InteractionType { get; }

        float HoldDuration { get; }
    }
}