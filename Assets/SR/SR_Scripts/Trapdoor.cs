using UnityEngine;

public class Trapdoor : Block
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public InvisibleToggleBlock[] rugBlocks;

    public Block ladder;
    
    protected override void Start()
    {
        base.Start();

        gridManager = FindFirstObjectByType<GridManager>();

        Debug.Log("Trapdoor GridManager: " + gridManager);
        Debug.Log("Grid List: " + gridManager.gridList);

        gridPos = findGridPos(this);
    }
    
    public bool checkInteractWithLadder()
    {
        if (ladder.gridPos == gridPos)
        {
            return true;
        }

        return false;
    }

    public bool IsCoveredByRug()
    {
        for (int i = 0; i < rugBlocks.Length; i++)
        {
            Vector2Int rugPos = rugBlocks[i].gridPos;
            if (gridPos == rugPos)
            {
                return true;
            }
        }

        return false;
    }
    
    public Vector2Int findGridPos(Block block)
    {
        Vector2Int closestPos = Vector2Int.zero;
        float closestDistance = Mathf.Infinity;

        for (int x = 0; x < gridManager.gridList.Count; x++)
        {
            for (int y = 0; y < gridManager.gridList[x].Count; y++)
            {
                GameObject cell = gridManager.gridList[x][y];

                float distance = Vector2.Distance(
                    new Vector2(block.transform.position.x,
                        block.transform.position.z),
                    new Vector2(cell.transform.position.x,
                        cell.transform.position.z)
                );

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPos = new Vector2Int(x, y);
                }
            }
        }
    
        return closestPos;
    }
    
}
