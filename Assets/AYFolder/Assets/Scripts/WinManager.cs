using UnityEngine;
using UnityEngine.SceneManagement;


public class WinManager : MonoBehaviour
{
    
    [SerializeField] private GridManager gridManager;
    [SerializeField] Vector2Int[] targetCells;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject nextLevelScreen;

    [SerializeField] private AudioSource levelClearSound;
    [SerializeField] private AudioSource goalSound;

    private bool solved;
    private bool[] isCovered;

    private void Start()
    {
        nextLevelButton.SetActive(false);
        nextLevelScreen.SetActive(false);
        
        isCovered = new bool[targetCells.Length];
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
            PlaySound(levelClearSound);
            
        }
    }

    private bool AllCellsCovered()
    {
        bool allCovered = true;

        for (int i = 0; i < targetCells.Length; i++)
        {
            Vector2Int pos = targetCells[i];
            Cell cell = gridManager.gridList[pos.x][pos.y].GetComponent<Cell>();
            
            bool covered = false;

            if (cell.CheckContainObj())
            {
                Block block = cell.ContainObj.GetComponent<Block>();
                covered = block.State == Block.MoveStates.idle && block.GetComponent<Player>() == null;
            }

            if (covered && !isCovered[i])
            {
                PlaySound(goalSound);
            }

            isCovered[i] = covered;

            if (!covered)
            {
                allCovered = false;
            }
        }

        return allCovered;
    }

    private void ShowButton()
    {
        nextLevelButton.SetActive(true);
        nextLevelScreen.SetActive(true);
    }

    private void PlaySound(AudioSource audioSource)
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}