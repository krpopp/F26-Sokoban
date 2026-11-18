using UnityEngine;

public class SoundManager : MonoBehaviour
{ 

    [SerializeField] private AudioSource soundEffects;
    [SerializeField] private AudioSource music;

    public static SoundManager Instance { get; private set; }

    public AudioClip caterpillarPush;

    public AudioClip cratePush;

    public AudioClip platePress;
    public AudioClip caterpillarWall;
    public AudioClip walk;

    public AudioClip win;
    public AudioClip reset;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void Play(AudioClip clip)
    {
        if (Instance != null && clip != null)
        {
            Instance.soundEffects.pitch = Random.Range(0.95f, 1.05f);
            Instance.soundEffects.PlayOneShot(clip);
        }
    }
}
