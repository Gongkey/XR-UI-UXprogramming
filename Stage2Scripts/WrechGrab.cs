using UnityEngine;

public class WrechGrab : MonoBehaviour
{
    [SerializeField] private GameObject redLine;
    [SerializeField] private GameObject blueLine;
    [SerializeField] private GameObject greenLine;
    [SerializeField] private GameObject pipeHoles;
    [SerializeField] private GameObject chap2Light;
    [SerializeField] private MonoBehaviour handGrabInteractable;
    [SerializeField] private MonoBehaviour grabInteractable;
    [SerializeField] private AudioClip lightOnSound;

    private AudioSource lightOnAudioSource;
    private bool actOne = true;

    void Start()
    {
        lightOnAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(transform.eulerAngles.z < 200 && actOne == true)
        {
            handGrabInteractable.enabled = false;
            grabInteractable.enabled = false;
            pipeHoles.SetActive(true);
            chap2Light.SetActive(true);
            redLine.SetActive(true);
            blueLine.SetActive(true);
            greenLine.SetActive(true);
            lightOnAudioSource.PlayOneShot(lightOnSound);
            actOne = false;
        } 
    }
}
