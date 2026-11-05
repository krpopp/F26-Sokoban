using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform cave;    
    
    [SerializeField] private Caterpillar head;
    [SerializeField] private Player player;

    [SerializeField] private GameObject winScreen; 

    private GridManager gridManager;
    private Cell caveCell;

    private bool gridChanged;
    private bool won;

    private void Start()
    {
        gridManager = GetComponent<GridManager>();
        caveCell = FindCell(cave.position);

        if(winScreen != null) winScreen.SetActive(false);
    }
    private void Update()
    {
        if(Keyboard.current.rKey.wasPressedThisFrame)
        {
            Restart();
        }
    }

    private void GridChanged()
    {
        gridChanged = true;
    }

    private void LateUpdate()
    {
        if (won || !gridChanged) { return; }

        if (caveCell.ContainObj == head.gameObject && head.State == Block.MoveStates.idle)
        {
            gridChanged = false;
            Win();
        }
    }

    private void Win()
    {
        SoundManager.Play(SoundManager.Instance.win);
        won = true;

        if (player != null) { player.enabled = false; }
        if (winScreen != null) { winScreen.SetActive(true); }
    }

    public void Restart()
    {
        SoundManager.Play(SoundManager.Instance.reset);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;

        if(next < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(next);
        }
        else {
            
            SceneManager.LoadScene(0); 
        }
    }

    private Cell FindCell(Vector3 _worldPos)
    {
        foreach(var column in gridManager.gridList)
        {
            foreach(GameObject cellObj in column)
            {
                Vector3 cellPos = cellObj.transform.position;

                if(Mathf.Abs(cellPos.x - _worldPos.x) < 0.1f &&
                    Mathf.Abs(cellPos.z - _worldPos.z) < 0.1f)
                {
                    return cellObj.GetComponent<Cell>();
                }
            }
        }
        return null;
    }
}