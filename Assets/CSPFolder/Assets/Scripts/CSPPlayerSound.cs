using System;
using UnityEngine;

public class CSPPlayerSound : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource stepSound;

    void BlockMoved(Vector2 delta)
    {
        stepSound.Play();
    }
}
