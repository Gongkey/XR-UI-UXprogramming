using UnityEngine;

public class ValveArea : MonoBehaviour
{
    [SerializeField] private GameObject valveObj;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider valveGrabCol)
    {
        if (valveGrabCol.CompareTag("ValveGrab"))
        {
            valveObj.SetActive(true);
            Destroy(valveGrabCol.gameObject);
        }
    }
}
