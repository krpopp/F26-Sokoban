using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting.FullSerializer;

public class WinManager : MonoBehaviour
{
    
    [SerializeField] private GridManager gridManager;
    [SerializeField] Vector2Int[] targetCells;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject nextLevelScreen;

    private bool solved;

    private void Start()
    {
        nextLevelButton.SetActive(false);
        nextLevelScreen.SetActive(false);
    }

    private void Update()
    {
        if (solved || targetCells.Length == 0)
        {
            return;
        }

        if (AllCellsCovered())
        {
            solved = true;
            Debug.Log("level cleared!!!");
            Invoke("ShowButton", 0.5f);
        }
    }

    private bool AllCellsCovered()
    {
        foreach (Vector2Int pos in targetCells)
        {
            Cell cell = gridManager.gridList[pos.x][pos.y].GetComponent<Cell>();

            if (!cell.CheckContainObj())
            {
                return false;
            }
            
            Block block = cell.ContainObj.GetComponent<Block>();

            if (block.State != Block.MoveStates.idle)
            {
                return false;
            }
        }

        return true;
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