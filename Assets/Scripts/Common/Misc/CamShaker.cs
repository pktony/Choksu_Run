using UnityEngine;

/// <summary>
/// Damping 효과 추가 구현 ?
/// </summary>
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

    public static void ShakeCamera(float _shakePower, float _shakeTime)
    {
        if (!isShake)
        {
            isShake = true;
            shakeTime = _shakeTime;
            shakePower = _shakePower;
            initialPosition = mainCam.transform.position;
        }
    }

    //public static void DampCamera(float )

    private void Update()
    {
#if UNITY_EDITOR
        if (isShakeFor1sec && !isShake)
            ShakeCamera(1f, DEFAULT_SHAKE_POWER);
#endif
        if (!isShake) return;

        timer += Time.deltaTime;
        if (timer < shakeTime)
        {
            mainCam.transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakePower;
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