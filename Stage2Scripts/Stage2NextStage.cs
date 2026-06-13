using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stage2NextStage : MonoBehaviour
{
    [Header("이동할 씬 이름 (인스펙터에서 정확히 입력)")]
    [SerializeField] private string targetSceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            Debug.Log($"[씬 전환] 플레이어 감지! {targetSceneName} 씬으로 이동합니다.");

            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("이동할 씬 이름(Target Scene Name)이 비어있습니다! 인스펙터를 확인해주세요.");
        }
    }
}