using UnityEngine;

public class FinalDoorOpen : MonoBehaviour
{
    public string keyBlockTag = "Finish";

    public Transform leftDoor;
    public Transform rightDoor;

    private bool opened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (opened) return;

        if (other.CompareTag(keyBlockTag))
        {
            opened = true;

            leftDoor.Rotate(0, -90, 0, Space.Self);
            rightDoor.Rotate(0, 90, 0, Space.Self);

            other.gameObject.SetActive(false);

            Destroy(other.gameObject, 0.2f);
        }
    }
}
