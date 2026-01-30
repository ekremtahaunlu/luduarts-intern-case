using UnityEngine;

namespace InteractionSystem.Runtime.Core
{
    [CreateAssetMenu(fileName = "NewKey", menuName = "InteractionSystem/Items/Key Item")]
    public class KeyItem : ScriptableObject
    {
        [Tooltip("Anahtarın UI'da görünecek ismi.")]
        [SerializeField] private string m_KeyName;

        public string KeyName => m_KeyName;
    }
}