using UnityEngine;
using UnityEngine.SceneManagement;

public class BZ_Goal : Slidey
{
    [SerializeField] private Vector2Int targetCell;
    [SerializeField] AudioSource lose;

    [SerializeField] AudioClip winner;
    //[SerializeField] public float points;
    [SerializeField] AudioClip collect;
    public BZ_RoomManager roomManager;

    public bool hasWon = false;

    void Start()
    {
        base.Start();
    }
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
                //AudioSource.PlayClipAtPoint(winner, transform.position);
                //hasWon = true;
                roomManager.ChangeLevel("LevelSelect");
            }
            //Debug.Log("ur da winner");
        }
    }

    protected override bool CheckHit(Block _hitObj, Vector2 _delta)
    {
        if (_hitObj.gameObject.GetComponent<BZ_Lion>() != null)
        {
            PlayLoseSound();
            Destroy(gameObject);
            return false;
        }

        if (GetComponent<Slidey>() == null && _hitObj.CheckMove(_delta)) return true;
        return false;
    }
    
    void PlayLoseSound()
    {
        AudioSource.PlayClipAtPoint(lose.clip, transform.position, lose.volume);
    }
}
