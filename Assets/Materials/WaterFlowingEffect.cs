using UnityEngine;

public class WaterFlowingEffect : MonoBehaviour
{
    public Material waterMaterial; // Reference to the water material
    public float flowSpeed = 0.1f; // Speed of the flow effect (increase for faster flow)

    private Vector2 currentBaseMapOffset;
    private Vector2 currentNormalMapOffset;

    void Start()
    {
        // Ensure the material is assigned
        if (waterMaterial == null)
        {
            Debug.LogError("Water material is not assigned.");
            return;
        }

        // Initialize offsets for both Base Map (Albedo) and Normal Map
        currentBaseMapOffset = waterMaterial.GetTextureOffset("_BaseMap");
        currentNormalMapOffset = waterMaterial.GetTextureOffset("_BumpMap");

        Debug.Log("Initial BaseMap Offset: " + currentBaseMapOffset); // Debugging line
        Debug.Log("Initial NormalMap Offset: " + currentNormalMapOffset); // Debugging line
    }

    void Update()
    {
        if (waterMaterial != null)
        {
            // Update the texture offset for both base texture (Albedo) and normal map
            currentBaseMapOffset.x += flowSpeed * Time.deltaTime; // Horizontal flow for Base Map
            currentNormalMapOffset.x += flowSpeed * Time.deltaTime; // Horizontal flow for Normal Map

            // Apply the new offsets to both textures
            waterMaterial.SetTextureOffset("_BaseMap", currentBaseMapOffset);
            waterMaterial.SetTextureOffset("_BumpMap", currentNormalMapOffset);

            // Debugging line to ensure the offsets are changing
            Debug.Log("Current BaseMap Offset: " + currentBaseMapOffset);
            Debug.Log("Current NormalMap Offset: " + currentNormalMapOffset);
        }
    }
}
