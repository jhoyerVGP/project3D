using UnityEngine;

public class ControlAnimacionesCristian : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("Punch");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("Kick");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            anim.SetTrigger("Roll");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            anim.SetTrigger("Death");
        }
    }
}
