using UnityEngine;

namespace InteractionSystem.Runtime.Core
{
    public interface IInteractable
    {
        /// <param name="interactor">Etkileşimi başlatan obje (Genelde Player).</param>
        void Interact(GameObject interactor);

        /// <returns>Etkileşim mesajı (Örn: "Open Door").</returns>
        string GetInteractionPrompt();
        bool CanInteract { get; }
    }
}