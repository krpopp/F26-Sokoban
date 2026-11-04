using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CSPGameManager : MonoBehaviour
{

    private InputAction resetButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resetButton = InputSystem.actions.FindAction("Reset");
    }

    // Update is called once per frame
    void Update()
    {
        if(resetButton.WasReleasedThisFrame()) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        switch (SceneManager.GetActiveScene().name)
        {
            case("CSPScene"):
                SceneManager.LoadScene("CSPScene2");
                break;
            case("CSPScene2"):
                SceneManager.LoadScene("CSPScene3");
                break;
            case("CSPScene3"):
                SceneManager.LoadScene("CSPScene");
                break;
        }
    }
}
