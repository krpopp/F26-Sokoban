using UnityEngine;

public class Slidey : Block
{
    protected override void FinishMove()
    {
        base.FinishMove();
        Slide(moveChange);
    }

    protected virtual void Slide(Vector2 _delta)
    {
        if (!CheckMove(_delta))
            Debug.Log("explode");
    }

}
