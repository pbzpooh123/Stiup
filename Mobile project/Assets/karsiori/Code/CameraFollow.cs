using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public Vector3 offset;    // Offset to keep the camera at a distance from the player
    public float smoothSpeed = 0.125f;  // Smoothness factor for camera movement

    // Boundaries for clamping the camera
    public float minX, maxX, minY, maxY;

    void LateUpdate()
    {
        // Desired position follows the player only on the x and y axes (ignore z-axis in 2D)
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

        // Smooth camera movement
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Clamp the camera's position to stay within the specified boundaries
        float clampedX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minY, maxY);

        // Apply the clamped position to the camera
        transform.position = new Vector3(clampedX, clampedY, smoothedPosition.z);
    }
}