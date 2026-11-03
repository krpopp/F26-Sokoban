using UnityEngine;

public class Explosion : MonoBehaviour // this is just vfx
{
    public Light light;
    private void Start()
    {
        CamShake.amplitude += 2.5f;
    }
    private void Update()
    {
        light.intensity = Mathf.Lerp(light.intensity, 0f, 10f * Time.deltaTime);
    }
}
