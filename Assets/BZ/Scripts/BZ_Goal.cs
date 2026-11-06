using UnityEngine;
using UnityEngine.SceneManagement;

public class BZ_Goal : Slidey
{
    [SerializeField] private Vector2Int targetCell;
    //[SerializeField] public float points;
    [SerializeField] AudioClip collect;
    public BZ_RoomManager roomManager;

    protected override void FinishMove()
    {
        base.FinishMove();
        if (gridPos.x == targetCell.x && gridPos.y == targetCell.y)
        {
            AudioSource.PlayClipAtPoint(collect, transform.position);
            roomManager.points += 1;
            Debug.Log(roomManager.points);
            Destroy(gameObject);
            //Debug.Log(points);

            if (roomManager.points >= 3 && roomManager.GetActiveScene() == "BZ_Main1")
            {
                roomManager.ChangeLevel("BZ_Main2");
            }
            else if (roomManager.points >= 3 && roomManager.GetActiveScene() == "BZ_Main2")
            {
                roomManager.ChangeLevel("BZ_Main3");
            }

            if (roomManager.points >= 3 && roomManager.GetActiveScene() == "BZ_Main3")
            {
                //WIN!!
            }
            //Debug.Log("ur da winner");
        }
    }
}
