using UnityEngine;

public class DoorController : MonoBehaviour
{
    public void OpenDoor()
    {
        Debug.Log("문 열림!");

        // 문을 회전해서 열기
        transform.Rotate(0, 90, 0);

        // 문을 아예 없애고 싶으면 위 줄 대신 이거 사용
        // Destroy(gameObject);
    }
}