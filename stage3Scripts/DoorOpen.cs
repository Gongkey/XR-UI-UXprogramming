using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public bool isUnlocked = false;

    public void UnlockDoor()
    {
        isUnlocked = true;
        Debug.Log("문 잠금 해제!");
    }
}
