using UnityEngine;

public class MaterialSwitcher : MonoBehaviour
{
    // Prefabs to which materials will be applied
    public GameObject prefab1, prefab2, prefab3, prefab4, prefab5, prefab6, prefab7, prefab8;

    // Materials to be applied
    public Material redMaterial;
    public Material blueMaterial;
    public Material whiteMaterial;

    // Apply red material
    public void ApplyRedMaterial()
    {
        SetMaterialToAllPrefabs(redMaterial);
    }

    // Apply blue material
    public void ApplyBlueMaterial()
    {
        SetMaterialToAllPrefabs(blueMaterial);
    }

    // Apply white material
    public void ApplyWhiteMaterial()
    {
        SetMaterialToAllPrefabs(whiteMaterial);
    }

    // Helper function
    private void SetMaterialToAllPrefabs(Material material)
    {
        GameObject[] prefabs = { prefab1, prefab2, prefab3, prefab4, prefab5, prefab6, prefab7, prefab8 };

        foreach (var prefab in prefabs)
        {
            if (prefab != null)
            {
                Renderer prefabRenderer = prefab.GetComponent<Renderer>();
                if (prefabRenderer != null)
                {
                    prefabRenderer.material = material;
                }
                else
                {
                    Debug.LogWarning("Renderer missing on prefab: " + prefab.name);
                }
            }
            else
            {
                Debug.LogWarning("A prefab slot is empty in MaterialSwitcher.");
            }
        }
    }
}
