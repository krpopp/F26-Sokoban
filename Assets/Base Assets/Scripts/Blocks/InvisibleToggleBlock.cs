using UnityEngine;

public class InvisibleToggleBlock : MultiBlock
{
    
    public override bool CheckMove(Vector2 _delta)
    {
        // The block receiving this method becomes the leader.
        // blockPushed = this;
        if (isInObj())
        {
            //if so this object cant move and can be passed through
            setAllCanMove(false);
            return true;
        }
        else 
        {
            //else this object can move
            setAllCanMove(true);
        }
        
        // functionality from parent function
        if (State != MoveStates.idle || !canMove)
        {
            return false;
        }
        
        Vector2Int delta = new Vector2Int((int)_delta.x, (int)_delta.y);
        
        //checking that every block in multiblock is not null, is idle, and canMove,and will be in grid if it moves
        for (int i = 0; i < followingBlocks.Length; i++) //making sure every block in mulitblock will be in grid
        {
            if (followingBlocks[i] == null || followingBlocks[i].State != MoveStates.idle || !followingBlocks[i].canMove) // making sure the block is not null and can move and is in idle state otherwise return false
            {
                return false;
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
                if (checkCell.ContainObj == followingBlocks[j].gameObject)
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
}
