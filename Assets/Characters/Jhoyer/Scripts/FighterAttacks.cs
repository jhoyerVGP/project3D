using UnityEngine;
using UnityEngine.InputSystem;

public class FighterAttacks : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float comboWindow = 0.08f;

    private float leftPunchTime = -1f;
    private float rightPunchTime = -1f;
    private float leftKickTime = -1f;
    private float rightKickTime = -1f;

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        // Registrar cuándo se pulsó cada tecla
        if (kb.uKey.wasPressedThisFrame) leftPunchTime = Time.time;
        if (kb.iKey.wasPressedThisFrame) rightPunchTime = Time.time;
        if (kb.jKey.wasPressedThisFrame) leftKickTime = Time.time;
        if (kb.kKey.wasPressedThisFrame) rightKickTime = Time.time;

        // Puños
        if (leftPunchTime >= 0 && rightPunchTime >= 0)
        {
            animator.SetTrigger("DoublePunch");
            leftPunchTime = rightPunchTime = -1f;
        }
        else
        {
            if (leftPunchTime >= 0 && Time.time - leftPunchTime > comboWindow)
            {
                animator.SetTrigger("LeftPunch");
                leftPunchTime = -1f;
            }
            if (rightPunchTime >= 0 && Time.time - rightPunchTime > comboWindow)
            {
                animator.SetTrigger("RightPunch");
                rightPunchTime = -1f;
            }
        }

        // Patadas
        if (leftKickTime >= 0 && rightKickTime >= 0)
        {
            animator.SetTrigger("DoubleKick");
            leftKickTime = rightKickTime = -1f;
        }
        else
        {
            if (leftKickTime >= 0 && Time.time - leftKickTime > comboWindow)
            {
                animator.SetTrigger("LeftKick");
                leftKickTime = -1f;
            }
            if (rightKickTime >= 0 && Time.time - rightKickTime > comboWindow)
            {
                animator.SetTrigger("RightKick");
                rightKickTime = -1f;
            }
        }
    }
}