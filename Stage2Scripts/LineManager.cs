using NUnit.Framework.Internal.Commands;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private PadLockController targetPadlock;
    [SerializeField] private MonoBehaviour finalHandGrabInteractable1;
    [SerializeField] private MonoBehaviour finalGrabInteractable1;
    [SerializeField] private MonoBehaviour finalHandGrabInteractable2;
    [SerializeField] private MonoBehaviour finalGrabInteractable2;
    [SerializeField] private MonoBehaviour valveHandGrabInteractable;
    [SerializeField] private MonoBehaviour valveGrabInteractable;
    

    public static LineManager Instance { get; private set; }
    private bool isBlueCorrect = false;
    private bool isRedCorrect = false;
    private bool isGreenCorrect = false;

    private void Awake()
    {
        finalHandGrabInteractable1.enabled = false;
        finalGrabInteractable1.enabled = false;
        finalHandGrabInteractable2.enabled = false;
        finalGrabInteractable2.enabled = false;
        valveHandGrabInteractable.enabled = false;
        valveGrabInteractable.enabled = false;
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateCableState(string cableTag, bool isConnected)
    {
        switch (cableTag)
        {
            case "RedLine":
                isRedCorrect = isConnected;
                break;
            case "BlueLine":
                isBlueCorrect = isConnected;
                break;
            case "GreenLine":
                isGreenCorrect = isConnected;
                break;
        }   

        Debug.Log($"상태 변경 -> 빨강:{isRedCorrect} | 파랑:{isBlueCorrect} | 초록:{isGreenCorrect}");

        CheckClearCondition();
    }

    private void CheckClearCondition()
    {
        if (isBlueCorrect && isRedCorrect && isGreenCorrect)
        {
            GameClear();
        }
    }

    private void GameClear()
    {

        if (targetPadlock != null)
        {
            finalHandGrabInteractable1.enabled = true;
            finalGrabInteractable1.enabled = true;
            finalHandGrabInteractable2.enabled = true;
            finalGrabInteractable2.enabled = true;
            valveHandGrabInteractable.enabled = true;
            valveGrabInteractable.enabled = true;
            targetPadlock.UnlockAndDrop();
        }
        Debug.Log("★★★ [게임 클리어] 모든 색상의 케이블이 올바르게 연결되었습니다! ★★★");

    }
}