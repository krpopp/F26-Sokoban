using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int[] scenes = { 0, 1, 2 };
    

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
        int i = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        
        if (i + 1 < scenes.Length)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(i + 1);
        }
    }

    public void reloadScene()
    {
        int i = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(i);
    }
}
