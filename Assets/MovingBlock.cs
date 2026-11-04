using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MovingBlock : Block
{
    
    [Header("Lose Condition")]
    public Vector2Int loseGoalPos = new Vector2Int(0, 5);
    public float restartDelay = 3f;

    private bool lost = false;
    
    private Player player;
    private Vector2Int lastPlayerPos;
    private bool hasLastPos = false;
    private bool needCheck = false;

    protected override void FinishMove()
    {
        base.FinishMove();

        if (!lost && gridPos == loseGoalPos)
        {
            lost = true;
            StartCoroutine(RestartScene());
        }
    }

    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    protected override void Start()
    {
        base.Start();
        player = FindFirstObjectByType<Player>();
    }

    private void Update()
    {
        if (player == null) return;
        
        if (player.State != MoveStates.idle) return;

        //record the grid position in the first time
        if (!hasLastPos)
        {
            lastPlayerPos = player.gridPos;
            hasLastPos = true;
            return;
        }

        //check
        if (player.gridPos != lastPlayerPos)
        {
            lastPlayerPos = player.gridPos;
            needCheck = true;
        }
        
        if (needCheck && State == MoveStates.idle)
        {
            needCheck = false;
            TryFlee(player.gridPos);
        }
    }

    private void TryFlee(Vector2Int playerPos)
    {
        Vector2Int offset = gridPos - playerPos;

        //just beside for one block distance
        if (Mathf.Abs(offset.x) + Mathf.Abs(offset.y) != 1) return;

        Vector2Int target = gridPos + offset;
        if (!InGrid(target)) return;

        //only move to empty grid
        Cell cell = gridManager.gridList[target.x][target.y].GetComponent<Cell>();
        if (cell.CheckContainObj()) return;

        CheckMove(offset); 
    }
}