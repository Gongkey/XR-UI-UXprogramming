using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LineGrab : MonoBehaviour
{
    private bool timeStart;
    float reActivetime = 2f;
    [SerializeField] private MonoBehaviour handGrabInteractable;
    [SerializeField] private MonoBehaviour grabInteractable;
    [SerializeField] private AudioClip cableSound;

    private AudioSource cableAudioSource;
    private CableHole targetHole;

    void Start()
    {
        cableAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (timeStart == true)
        {
            reActivetime -= Time.deltaTime;
        }

        if (reActivetime <= 0)
        {
            handGrabInteractable.enabled = true;
            grabInteractable.enabled = true;
            timeStart = false;
            reActivetime = 2f;
        }
    }

    private void OnTriggerEnter(Collider lineHoleCol)
    {
        if (lineHoleCol.CompareTag("RedLine") || lineHoleCol.CompareTag("BlueLine") || lineHoleCol.CompareTag("GreenLine"))
        {
            CableHole hole = lineHoleCol.GetComponent<CableHole>();

            if (hole != null)
            {
                if (!hole.TryOccupy())
                {
                    Debug.LogWarning($"{lineHoleCol.name} 구멍은 이미 다른 케이블이 점유 중입니다!");
                    return;
                }

                targetHole = hole;
            }
            handGrabInteractable.enabled = false;
            grabInteractable.enabled = false;
            timeStart = true;
            transform.position = lineHoleCol.gameObject.transform.position;
            cableAudioSource.PlayOneShot(cableSound);

            if (lineHoleCol.CompareTag(gameObject.tag))
            {
                if (LineManager.Instance != null)
                {
                    LineManager.Instance.UpdateCableState(gameObject.tag, true);
                }
                Debug.Log("올바른 케이블이 연결되었습니다");
            }
        }
    }

    private void OnTriggerExit(Collider lineHoleCol)
    {
        if (lineHoleCol.CompareTag("RedLine") || lineHoleCol.CompareTag("BlueLine") || lineHoleCol.CompareTag("GreenLine"))
        {
            CableHole hole = lineHoleCol.GetComponent<CableHole>();
            if (hole != null && hole == targetHole)
            {
                hole.ReleaseHole();
                targetHole = null;
            }

            if (lineHoleCol.CompareTag(gameObject.tag))
            {

                Debug.Log($"[분리] {gameObject.tag} 케이블이 구멍에서 빠졌습니다.");

                if (LineManager.Instance != null)
                {
                    LineManager.Instance.UpdateCableState(gameObject.tag, false);
                }
            }
        }
    }
}