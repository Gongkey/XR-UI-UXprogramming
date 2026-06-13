using UnityEngine;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour
{
    // 인스펙터에서 드래그 앤 드롭으로 연결할 Door 스크립트 
    [Header("연동할 오브젝트")]
    [SerializeField] private Door doorTarget;

    private List<Grabbing> childButtons = new List<Grabbing>();
    private List<string> inputOrder = new List<string>();

    private void Awake()
    {
        Grabbing[] grabbedComponents = GetComponentsInChildren<Grabbing>();

        foreach (Grabbing button in grabbedComponents)
        {
            childButtons.Add(button);
            button.OnButtonGrabbed += HandleButtonGrabbed;
        }

        // 컴포넌트 연결 누락 방지 경고
        if (doorTarget == null)
        {
            Debug.LogError("[ButtonManager] Door 오브젝트가 연결되지 않았습니다! 인스펙터에서 Door를 드래그해서 넣어주세요.");
        }
    }

    private void OnDestroy()
    {
        foreach (Grabbing button in childButtons)
        {
            if (button != null) button.OnButtonGrabbed -= HandleButtonGrabbed;
        }
    }

    public bool CanInputMore()
    {
        return inputOrder.Count < 4;
    }

    private void HandleButtonGrabbed(Grabbing grabbedButton) // AI가 구현한 것을 약간 수정 (클릭 감지 및 어떤 버튼이 눌렸는지 판단)
    {
        string buttonName = grabbedButton.gameObject.name;
        Debug.Log($"[ButtonManager] 클릭 감지된 버튼: {buttonName}");

        if (buttonName != "End")
        {
            inputOrder.Add(buttonName);
        }
        else if (buttonName == "End")
        {
            Debug.Log("완료(End) 버튼 눌림. 비밀번호를 검증합니다.");
            CheckPassword();
            ResetOnlyNumberButtons();
        }
    }

    private void CheckPassword()
    {
        if (inputOrder.Count == 4) // 직접 구현 (4->5->7->3 순서로 눌러야 한다)
        {
            if (inputOrder[0] == "Four" &&
                inputOrder[1] == "Five" &&
                inputOrder[2] == "Seven" &&
                inputOrder[3] == "Three")
            {
                Debug.Log("★ 정답이다! ★");

                // ★ [핵심 로직] 비밀번호가 맞았으므로 외부 문 오브젝트의 열기 함수를 작동시킵니다.
                if (doorTarget != null)
                {
                    doorTarget.OpenDoor();
                }
            }
            else
            {
                Debug.Log("틀렸습니다! 비밀번호 순서가 올바르지 않습니다.");
            }
        }
        else
        {
            Debug.Log($"틀렸습니다! 입력된 버튼 개수가 부족합니다. (현재 입력 개수: {inputOrder.Count}개 / 4개 필수)");
        }

        inputOrder.Clear();
    }        // 여기까지 직접 구현!!

    private void ResetOnlyNumberButtons() // 이 부분도 약간 다듬음
    {
        foreach (Grabbing button in childButtons)
        {
            if (button != null && button.gameObject.name != "End")
            {
                button.ResetButton();
            }
        }
    }
}