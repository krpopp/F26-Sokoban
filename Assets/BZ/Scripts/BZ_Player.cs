using UnityEngine;

public class BZ_Player : Player
{
    [SerializeField] AudioSource lose;
    
    void Start()
    {
        base.Start();
        lose = GetComponent<AudioSource>();
    }
    protected override bool CheckHit(Block _hitObj, Vector2 _delta)
    {
        base.CheckHit(_hitObj, _delta);
        if (_hitObj.gameObject.GetComponent<BZ_Lion>() != null)
        {
            Debug.Log("???????");
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

