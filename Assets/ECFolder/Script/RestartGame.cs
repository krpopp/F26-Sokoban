using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
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
        if (resetButton.WasReleasedThisFrame())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
