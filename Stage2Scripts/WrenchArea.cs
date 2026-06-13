using UnityEngine;

public class WrenchArea : MonoBehaviour
{
    [SerializeField] private GameObject wrenchInObj;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider wrenchGrabCol)
    {
        if(wrenchGrabCol.CompareTag("WrenchGrab"))
        {
            wrenchInObj.SetActive(true);
            Destroy(wrenchGrabCol.gameObject);
        }
    }
}
