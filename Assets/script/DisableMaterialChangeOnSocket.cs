using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableMaterialChangeOnSocket : MonoBehaviour
{
    // Reference to the XRSocketInteractor component on Object B
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socketInteractor;

    // Reference to the Prefab of Object A (to assign in the Inspector)
    public GameObject objectAPrefab;

    // Reference to the XRGrabMaterialChange script on Object A (this will be assigned at runtime)
    private XRGrabMaterialChange grabMaterialChangeScript;

    void Start()
    {
        // Ensure the socketInteractor reference is assigned in the Inspector
        if (socketInteractor != null)
        {
            // Subscribe to the selectEntered event to detect when an object is placed in the socket
            socketInteractor.selectEntered.AddListener(OnObjectPlacedInSocket);
        }

        // Ensure the objectAPrefab is assigned in the Inspector
        if (objectAPrefab != null)
        {
            // Get the XRGrabMaterialChange script from the Prefab of Object A
            grabMaterialChangeScript = objectAPrefab.GetComponent<XRGrabMaterialChange>();

            if (grabMaterialChangeScript == null)
            {
                Debug.LogError("XRGrabMaterialChange script not found on Object A Prefab!");
            }
        }
        else
        {
            Debug.LogError("Object A Prefab is not assigned in the Inspector!");
        }
    }

    // This function will be called when any object enters the socket
    private void OnObjectPlacedInSocket(SelectEnterEventArgs args)
    {
        // If Object B has an object in its socket, disable the material change script on Object A
        if (grabMaterialChangeScript != null)
        {
            grabMaterialChangeScript.enabled = false;
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnObjectPlacedInSocket);
        }
    }
}
