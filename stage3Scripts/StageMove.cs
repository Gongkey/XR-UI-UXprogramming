using UnityEngine;
using UnityEngine.SceneManagement;

public class StageMove : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("End Scene");
        }
    }
}
