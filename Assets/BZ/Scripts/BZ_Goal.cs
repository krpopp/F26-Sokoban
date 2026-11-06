using UnityEngine;
using UnityEngine.SceneManagement;

public class BZ_Goal : Slidey
{
    [SerializeField] private Vector2Int targetCell;
    [SerializeField] public float points;

    public BZ_RoomManager roomManager ;
    protected override void FinishMove()
    {
        base.FinishMove();
        if (gridPos.x == targetCell.x && gridPos.y == targetCell.y)
        {
            points += 1;
            Destroy(gameObject);

            if (points >= 2 && roomManager.GetActiveScene() == "BZ_Main1")
            {
                roomManager.ChangeLevel("BZ_Main2");
            }
            else if (points >= 2 && roomManager.GetActiveScene() == "BZ_Main2")
            {
                roomManager.ChangeLevel("BZ_Main3");
            }
            //Debug.Log("ur da winner");
        }
    }
}
