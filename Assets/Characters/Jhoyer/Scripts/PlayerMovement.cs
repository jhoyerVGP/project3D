using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float runSpeed = 5f;
    public float rotationSpeed = 12f;
    public float gravity = -20f;

    [Header("Referencias")]
    public Animator animator;

    CharacterController controller;
    float verticalVelocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3(h, 0f, v);
        input = Vector3.ClampMagnitude(input, 1f);

        // Dirección relativa a la cámara
        Transform cam = Camera.main.transform;
        Vector3 camForward = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(cam.right, Vector3.up).normalized;
        Vector3 moveDir = camForward * input.z + camRight * input.x;

        bool moving = moveDir.sqrMagnitude > 0.01f;

        // Gira hacia donde quiere ir y avanza siempre hacia adelante
        if (moving)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        Vector3 move = moving ? transform.forward * runSpeed : Vector3.zero;

        // Gravedad
        if (controller.isGrounded && verticalVelocity < 0f) verticalVelocity = -2f;
        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);

        // 0 = idle de pelea, 1 = corriendo
        if (animator != null)
            animator.SetFloat("Speed", moving ? 1f : 0f, 0.1f, Time.deltaTime);
    }
}