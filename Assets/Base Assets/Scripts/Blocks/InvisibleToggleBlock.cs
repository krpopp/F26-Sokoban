using UnityEngine;

public class InvisibleToggleBlock : MultiBlock
{
    public Block[] allBlocks;

    public Block player;
    protected override void Start()
    {
        base.Start();
        
        if (gridManager == null)
        {
            gridManager = FindFirstObjectByType<GridManager>();
        }

        fixGridPositions();
        updateContainObj();
        
        Cell ladderCell = gridManager.gridList[4][4].GetComponent<Cell>();

        Debug.Log(
            "Cell (4,4) contains: " +
            (ladderCell.ContainObj != null
                ? ladderCell.ContainObj.name
                : "EMPTY")
        );
    }
    public override bool CheckMove(Vector2 _delta)
    {
        //Debug.Log("Player is in rug: " + playerIsInRug());
        if (playerIsInRug())
        {
            return true;
        }
        
        updateContainObj();
        
        setAllCanMove(true);
        Debug.Log("Rug occupied: " + isInObj());

        if (isInObj())
        {
            //if so this object cant move and can be passed through
            setAllCanMove(false);
            return true;
        }
        
        // functionality from parent function
        if (State != MoveStates.idle || !canMove)
        {
            setAllCanMove(false);
            return true;
        }
        
        Vector2Int delta = new Vector2Int((int)_delta.x, (int)_delta.y);
        
        //checking that every block in multiblock is not null, is idle, and canMove,and will be in grid if it moves
        for (int i = 0; i < followingBlocks.Length; i++) //making sure every block in mulitblock will be in grid
        {
            if (followingBlocks[i] == null)
            {
                Debug.LogError("Missing block in followingBlocks!");
                return false;
            }

            if (followingBlocks[i].State != MoveStates.idle || !followingBlocks[i].canMove)
            {
                setAllCanMove(false);
                return true;
            }

            Vector2Int target = followingBlocks[i].gridPos + delta; //calculating delta of each block
            
            if (!InGrid(target)) //checking if it will be in grid
            {
                setAllCanMove(false);
                return true;
            }
            // if not return false and if so continue
        }//end for 1

        //checks if the destination cell has a block in it and if it has a block in it that is not part of the multiblock then try to move it and if it cant move or will be outside of the grid then return false but if not move and move it and return true
        for (int i = 0; i < followingBlocks.Length; i++) //start for 2
        {
            Vector2Int target = followingBlocks[i].gridPos + delta;

            Cell checkCell = gridManager.gridList[target.x][target.y].GetComponent<Cell>();
            
            bool partOfMultiBlock = false;// stores whether cell ur about to move into has a block thats part of multiblock

            for (int j = 0; j < followingBlocks.Length; j++)
            {
                //if (checkCell.ContainObj == followingBlocks[j].gameObject)
                if (target == followingBlocks[j].gridPos)
                {
                    partOfMultiBlock = true;
                    break;
                }
            }

            if (checkCell.CheckContainObj() && !partOfMultiBlock)
            {
                setAllCanMove(false);
                return true;
            }
        }// end for 2
        
        //if nothing has reurned false at this point and all the checks have been passed through, then you can move every block
        for (int i = 0; i < followingBlocks.Length; i++) //start for 2
        {
            Vector2Int target = followingBlocks[i].gridPos + delta;

            Cell targetCell = gridManager.gridList[target.x][target.y].GetComponent<Cell>();

            followingBlocks[i].startMove(targetCell, _delta);
            
        }
        return true;
    }//end override checkMove

    
    public bool isInObj()
    {
        for (int i = 0; i < followingBlocks.Length; i++)
        {
            Cell checkCell = gridManager.gridList[followingBlocks[i].gridPos.x][followingBlocks[i].gridPos.y].GetComponent<Cell>();

            if (checkCell.CheckContainObj() &&
                checkCell.ContainObj != followingBlocks[i].gameObject)
            {
                return true;
            }
        }

        return false;
    }

    public void setAllCanMove(bool canMove)
    {
        for (int i = 0; i < followingBlocks.Length; i++)
        {
            followingBlocks[i].canMove = canMove;
        }
    }
    
        public void updateContainObj()
    {
        foreach (MultiBlock rug in followingBlocks)
        {
            if (rug == null) continue;

            Cell checkCell = gridManager.gridList[rug.gridPos.x]
                [rug.gridPos.y].GetComponent<Cell>();

            Block overlappingBlock = null;

            foreach (Block block in allBlocks)
            {
                if (block == null) continue;

                if (block.gridPos == rug.gridPos)
                {
                    overlappingBlock = block;
                    break;
                }
            }

            if (overlappingBlock != null)
            {
                checkCell.ContainObj = overlappingBlock.gameObject;
            }
            else
            {
                checkCell.ContainObj = rug.gameObject;
            }
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
    
    public void fixGridPositions()
    {
        foreach (MultiBlock rug in followingBlocks)
        {
            if (rug == null) continue;

            Vector2Int pos = findGridPos(rug);

            rug.gridPos = pos;
            rug.transform.SetParent(
                gridManager.gridList[pos.x][pos.y].transform
            );
        }

        foreach (Block block in allBlocks)
        {
            if (block == null) continue;

            Vector2Int pos = findGridPos(block);

            GameObject cell = gridManager.gridList[pos.x][pos.y];

            block.SetupNewGridBlock(
                gridManager,
                cell,
                pos
            );
        }
    }

    public bool playerIsInRug()
    {
        
        for (int i = 0; i < followingBlocks.Length; i++)
        {
            if (player.gridPos == followingBlocks[i].gridPos)
            {
                return true;
            }
        }

        return false;
    }

}
