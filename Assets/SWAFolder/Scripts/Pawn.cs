using UnityEngine;

public class Pawn : Block
{
    public GameObject queenMesh;
    public AudioClip queenSound;
    public AudioSource source;
    public override bool CheckMove(Vector2 _delta)
    {
        if (_delta != new Vector2(0, 1)) return false;
        return base.CheckMove(_delta);
    }

    protected override void FinishMove()
    {
        if(gridPos.y == 8) Promote();
        base.FinishMove();
    }
    
    public void Promote()
    {
        queenMesh.active = true;
        (gridManager as ChessGridManager).Win();
        source.PlayOneShot(queenSound);
    }
}
