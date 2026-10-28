using UnityEngine;

public class KPShiny : Slidey
{
    [SerializeField] private Vector2Int targetCell;

    protected override void FinishMove()
    {
        base.FinishMove();
        if (gridPos.x == targetCell.x && gridPos.y == targetCell.y)
        {
            Debug.Log("ur da winner");
        }
    }
}
