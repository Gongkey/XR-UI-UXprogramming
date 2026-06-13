using UnityEngine;

public class PadLockController : MonoBehaviour
{

    private Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    public void UnlockAndDrop()
    {
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            
            rb.AddForce(Vector3.down * 2f, ForceMode.Impulse);
            
            Debug.Log("자물쇠 리지드바디 활성화 - 바닥으로 추락");
        }
    }
}