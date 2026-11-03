using System;
using UnityEngine;

public class King : Player
{
    public override bool CheckMove(Vector2 _delta)
    {
        if(gridManager.GetType() == typeof(ChessGridManager)) Debug.Log("its a chesser!");
        else
        {
            Debug.Log("its not a chesser:( hmm");
        }
        if((gridManager as ChessGridManager).IsSquareAttacked((gridManager as  ChessGridManager).WorldToBoardCoords(transform.position)+ _delta,GetComponent<Piece>().color)) return false;
        return base.CheckMove(_delta);
    }
    
}
