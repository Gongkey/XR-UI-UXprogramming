using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class stage1NextStage : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    
    [SerializeField] private Slider loadingSlider;

    private bool isLoadingStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isLoadingStarted) return;

        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            isLoadingStarted = true;
            // 코루틴(Coroutine)을 사용해 비동기 로딩을 시작합니다.
            StartCoroutine(LoadSceneAsyncCoroutine());
        }
    }

    private IEnumerator LoadSceneAsyncCoroutine()
    {
        Debug.Log($"[로딩 시작] {sceneToLoad} 씬을 백그라운드에서 불러옵니다.");

        // 1. 비동기 로딩 시작 (씬을 백그라운드 메모리에 올리기 시작함)
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

        // 씬 로딩이 100% 완료되어도 자동으로 화면을 전환하지 못하게 막아둡니다.
        operation.allowSceneActivation = false;

        // 2. 씬을 완전히 불러올 때까지 반복문 안에서 대기
        while (!operation.isDone)
        {
            // operation.progress는 0부터 0.9까지 채워집니다. (0.9가 로딩 완료 상태)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // 만약 로딩바 UI를 연결해 두었다면 실시간으로 게이지를 채워줍니다.
            if (loadingSlider != null)
            {
                loadingSlider.value = progress;
            }

            Debug.Log($"로딩 진행률: {progress * 100}%");

            // 로딩이 내부적으로 끝났다(0.9 이상)면 잠시 대기 후 화면을 전환합니다.
            if (operation.progress >= 0.9f)
            {
                // VR 환경이라면 플레이어가 마음의 준비를 할 수 있게 1~2초 정도 잠깐의 여유를 줍니다.
                yield return new WaitForSeconds(1f);

                // ★ 막아두었던 씬 활성화를 잠금 해제하여 다음 씬으로 부드럽게 이동시킵니다.
                operation.allowSceneActivation = true;
            }

            // 한 프레임 쉬고 다음 체크 진행 (렉 걸림 방지)
            yield return null;
        }
    }
}