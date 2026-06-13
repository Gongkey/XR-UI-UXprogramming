using UnityEngine;
using Oculus.Interaction;
using System;
using System.Collections;

public class Grabbing : MonoBehaviour
{
    private Grabbable _grabbable;
    public bool onClick = false;
    public event Action<Grabbing> OnButtonGrabbed;
    public AudioSource audioSource; // 

    [Header("버튼 이동 설정")]
    [SerializeField] private Vector3 pushDirection = new Vector3(-0.05f, 0, 0);
    [SerializeField] private float pushSpeed = 10f;
    [SerializeField] private AudioClip buttonSound;

    private ButtonManager buttonManager; // 부모 매니저 참조용
    private MeshRenderer meshRenderer;
    private Color originalColor;
    private Vector3 originLocalPosition;
    private Vector3 pushedLocalPosition;
    private Coroutine moveCoroutine;
    private readonly int baseColorPropId = Shader.PropertyToID("_BaseColor");

    private void Awake()
    {
        _grabbable = GetComponent<Grabbable>();
        meshRenderer = GetComponent<MeshRenderer>();

        // 최상위 혹은 부모 오브젝트에 있는 ButtonManager 컴포넌트를 찾아옵니다.
        buttonManager = GetComponentInParent<ButtonManager>();
        audioSource = GetComponent<AudioSource>();

        originLocalPosition = transform.localPosition;
        pushedLocalPosition = originLocalPosition + pushDirection;

        if (meshRenderer != null && meshRenderer.sharedMaterial != null)
        {
            originalColor = meshRenderer.sharedMaterial.GetColor(baseColorPropId);
        }
    }

    private void OnEnable()
    {
        if (_grabbable != null) _grabbable.WhenPointerEventRaised += OnPointerEventRaised;
    }

    private void OnDisable()
    {
        if (_grabbable != null) _grabbable.WhenPointerEventRaised -= OnPointerEventRaised;
    }

    private void OnPointerEventRaised(PointerEvent pointerEvent)
    {
        if (onClick && gameObject.name != "End") return;

        if (pointerEvent.Type == PointerEventType.Select)
        {
            // ★ [핵심 제약] 숫자 버튼인데 이미 부모 매니저에 4개가 꽉 찼다면 터치 이벤트를 완전히 무시합니다.
            if (gameObject.name != "End" && buttonManager != null && !buttonManager.CanInputMore())
            {
                
                Debug.LogWarning($"[입력 제한] 이미 4개 버튼을 모두 입력하여 {gameObject.name} 버튼은 눌리지 않습니다.");
                return;
            }
            OnGrabbed();
        }
    }

    private void OnGrabbed() //End(*) 버튼인지 판단
    {
        // 부모에게 신호 전달 (여기서 inputOrder 리스트에 추가됨)
        
        OnButtonGrabbed?.Invoke(this);
        audioSource.PlayOneShot(buttonSound);

        Debug.Log("Clicked!");

        if (gameObject.name == "End")
        {
            StartMoveAndReturn();
        }
        else
        {
            onClick = true;
            StartMove(pushedLocalPosition);
        }
    }


    private void SetButtonColor(Color color)
    {
        if (meshRenderer != null && meshRenderer.material != null)
        {
            if (meshRenderer.material.HasProperty(baseColorPropId))
            {
                meshRenderer.material.SetColor(baseColorPropId, color);
            }
            else
            {
                meshRenderer.material.color = color;
            }
        }
    }

    private void StartMove(Vector3 targetPos)
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(CoMovePosition(targetPos));
    }

    private IEnumerator CoMovePosition(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.localPosition, targetPos) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * pushSpeed);
            yield return null;
        }
        transform.localPosition = targetPos;
    }

    private void StartMoveAndReturn()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(CoPushAndReturn());
    }

    private IEnumerator CoPushAndReturn()
    {

        while (Vector3.Distance(transform.localPosition, pushedLocalPosition) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, pushedLocalPosition, Time.deltaTime * pushSpeed);
            yield return null;
        }
        transform.localPosition = pushedLocalPosition;

        while (Vector3.Distance(transform.localPosition, originLocalPosition) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originLocalPosition, Time.deltaTime * pushSpeed);
            yield return null;
        }
        transform.localPosition = originLocalPosition;

        SetButtonColor(originalColor);
    }

    public void ResetButton()
    {
        onClick = false;
        SetButtonColor(originalColor);
        StartMove(originLocalPosition);
    }
}