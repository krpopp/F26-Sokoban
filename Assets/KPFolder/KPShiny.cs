using UnityEngine;

public class KPShiny : Slidey
{
  [SerializeField] Vector2Int targetCell;

  protected override void FinishMove()
  {
    base.FinishMove();
    if (gridPos.x == targetCell.x && gridPos.y == targetCell.y)
    {
      
    }
  }
  
}
