using UnityEngine;

public class BlockReceiver : MonoBehaviour
{
    public string requiredTag = "KeyBlock";

    public DoorController leftDoor;
    public DoorController01 rightDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            Destroy(other.gameObject);

            leftDoor.OpenDoor();
            rightDoor.OpenDoor();
        }
    }
}