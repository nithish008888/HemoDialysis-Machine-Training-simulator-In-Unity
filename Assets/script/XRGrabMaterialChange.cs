using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class XRGrabMaterialChange : MonoBehaviour
{
    public Material originalMaterial;
    public Material glowingMaterial;
    public GameObject targetObject;
    private Renderer targetObjectRenderer;
    private Material targetOriginalMaterial;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socketInteractor;

    private bool isBeingGrabbed = false;
    public AudioSource grabSound;

    private Coroutine glowCoroutine;

    void Start()
    {
        // Get renderer
        if (targetObject != null)
        {
            targetObjectRenderer = targetObject.GetComponent<Renderer>();
            if (targetObjectRenderer != null)
            {
                targetOriginalMaterial = targetObjectRenderer.material;
            }
        }

        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        socketInteractor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }

        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.AddListener(OnSocketPlaced);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // ✅ Only run if the grabbed interactor has tag "gr"
        if (args.interactorObject.transform.CompareTag("gr"))
        {
            isBeingGrabbed = true;

            // Play grab sound
            if (grabSound != null)
                grabSound.Play();

            // Start glowing coroutine
            if (glowCoroutine == null)
                glowCoroutine = StartCoroutine(GlowEffect());
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (!isBeingGrabbed)
            return;

        isBeingGrabbed = false;

        // Stop coroutine and revert material
        if (glowCoroutine != null)
        {
            StopCoroutine(glowCoroutine);
            glowCoroutine = null;
        }

        if (targetObjectRenderer != null && targetOriginalMaterial != null)
            targetObjectRenderer.material = targetOriginalMaterial;
    }

    private void OnSocketPlaced(SelectEnterEventArgs args)
    {
        if (!isBeingGrabbed)
        {
            // Stop coroutine
            if (glowCoroutine != null)
            {
                StopCoroutine(glowCoroutine);
                glowCoroutine = null;
            }

            // Reset and disable
            if (targetObjectRenderer != null && targetOriginalMaterial != null)
                targetObjectRenderer.material = targetOriginalMaterial;

            enabled = false;
        }
    }

    private IEnumerator GlowEffect()
    {
        if (targetObjectRenderer == null || glowingMaterial == null)
            yield break;

        // While grabbed, set glowing material
        targetObjectRenderer.material = glowingMaterial;

        // Optional pulse or blink effect
        while (isBeingGrabbed)
        {
            targetObjectRenderer.material = glowingMaterial;
            yield return new WaitForSeconds(0.5f);
            targetObjectRenderer.material = targetOriginalMaterial;
            yield return new WaitForSeconds(0.5f);
        }

        // Ensure it reverts when done
        targetObjectRenderer.material = targetOriginalMaterial;
    }

    void OnDisable()
    {
        if (targetObjectRenderer != null && targetOriginalMaterial != null)
            targetObjectRenderer.material = targetOriginalMaterial;
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }

        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnSocketPlaced);
        }
    }
}
