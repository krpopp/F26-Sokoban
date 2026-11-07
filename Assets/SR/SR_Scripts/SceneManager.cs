using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    string currentScene = "Level_1";
    string nextScene = "Level_2";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            reloadScene();
        }
    }

    public void goToNextScene()
    {
        if (nextScene != null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }
    }

    public void reloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
    }
}
