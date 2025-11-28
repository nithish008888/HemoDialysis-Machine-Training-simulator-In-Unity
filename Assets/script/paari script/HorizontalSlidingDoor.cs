using UnityEngine;

public class HorizontalSlidingDoor : MonoBehaviour
{
    public Transform door;                // The door to move
    public float slideDistance = 2f;      // How far it slides horizontally
    public float moveSpeed = 2f;          // Speed of the door movement

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private bool isOpen = false;
    private bool isMoving = false;

    void Start()
    {
        closedPosition = door.localPosition;
        openPosition = closedPosition + Vector3.right * slideDistance; // Change to Vector3.left to slide the other way
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
