using System;
using UnityEngine;

public class Ladder : Block
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Trapdoor trapdoor;

    public SceneManager sceneManager;
    protected override void Start()
    {
        base.Start();

        gridPos = findGridPos(this);
    }
    void Update()
    {
        if (!trapdoor.IsCoveredByRug())
        {
            Debug.Log("Is not covered by rug");
            if (trapdoor.checkInteractWithLadder())
            {
                Debug.Log("ladder is on trapdoor");
                sceneManager.goToNextScene();
            }
        }
        else
        {
            Debug.Log("Is covered by rug");
        }
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
