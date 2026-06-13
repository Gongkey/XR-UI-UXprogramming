using UnityEngine;

public class Time_Script : MonoBehaviour
{
    [Header("해 뜨는 시간")]
    public float sunriseDuration = 30f;

    [Header("밤 설정")]
    public Vector3 nightRotation = new Vector3(-30f, 0f, 0f);
    public Color nightColor = new Color(0.1f, 0.15f, 0.35f);
    public float nightIntensity = 0.1f;

    [Header("아침 설정")]
    public Vector3 morningRotation = new Vector3(50f, 30f, 0f);
    public Color morningColor = new Color(1f, 0.8f, 0.45f);
    public float morningIntensity = 1.2f;

    private Light sunLight;
    private float timer = 0f;

    void Start()
    {
        sunLight = GetComponent<Light>();

        transform.rotation = Quaternion.Euler(nightRotation);
        sunLight.color = nightColor;
        sunLight.intensity = nightIntensity;
    }

    void Update()
    {
        if (timer < sunriseDuration)
        {
            timer += Time.deltaTime;
            float t = timer / sunriseDuration;

            transform.rotation = Quaternion.Lerp(
                Quaternion.Euler(nightRotation),
                Quaternion.Euler(morningRotation),
                t
            );

            sunLight.color = Color.Lerp(nightColor, morningColor, t);
            sunLight.intensity = Mathf.Lerp(nightIntensity, morningIntensity, t);
        }
    }
}
