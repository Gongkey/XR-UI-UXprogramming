using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    [Header("문 이동 설정")]
    [SerializeField] private float openHeight = 3.0f; // 위로 얼마나 올라갈지 (미터 단위)
    [SerializeField] private float openSpeed = 2.0f;  // 문이 열리는 속도

    private Vector3 targetPosition;
    private bool isOpening = false;

    private void Awake()
    {
        // 직접 구현 ----> 문이 열렸을 때의 최종 목표 위치를 계산합니다 (현재 Y축 위치 + 열릴 높이)
        targetPosition = transform.position + new Vector3(0, openHeight, 0);
    }

    // ButtonManager에서 정답을 맞혔을 때 호출할 함수
    public void OpenDoor()
    {
        // 이미 열리고 있는 중이라면 중복 실행 방지
        if (isOpening) return;

        isOpening = true;
        Debug.Log($"[Door] 문이 위로 {openHeight}m 만큼 스르륵 열립니다.");

        // 부드러운 이동을 위해 코루틴 시작
        StartCoroutine(CoOpenDoor());
    }

    private IEnumerator CoOpenDoor()
    {
        // 목표 위치에 거의 도달할 때까지 매 프레임 부드럽게 이동(Lerp)
        while (Vector3.Distance(transform.position, targetPosition) > 0.001f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * openSpeed);
            yield return null; // 다음 프레임까지 대기
        }

        // 완전히 목표 위치로 고정
        transform.position = targetPosition;
        Debug.Log("[Door] 문이 완전히 열렸습니다.");
    }
}