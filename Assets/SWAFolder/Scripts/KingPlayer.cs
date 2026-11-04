using System;
using UnityEngine;

public class King : Player
{
    
    public AudioSource audioSource;
    public AudioClip error;
    public AudioClip moveSound;
    
    public override bool CheckMove(Vector2 _delta)
    {
        if(gridManager.GetType() == typeof(ChessGridManager)) Debug.Log("its a chesser!");
        else
        {
            Debug.Log("its not a chesser:( hmm");
        }
        if((gridManager as ChessGridManager).IsSquareAttacked((gridManager as  ChessGridManager).WorldToBoardCoords(transform.position)+ _delta,GetComponent<Piece>().color))
        {
            audioSource.PlayOneShot(error);
            return false;
        }
        bool success = base.CheckMove(_delta);
        if (success) audioSource.PlayOneShot(moveSound);
        return success;
    }
    
}
