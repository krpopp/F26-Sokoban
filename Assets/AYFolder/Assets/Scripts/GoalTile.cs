using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTile : MonoBehaviour
{
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject nextLevelScreen;

    private void Start()
    {
        nextLevelButton.SetActive(false);
        nextLevelScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("block"))
        {
            Debug.Log("clear");
            Invoke("ShowButton", 0.5f);
        }
    }

    private void ShowButton()
    {
        nextLevelButton.SetActive(true);
        nextLevelScreen.SetActive(true);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}