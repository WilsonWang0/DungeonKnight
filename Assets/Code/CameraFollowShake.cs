using UnityEngine;

public class CameraFollowWithShake : MonoBehaviour
{
    public Transform target;
    private Vector3 initialLocalPos;

    void Start()
    {
        initialLocalPos = transform.localPosition;
    }

    void LateUpdate()
    {
        transform.localPosition = initialLocalPos + CameraShake.Instance.ShakeOffset;
    }
}
