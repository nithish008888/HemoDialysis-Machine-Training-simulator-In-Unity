using UnityEngine;

public class FlowingMaterialEffect : MonoBehaviour
{
    // Expose the material field in the Inspector
    public Material material;

    // Speed of the flow effect (how fast the texture moves)
    public float flowSpeedX = 0.1f; // Horizontal flow speed
    public float flowSpeedY = 0.1f; // Vertical flow speed

    // Variable to hold the offset value
    private Vector2 currentOffset;

    void Update()
    {
        if (material != null)
        {
            // Get the current offset from the material
            currentOffset = material.GetTextureOffset("_MainTex");

            // Update the offset based on time and speed
            currentOffset.x += flowSpeedX * Time.deltaTime;
            currentOffset.y += flowSpeedY * Time.deltaTime;

            // Apply the new offset to the material
            material.SetTextureOffset("_MainTex", currentOffset);
        }
    }
}
