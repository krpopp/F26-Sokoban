using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BZ_RoomManager : MonoBehaviour
{
    public float points = 0;
    [SerializeField] AudioSource win;
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        win = GetComponent<AudioSource>();
    }
    public void ChangeLevel(string sceneName)
    {
        win.Play();
        SceneManager.LoadScene(sceneName);
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
