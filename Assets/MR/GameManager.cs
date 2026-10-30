using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GridManager grid;

    private void Awake()
    {
        grid = GetComponent<GridManager>();
    }

    // assume x and y will always be in bounds when calling 
    public static Cell GridCell(int x, int y) { return grid.gridList[x][y].GetComponent<Cell>(); }
    public static bool InBounds(int x, int y) { return x >= 0 && y >= 0 && x < grid.gridList.Count && y < grid.gridList[0].Count; }
}
