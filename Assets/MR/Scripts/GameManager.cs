using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject gameOverMenu;
    public static GridManager grid;
    public static Block Player;
    public static Block Stairs;

    private void Awake()
    {
        grid = GetComponent<GridManager>();
        Player = GameObject.FindWithTag("Player").GetComponent<Block>();
        Stairs = GameObject.FindAnyObjectByType<Stairs>();
        gameOverMenu = GameObject.FindGameObjectWithTag("GameOver");
        gameOverMenu.SetActive(false);
    }

    public static void GameOver()
    {
        gameOverMenu.SetActive(true);
    }
    // assume x and y will always be in bounds when calling 
    public static Cell GridCell(int x, int y) { return grid.gridList[x][y].GetComponent<Cell>(); }
    public static bool InBounds(int x, int y) { return x >= 0 && y >= 0 && x < grid.gridList.Count && y < grid.gridList[0].Count; }
}
