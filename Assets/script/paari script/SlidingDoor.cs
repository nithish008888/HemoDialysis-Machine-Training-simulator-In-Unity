using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SlidingDoor : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The actual mesh (child) that will move. Usually a child of this GameObject.")]
    public Transform doorTransform;

    [Tooltip("Player or camera transform used to decide which side to open toward.")]
    public Transform playerTransform;

    [Header("Movement")]
    [Tooltip("How far the door slides in local units.")]
    public float openDistance = 2f;

    [Tooltip("Seconds it takes to move fully.")]
    public float moveTime = 0.4f;

    [Tooltip("Which local axis to slide on (use X,Y or Z).")]
    public Axis slideAxis = Axis.X;

    [Tooltip("If true, pressing again closes the door.")]
    public bool toggleOnInteract = true;

    [Header("Trigger / Interaction")]
    [Tooltip("If assigned, this script will auto-open when player enters this trigger.")]
    public Collider interactionTrigger;

    // private state
    Vector3 closedLocalPos;
    Vector3 openLocalPos;
    Vector3 velocity = Vector3.zero;
    bool isOpen = false;
    bool moving = false;
    float currentLerp = 0f; // 0 closed, 1 open

    public enum Axis { X, Y, Z }

    void Start()
    {
        if (doorTransform == null)
        {
            Debug.LogError("SlidingDoor: doorTransform not assigned. Assign the child mesh transform.");
            enabled = false;
            return;
        }

        // store closed local position
        closedLocalPos = doorTransform.localPosition;

        // initial openLocalPos (we'll choose sign later on interact)
        openLocalPos = closedLocalPos;

        // ensure trigger is set up to call OnTriggerEnter if using trigger auto-open
        if (interactionTrigger != null && interactionTrigger.isTrigger == false)
            Debug.LogWarning("SlidingDoor: interactionTrigger should be set as isTrigger = true.");
    }

    void Update()
    {
        if (moving)
        {
            // smooth step; you can use Mathf.SmoothStep or SmoothDamp; here we do SmoothDamp on currentLerp
            float target = isOpen ? 1f : 0f;
            currentLerp = Mathf.MoveTowards(currentLerp, target, Time.deltaTime / Mathf.Max(0.0001f, moveTime));
            doorTransform.localPosition = Vector3.Lerp(closedLocalPos, openLocalPos, currentLerp);

            if (Mathf.Approximately(currentLerp, target))
                moving = false;
        }
    }

    // Public method to trigger the door from other code (XR input, button, etc.)
    public void Interact()
    {
        // If toggle mode: flip state; otherwise always open
        if (toggleOnInteract)
            isOpen = !isOpen;
        else
            isOpen = true;

        // When opening, compute direction based on player position so it opens away from player
        if (isOpen && playerTransform != null)
            ComputeOpenPositionAwayFromPlayer();

        // start moving
        moving = true;
    }

    void ComputeOpenPositionAwayFromPlayer()
    {
        // local axis unit vector in local space
        Vector3 localAxis = AxisToVector(slideAxis);

        // world direction from door to player
        Vector3 toPlayerWorld = playerTransform.position - doorTransform.position;

        // convert world direction into door's local space:
        Vector3 toPlayerLocal = transform.InverseTransformDirection(toPlayerWorld);

        // Decide sign: we want to slide away from the player, so if player is positive along the localAxis, slide negative
        float dot = Vector3.Dot(toPlayerLocal, localAxis);

        float sign = dot >= 0f ? -1f : 1f; // if player is in front (positive), open backwards (negative)
        Vector3 offsetLocal = localAxis * (openDistance * sign);

        openLocalPos = closedLocalPos + offsetLocal;
    }

    Vector3 AxisToVector(Axis a)
    {
        switch (a)
        {
            case Axis.X: return Vector3.right;
            case Axis.Y: return Vector3.up;
            default:     return Vector3.forward;
        }
    }

    // Optional: auto open on trigger enter
    void OnTriggerEnter(Collider other)
    {
        if (interactionTrigger == null) return;

        if (other.transform == playerTransform || other.CompareTag("Player"))
        {
            isOpen = true;
            if (playerTransform != null) ComputeOpenPositionAwayFromPlayer();
            moving = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (interactionTrigger == null) return;

        if (other.transform == playerTransform || other.CompareTag("Player"))
        {
            isOpen = false;
            moving = true;
        }
    }

    // Debug: draw local open direction gizmo
    void OnDrawGizmosSelected()
    {
        if (doorTransform == null) return;
        Gizmos.color = Color.cyan;
        Vector3 a = doorTransform.position;
        Vector3 localAxis = transform.TransformDirection(AxisToVector(slideAxis));
        Gizmos.DrawLine(a, a + localAxis * openDistance);
    }
}
