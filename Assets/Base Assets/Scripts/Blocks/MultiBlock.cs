using System.Collections.Generic;
using UnityEngine;


public class MultiBlock : Block
{
    public int numBlocks;

    //public GameObject 
    public MultiBlock[] followingBlocks;

    //public List<Vector2Int> myCells = new List<Vector2Int>();
    //public List<MultiBlock> blockGroup = new List<MultiBlock>();
    public Block myObj;

    //public Block blockPushed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override bool CheckMove(Vector2 _delta)
    {
        // The block receiving this method becomes the leader.
        // blockPushed = this;

        // functionality from parent function
        if (State != MoveStates.idle || !canMove)
        {
            return false;
        }

        Vector2Int delta = new Vector2Int((int)_delta.x, (int)_delta.y);
        
        //checking that every block in multiblock is not null, is idle, and canMove,and will be in grid if it moves
        for (int i = 0; i < followingBlocks.Length; i++) //making sure every block in mulitblock will be in grid
        {
            if (followingBlocks[i] == null || followingBlocks[i].State != MoveStates.idle) // making sure the block is not null and can move and is in idle state otherwise return false
            {
                return false;
            }

            Vector2Int target = followingBlocks[i].gridPos + delta; //calculating delta of each block
            
            if (!InGrid(target)) //checking if it will be in grid
            {
                return false;
            }
            // if not return false and if so continue
        }//end for 1

        //checks if the destination cell has a block in it and if it has a block in it that is not part of the multiblock then try to move it and if it cant move or will be outside of the grid then return false but if not move and move it and return true
        for (int i = 0; i < followingBlocks.Length; i++) //start for 2
        {
            Vector2Int target = followingBlocks[i].gridPos + delta;

            Cell checkCell = gridManager.gridList[target.x][target.y].GetComponent<Cell>();
            
            bool partOfMultiBlock = false; // stores whether cell ur about to move into has a block thats part of multiblock
            
            //go through each cell and see if there is a block in the cell it is trying to move to that is not a part of the multiblock
            for (int j = 0; j < followingBlocks.Length; j++)
            {
                if (checkCell.ContainObj == followingBlocks[j].gameObject)
                {
                    partOfMultiBlock = true;
                    break;
                }
            } //if the cell is not empty and if the block is not part of the multiblock the call checkHit

            if (checkCell.CheckContainObj() && !partOfMultiBlock)
            {
                Block otherBlock = checkCell.ContainObj.GetComponent<Block>();

                if (otherBlock == null)
                {
                    return false;
                }

                if (otherBlock.canMove)
                {
                    if (!CheckHit(otherBlock, _delta))
                        return false;
                }
            }
        }// end for 2
        
        //if nothing has reurned false at this point and all the checks have been passed through, then you can move every block
        for (int i = 0; i < followingBlocks.Length; i++) //start for 2
        {
            Vector2Int target = followingBlocks[i].gridPos + delta;

            Cell targetCell = gridManager.gridList[target.x][target.y].GetComponent<Cell>();

            followingBlocks[i].StartMove(targetCell, _delta);
            
        }
        return true;
    }//end override checkMove

    public bool CheckHit(Block _hitObj, Vector2 _delta)
    {
        if (GetComponent<Slidey>() == null && _hitObj.CheckMove(_delta)) return true;
        return false;
    }
        
    public bool InGrid(Vector2 _newPos)
    {
            if (_newPos.x >= 0 && _newPos.x < gridManager.gridList.Count &&
                _newPos.y >= 0 && _newPos.y < gridManager.gridList[0].Count) return true;
            return false;
    }

    public void startMove(Cell targetCell, Vector2 delta)
    {   
        StartMove(targetCell, delta);
    }
}
