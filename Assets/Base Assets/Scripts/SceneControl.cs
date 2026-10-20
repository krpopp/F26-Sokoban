using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public void ButtonToScene(string toLoad)
    {
        SceneManager.LoadScene(toLoad);
    }
}
