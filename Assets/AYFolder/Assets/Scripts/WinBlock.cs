using UnityEngine;
using UnityEngine.SceneManagement;


public class WinBlock : Slidey
{
    [SerializeField] private Vector2Int targetCell;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject nextLevelScreen;

    protected override void Start()
    {
        nextLevelButton.SetActive(false);
        nextLevelScreen.SetActive(false);
    }
    protected override void FinishMove()
    {
        base.FinishMove();
        if (gridPos.x == targetCell.x && gridPos.y == targetCell.y)
        {
            Debug.Log("ur da winner");
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
