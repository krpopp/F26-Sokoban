using UnityEngine;

public class CSPBlockSound : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource pushSound;

    void BlockMoved(Vector2 delta)
    {
        pushSound.Play();
    }
}
