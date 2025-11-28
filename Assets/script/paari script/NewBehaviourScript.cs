using UnityEngine;

public class BackwardsSlidingDoor : MonoBehaviour
{
    public Transform door;                // The moving part of the door
    public float slideDistance = 2f;      // How far the door slides backward
    public float moveSpeed = 2f;          // Speed of sliding

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private bool isOpen = false;
    private bool isMoving = false;

    void Start()
    {
        // Save the original position as closed
        closedPosition = door.localPosition;

        // Backwards = negative Z in local space
        openPosition = closedPosition + Vector3.back * slideDistance;
    }

    public void ToggleDoor()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveDoor(isOpen ? closedPosition : openPosition));
            isOpen = !isOpen;
        }
    }

    System.Collections.IEnumerator MoveDoor(Vector3 targetPosition)
    {
        isMoving = true;

        while (Vector3.Distance(door.localPosition, targetPosition) > 0.01f)
        {
            door.localPosition = Vector3.MoveTowards(door.localPosition, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        door.localPosition = targetPosition;
        isMoving = false;
    }
}
