using UnityEngine;

public class BlockMoveSound : MonoBehaviour
{
    [SerializeField] private AudioSource moveSound;

    private void BlockMoved(Vector2 delta)
    {
        if (moveSound != null && !moveSound.isPlaying)
        {
            moveSound.Play();
        }
    }
}