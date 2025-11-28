using UnityEngine;

public class EnableOnRigTrigger : MonoBehaviour
{
    [Header("Object to Enable")]
    public GameObject objectToEnable;   // Assign in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rig"))
        {
            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
            }
            else
            {
                Debug.LogWarning("objectToEnable is not assigned!");
            }
        }
    }
}
