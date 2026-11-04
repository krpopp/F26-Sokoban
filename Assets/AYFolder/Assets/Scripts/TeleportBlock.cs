using UnityEngine;

public class TeleportBlock : Block
{
    [SerializeField] private Vector2Int targetCell;
    [SerializeField] private Vector2Int teleportCell;
    
    [SerializeField] private ParticleSystem startEffect;
    [SerializeField] private ParticleSystem endEffect;
    
    [SerializeField] private float yOffset;
    
    private bool canTeleport = true;

    protected override void FinishMove()
    {
        base.FinishMove();

        Vector2Int curSpace;

        if (gridPos == targetCell)
        {
            curSpace = teleportCell;
        }
        else if (gridPos == teleportCell)
        {
            curSpace = targetCell;
        }
        else
        {
            canTeleport = true;
            return;
        }

        if (canTeleport)
        {
            Teleport(curSpace);
        }
    }

    private void Teleport(Vector2Int destination)
    {
        Cell newCell = gridManager.gridList[destination.x][destination.y].GetComponent<Cell>();

        if (newCell.CheckContainObj())
        {
            return;
        }
        
        canTeleport = false;
        
        playEffect(startEffect, transform.position);

        transform.parent.GetComponent<Cell>().RemoveContainObj();
        
        SetNewGridPos(newCell.gameObject, destination);
        
        transform.position = newCell.transform.position;
        targetPos = transform.position;
        gridManager.UpdateGrid();
        
        playEffect(endEffect, transform.position);
    }

    private void playEffect(ParticleSystem effect, Vector3 pos)
    {
        if (effect == null)
        {
            return;
        }
        
        effect.transform.position = pos+Vector3.up*yOffset;
        effect.Play();
    }
}