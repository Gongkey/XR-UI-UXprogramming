using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private GameObject cameraRig;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraRig.transform.position = destination.position;
        }
    }
}
