using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketDisableGrabMaterialChange : MonoBehaviour
{
    // Reference to the XRGrabMaterialChange script attached to the object
    public XRGrabMaterialChange grabMaterialChangeScript;

    // Reference to the XRSocketInteractor component (this socket)
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socketInteractor;

    void Start()
    {
        // Get the XRSocketInteractor component attached to this GameObject (the socket)
        socketInteractor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();

        if (socketInteractor != null)
        {
            // Subscribe to the selectEntered event for detecting object placement in socket
            socketInteractor.selectEntered.AddListener(OnObjectPlacedInSocket);
        }
    }

    // This method will be called when an object is placed into the socket
    private void OnObjectPlacedInSocket(SelectEnterEventArgs args)
    {
        // Access the gameObject of the interactable object placed in the socket
        GameObject placedObject = args.interactableObject.transform.gameObject;

        // Check if the object placed in the socket has the XRGrabMaterialChange script
        XRGrabMaterialChange materialChangeScript = placedObject.GetComponent<XRGrabMaterialChange>();
        
        if (materialChangeScript != null)
        {
            // Disable the XRGrabMaterialChange script when the object is placed in the socket
            materialChangeScript.enabled = false; // Disable the script
            Debug.Log("XRGrabMaterialChange script disabled after placement in the socket.");
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from events when the object is destroyed to avoid memory leaks
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnObjectPlacedInSocket);
        }
    }
}
