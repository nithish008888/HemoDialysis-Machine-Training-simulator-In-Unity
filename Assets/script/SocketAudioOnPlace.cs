using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketAudioOnPlace : MonoBehaviour
{
    // Reference to the AudioSource for the sound effect
    public AudioSource socketAudioSource;

    // Reference to the XRSocketInteractor
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socketInteractor;

    void Start()
    {
        // Get the XRSocketInteractor component attached to the same GameObject
        socketInteractor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();

        // Ensure we have a valid AudioSource to play the sound
        if (socketAudioSource == null)
        {
            Debug.LogError("AudioSource not assigned on " + gameObject.name);
        }

        if (socketInteractor != null)
        {
            // Subscribe to the selectEntered event to detect when an object is placed in the socket
            socketInteractor.selectEntered.AddListener(OnObjectPlaced);
        }
        else
        {
            Debug.LogError("XRSocketInteractor not found on this GameObject.");
        }
    }

    // This method will be called when an object is placed into the socket
    private void OnObjectPlaced(SelectEnterEventArgs args)
    {
        // Check if the AudioSource is assigned
        if (socketAudioSource != null)
        {
            // Play the audio when the object is placed in the socket
            socketAudioSource.Play();
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed to avoid memory leaks
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnObjectPlaced);
        }
    }
}
