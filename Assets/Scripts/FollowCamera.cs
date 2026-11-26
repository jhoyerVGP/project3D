using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 lookOffset = new Vector3(0f, 1.5f, 0f);
    public float distance = 4f;
    public float sensitivity = 3f;
    public float minPitch = -10f;
    public float maxPitch = 60f;

    float yaw;
    float pitch = 15f;

    void Start()
    {
        yaw = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (target == null) return;

        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 pivot = target.position + lookOffset;

        transform.position = pivot - rot * Vector3.forward * distance;
        transform.rotation = rot;
    }
}