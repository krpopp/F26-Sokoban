using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Update()
    {
        if (InputSystem.actions.FindAction("Reset").WasPressedThisFrame())
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
