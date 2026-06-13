using UnityEngine;
using UnityEngine.Audio;

public class ValveGrab : MonoBehaviour
{
    [SerializeField] private GameObject globalVolume;
    [SerializeField] private GameObject allLights;
    [SerializeField] private GameObject doorObj;
    [SerializeField] private GameObject pipeHoles;
    [SerializeField] private float openHeight = 3.0f;
    [SerializeField] private float openSpeed = 2.0f;
    [SerializeField] private AudioClip engineSound;

    private AudioSource engineSoundAudioSource;
    private Vector3 closedPosition;
    private Vector3 openedPosition;
    private bool isOpening = false;
    private bool doorOpen = false;

    void Start()
    {
        engineSoundAudioSource = GetComponent<AudioSource>();

        if (doorObj != null)
        {    
            closedPosition = doorObj.transform.position;

            openedPosition = closedPosition + (Vector3.up * openHeight);
        }
        else
        {
            Debug.LogError("Door Object가 지정되지 않았습니다!");
        }
    }

    void Update()
    {
        if (transform.eulerAngles.y > 180 && doorOpen == false )
        {
            isOpening = true;
            allLights.SetActive(false);
            globalVolume.SetActive(true);
            pipeHoles.SetActive(false);
            engineSoundAudioSource.clip = engineSound;
            engineSoundAudioSource.loop = true;
            engineSoundAudioSource.Play();           
            doorOpen = true;
        }
        if (isOpening && doorObj != null)
        {
            doorObj.transform.position = Vector3.MoveTowards(
                doorObj.transform.position,
                openedPosition,
                openSpeed * Time.deltaTime
            );

            if (doorObj.transform.position == openedPosition)
            {
                isOpening = false;
                Debug.Log("문이 완전히 열렸습니다.");
            }
        }
    }
}
