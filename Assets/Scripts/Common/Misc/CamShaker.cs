using UnityEngine;

public class CamShaker : MonoBehaviour
{
    static Camera mainCam;

    private static Vector3 initialPosition;
    private static bool isShake = false;

    private float timer = 0f;
    private static float shakeTime;
    private static float shakePower;

    private const float DEFAULT_SHAKE_POWER = 1.0f;

#if UNITY_EDITOR
    [SerializeField]
    private bool isShakeFor1sec = false;
#endif

    private void Awake()
    {
        mainCam = GetComponent<Camera>();
    }

    public static void ShakeCamera(float _shakeTime, float _shakePower)
    {
        if (!isShake)
        {
            isShake = true;
            shakeTime = _shakeTime;
            shakePower = _shakePower;
            initialPosition = mainCam.transform.position;
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (isShakeFor1sec && !isShake)
            ShakeCamera(1f, DEFAULT_SHAKE_POWER);
#endif
        if (isShake)
        {
            timer += Time.deltaTime;
            if (timer < shakeTime)
            {
                mainCam.transform.position = (Vector3)Random.insideUnitCircle * shakePower +
                    initialPosition.z * Vector3.forward;
            }
            else
            {
                mainCam.transform.position = initialPosition;
                timer = 0f;
                isShake = false;
#if UNITY_EDITOR
                isShakeFor1sec = false;
#endif
            }
        }
    }

}