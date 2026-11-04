using UnityEngine;

public class DoorBlock : Block
{
    public DoorController doorController;

    public override bool CheckMove(Vector2 direction)
    {
        if (doorController == null) return false;
        //closed door blocks player, open allows player through
        return doorController.IsOpen;
    }
    
}
