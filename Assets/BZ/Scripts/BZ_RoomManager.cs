using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BZ_RoomManager : MonoBehaviour
{
    public float points = 0;
    [SerializeField] AudioSource win;

    float waitTimer;
    string nextScene;
    bool changingScene = false;
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        win = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (changingScene)
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    public void ChangeLevel(string sceneName)
    {
        //AudioSource.PlayClipAtPoint(win.clip, transform.position, win.volume);
        if (changingScene) return;

        changingScene = true;
        nextScene = sceneName;

        win.Play();
        waitTimer = win.clip.length;
    }
  
    public string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void Restart()
    {
        SceneManager.LoadScene("BZ_Main1");
    }
    
}
