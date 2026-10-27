
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 8f, -8f);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 posisiTujuan = target.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            posisiTujuan,
            smoothSpeed * Time.deltaTime
        );

        transform.LookAt(target);
    }
}
