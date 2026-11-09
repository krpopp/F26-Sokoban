using UnityEngine;

public class FinishPlate : MonoBehaviour
{
    public Vector2Int gridPos;

    public bool IsAtGoal(Slidey slidey)
    {
        return slidey != null && slidey.gridPos == gridPos;
    }
}