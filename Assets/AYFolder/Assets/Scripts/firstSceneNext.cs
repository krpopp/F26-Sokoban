using UnityEngine;
using UnityEngine.SceneManagement;

public class firstSceneNext : MonoBehaviour
{
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
