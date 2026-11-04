using UnityEngine;
using UnityEngine.SceneManagement;

public class CSPGameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CSPScene2");
    }
}
