using UnityEngine;

    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float velocity = 5f;
        [SerializeField] private float rotationSpeed = 12f;
        [SerializeField] private Transform modelRoot;
        bool controlling = true;

        private UnityEngine.CharacterController characterController;

        private void Awake()
        {
            characterController = GetComponent<UnityEngine.CharacterController>();

            if (modelRoot == null)
            {
                modelRoot = transform;
            }
        }

        private void Update()
        {
            if (!controlling) return;
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 input = new Vector3(horizontal, 0f, vertical).normalized;

            GetComponent<Animator>().SetBool("Walk", input != Vector3.zero);
            if (input == Vector3.zero)
            {
                return;
            }

            Transform cameraTransform = Camera.main != null ? Camera.main.transform : null;
            if (cameraTransform == null)
            {
                return;
            }

            Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1f, 0f, 1f)).normalized;
            Vector3 cameraRight = Vector3.Scale(cameraTransform.right, new Vector3(1f, 0f, 1f)).normalized;

            Vector3 moveDirection = (cameraForward * input.z + cameraRight * input.x).normalized;

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                modelRoot.rotation = Quaternion.Slerp(modelRoot.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            characterController.Move(moveDirection * velocity * Time.deltaTime);
        }
        public void Controlling(bool b)
        {
            controlling = b;
             GetComponent<Animator>().SetBool("Walk",false);
        }
    }