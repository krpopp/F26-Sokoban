using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Light light;
    private void Start()
    {
        CamShake.amplitude += 5f;
    }
    private void Update()
    {
        light.intensity = Mathf.Lerp(light.intensity, 0f, 10f * Time.deltaTime);
    }
}
