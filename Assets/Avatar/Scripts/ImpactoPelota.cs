using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ImpactoPelota : MonoBehaviour
{
    [SerializeField] private SphereCollider pelota;
    [SerializeField] private float alcance = 0.25f;
    [SerializeField] private float impulsoPuno = 3f;
    [SerializeField] private float impulsoPatada = 5f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ImpactoIzquierdo()
    {
        EmpujarPelota(HumanBodyBones.LeftHand, impulsoPuno);
    }

    public void ImpactoDerecho()
    {
        EmpujarPelota(HumanBodyBones.RightHand, impulsoPuno);
    }

    public void ImpactoPatada()
    {
        EmpujarPelota(HumanBodyBones.LeftFoot, impulsoPatada);
    }

    private void EmpujarPelota(HumanBodyBones hueso, float impulso)
    {
        if (pelota == null || pelota.attachedRigidbody == null)
            return;

        Transform extremidad = animator.GetBoneTransform(hueso);
        if (extremidad == null)
            return;

        Vector3 puntoCercano = pelota.ClosestPoint(extremidad.position);
        if (Vector3.Distance(extremidad.position, puntoCercano) > alcance)
            return;

        Vector3 direccion = pelota.bounds.center - transform.position;
        direccion.y = 0f;
        pelota.attachedRigidbody.AddForce(
            direccion.normalized * impulso, ForceMode.Impulse);
    }
}