using UnityEngine;

public class BZ_Lion : Block
{
    protected override bool CheckHit(Block _hitObj, Vector2 _delta)
    {
        Debug.Log("Destroy");
        bool hasHit = base.CheckHit(_hitObj, _delta);
        Destroy(_hitObj.gameObject);
        return hasHit;
    }
}

