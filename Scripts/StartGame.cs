using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToStage1OnTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("TestScene");
        }
    }
}
