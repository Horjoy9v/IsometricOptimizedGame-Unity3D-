using UnityEngine;
using Unity.Burst;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private byte positionSmoothing = 10;
    private byte lookAtSmoothing = 10;
    public Vector3 offset;

    private Vector3 currentLookAt;
    private void Start()
    {
        target = GameObject.FindWithTag("Hero").GetComponent<Transform>();
        currentLookAt = target.position;
    }

    private void FixedUpdate()
    {
        SmoothFollowAndLookAtTarget();
    }

    [BurstCompile]
    public void SmoothFollowAndLookAtTarget()
    {
        Vector3 targetCamPos = target.position + offset;

        targetCamPos.y = transform.position.y;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, positionSmoothing * Time.deltaTime);
        currentLookAt = Vector3.Lerp(currentLookAt, target.position, lookAtSmoothing * Time.deltaTime);
        transform.LookAt(currentLookAt);
    }
}
