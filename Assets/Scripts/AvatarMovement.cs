using UnityEngine;
using UnityEngine.InputSystem;

public class AvatarMovement : MonoBehaviour
{
    public float speed = 1.5f;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.applyRootMotion = false;
    }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        float h = 0f, v = 0f;
        if (kb.dKey.isPressed) h += 1f;
        if (kb.aKey.isPressed) h -= 1f;
        if (kb.wKey.isPressed) v += 1f;
        if (kb.sKey.isPressed) v -= 1f;

        anim.SetFloat("Horizontal", h, 0.1f, Time.deltaTime);
        anim.SetFloat("Vertical", v, 0.1f, Time.deltaTime);

        Vector3 dir = new Vector3(h, 0f, v);
        if (dir.magnitude > 1f) dir.Normalize();
        transform.Translate(dir * speed * Time.deltaTime, Space.Self);
    }
}