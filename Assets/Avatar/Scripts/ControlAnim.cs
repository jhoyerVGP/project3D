using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator), typeof(CharacterController))]
public class ControlAnim : MonoBehaviour
{
    [SerializeField] private float velocidad = 1.5f;
    [SerializeField, Min(0.1f)] private float alturaSalto = 1f;

    private const float Gravedad = -18f;

    private Animator animator;
    private CharacterController controlador;
    private float velocidadVertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controlador = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var teclado = Keyboard.current;
        Vector3 direccion = Vector3.zero;
        bool enSuelo = controlador.isGrounded;

        if (enSuelo && velocidadVertical < 0f)
        {
            velocidadVertical = -2f;
        }

        if (teclado != null)
        {
            if (teclado.spaceKey.wasPressedThisFrame && enSuelo)
            {
                velocidadVertical = Mathf.Sqrt(alturaSalto * -2f * Gravedad);
                animator.SetTrigger("Saltar");
            }
            else if (teclado.tKey.wasPressedThisFrame)
            {
                animator.SetTrigger("Golpear");
            }
            else if (teclado.yKey.wasPressedThisFrame)
            {
                animator.SetTrigger("GolpearDerecho");
            }
            else if (teclado.gKey.wasPressedThisFrame)
            {
                animator.SetTrigger("Patear");
            }

            if (teclado.wKey.isPressed) direccion += Vector3.forward;
            if (teclado.sKey.isPressed) direccion += Vector3.back;
            if (teclado.aKey.isPressed) direccion += Vector3.left;
            if (teclado.dKey.isPressed) direccion += Vector3.right;
        }

        bool caminando = direccion.sqrMagnitude > 0f;
        animator.SetBool("Caminando", caminando);

        if (caminando)
        {
            direccion.Normalize();
            transform.rotation = Quaternion.LookRotation(direccion);
        }

        velocidadVertical += Gravedad * Time.deltaTime;

        Vector3 movimiento = direccion * velocidad;
        movimiento.y = velocidadVertical;
        controlador.Move(movimiento * Time.deltaTime);
    }
}