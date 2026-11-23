using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class ControlCaminar : MonoBehaviour
{
    [SerializeField] private float velocidad = 1.5f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var teclado = Keyboard.current;
        Vector3 direccion = Vector3.zero;

        if (teclado != null)
        {
            if (teclado.wKey.isPressed) direccion += Vector3.forward;
            if (teclado.sKey.isPressed) direccion += Vector3.back;
            if (teclado.aKey.isPressed) direccion += Vector3.left;
            if (teclado.dKey.isPressed) direccion += Vector3.right;
        }

        bool caminando = direccion.sqrMagnitude > 0f;
        animator.SetBool("Caminando", caminando);

        if (!caminando)
            return;

        direccion.Normalize();
        transform.rotation = Quaternion.LookRotation(direccion);
        transform.position += direccion * velocidad * Time.deltaTime;
    }
}