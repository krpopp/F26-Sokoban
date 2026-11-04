using UnityEngine;

public class KPExplody : Block
{
    protected override bool CheckHit(Block _hitObj, Vector2 _delta)
    {
        bool hasHit = base.CheckHit( _hitObj,  _delta);
        Destroy(_hitObj.gameObject);
        return hasHit;
    }
}
