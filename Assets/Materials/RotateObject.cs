using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 30f;  // Rotation speed in degrees per second
    private bool isRotating = false;   // Flag to check if rotation is active

    // Update is called once per frame
    void Update()
    {
        // Rotate only if the rotation is active
        if (isRotating)
        {
            RotateOnXAxis();
        }
    }

    // Function to rotate the object on the X-axis
    private void RotateOnXAxis()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
    }

    // Function to toggle rotation (start/stop)
    public void ToggleRotation()
    {
        // Toggle the rotation state
        isRotating = !isRotating;
    }
}
