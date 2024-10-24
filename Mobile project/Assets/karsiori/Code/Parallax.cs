using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPosX, startPosY;
    public GameObject cam;  // The camera object or player
    public float parallaxEffectX = 0.5f;  // Horizontal parallax effect
    public float parallaxEffectY = 0.2f;  // Vertical parallax effect

    private float lengthX;

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Get the camera or player's position
        float distX = cam.transform.position.x * parallaxEffectX;  // Horizontal distance to move background
        float distY = cam.transform.position.y * parallaxEffectY;  // Vertical distance to move background

        // Move the background based on camera position
        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

        // Loop the background horizontally
        if (cam.transform.position.x * (1 - parallaxEffectX) > startPosX + lengthX)
        {
            startPosX += lengthX;
        }
        else if (cam.transform.position.x * (1 - parallaxEffectX) < startPosX - lengthX)
        {
            startPosX -= lengthX;
        }
    }
}