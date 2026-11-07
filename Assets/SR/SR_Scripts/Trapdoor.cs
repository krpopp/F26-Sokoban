using UnityEngine;

public class Trapdoor : Block
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public InvisibleToggleBlock[] rugBlocks;

    public Block ladder;
    

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
    
    
}
