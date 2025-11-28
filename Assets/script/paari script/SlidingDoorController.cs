using UnityEngine;

public class SlidingDoorController : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public float slideDistance = 1.5f;
    public float moveSpeed = 2f;

    private Vector3 leftClosedPos;
    private Vector3 leftOpenPos;
    private Vector3 rightClosedPos;
    private Vector3 rightOpenPos;

    private bool isOpen = false;
    private bool isMoving = false;

    void Start()
    {
        leftClosedPos = leftDoor.localPosition;
        rightClosedPos = rightDoor.localPosition;

        leftOpenPos = leftClosedPos + Vector3.left * slideDistance;
        rightOpenPos = rightClosedPos + Vector3.right * slideDistance;
    }

    public void ToggleDoor()
    {
        if (!isMoving)
            StartCoroutine(MoveDoors());
    }

    System.Collections.IEnumerator MoveDoors()
    {
        isMoving = true;

        Vector3 leftTarget = isOpen ? leftClosedPos : leftOpenPos;
        Vector3 rightTarget = isOpen ? rightClosedPos : rightOpenPos;

        while (Vector3.Distance(leftDoor.localPosition, leftTarget) > 0.01f)
        {
            leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, leftTarget, moveSpeed * Time.deltaTime);
            rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, rightTarget, moveSpeed * Time.deltaTime);
            yield return null;
        }

        leftDoor.localPosition = leftTarget;
        rightDoor.localPosition = rightTarget;

        isOpen = !isOpen;
        isMoving = false;
    }
}
