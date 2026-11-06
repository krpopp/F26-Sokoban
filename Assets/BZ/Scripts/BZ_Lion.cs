using TMPro;
using UnityEngine;

public class BZ_Lion : Block
{
    [SerializeField] AudioSource lose;

    void Start()
    {
        lose = GetComponent<AudioSource>();
    }
    protected override bool CheckHit(Block _hitObj, Vector2 _delta)
    {
        Debug.Log("Destroy");
        bool hasHit = base.CheckHit(_hitObj, _delta);
        lose.Play();
        Destroy(_hitObj.gameObject);
        return hasHit;
    }
}

