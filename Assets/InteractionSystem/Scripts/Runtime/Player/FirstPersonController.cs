using UnityEngine;

namespace InteractionSystem.Runtime.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {
        #region Fields

        [Header("Movement Settings")]
        [SerializeField] private float m_MoveSpeed = 5f;
        [SerializeField] private float m_Gravity = -9.81f;

        [Header("Look Settings")]
        [SerializeField] private float m_MouseSensitivity = 2f;
        [SerializeField] private Transform m_CameraTransform;

        // Private fields
        private CharacterController m_CharacterController;
        private float m_CameraPitch = 0f;
        private Vector3 m_Velocity;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_CharacterController = GetComponent<CharacterController>();

            // Eğer kamera atanmadıysa otomatik bulmayı dene
            if (m_CameraTransform == null)
            {
                m_CameraTransform = GetComponentInChildren<Camera>()?.transform;
            }

            // Mouse imlecini gizle ve kilitle
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            HandleLook();
            HandleMovement();
        }

        #endregion

        #region Methods

        private void HandleLook()
        {
            if (m_CameraTransform == null) return;

            float mouseX = Input.GetAxis("Mouse X") * m_MouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * m_MouseSensitivity;

            transform.Rotate(Vector3.up * mouseX);

            m_CameraPitch -= mouseY;
            m_CameraPitch = Mathf.Clamp(m_CameraPitch, -90f, 90f);

            m_CameraTransform.localRotation = Quaternion.Euler(m_CameraPitch, 0f, 0f);
        }

        private void HandleMovement()
        {
            if (m_CharacterController.isGrounded && m_Velocity.y < 0)
            {
                m_Velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            m_CharacterController.Move(move * m_MoveSpeed * Time.deltaTime);

            m_Velocity.y += m_Gravity * Time.deltaTime;
            m_CharacterController.Move(m_Velocity * Time.deltaTime);
        }

        #endregion
    }
}