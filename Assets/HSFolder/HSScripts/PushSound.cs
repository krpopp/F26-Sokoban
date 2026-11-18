using UnityEngine;

public class PushSound : MonoBehaviour
{
    [SerializeField] private AudioClip sound;

    private void BlockMoved(Vector2 _delta)
    {
        SoundManager.Play(sound);
    }
}
