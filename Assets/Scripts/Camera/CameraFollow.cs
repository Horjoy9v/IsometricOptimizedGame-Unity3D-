using UnityEngine;
using Unity.Burst;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // The position that the camera will be following.
    public float positionSmoothing; // The speed with which the camera will be following.
    public float lookAtSmoothing;   // The speed with which the camera will be following.
    public Vector3 offset;               // The initial offset from the target.

    private Vector3 currentLookAt;

    private void Start()
    {
        currentLookAt = target.position;
    }

    private void FixedUpdate()
    {
        SmoothFollowAndLookAtTarget();
    }

    [BurstCompile]
    public void SmoothFollowAndLookAtTarget()
    {
        // Create a position the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + offset;

        // Set the Y position of the target camera position to the current Y position of the camera.
        targetCamPos.y = transform.position.y;

        // Smoothly interpolate between the camera's current position and its target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, positionSmoothing * Time.deltaTime);
        currentLookAt = Vector3.Lerp(currentLookAt, target.position, lookAtSmoothing * Time.deltaTime);
        transform.LookAt(currentLookAt);
    }
}
