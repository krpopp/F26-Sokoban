using UnityEngine;

public class CamShake : MonoBehaviour
{
    public static float amplitude;
    public Transform cam;

    private void Update()
    {
        amplitude = Mathf.Lerp(amplitude, 0f, 10f * Time.deltaTime);
        cam.localPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized * amplitude;
    }
}
