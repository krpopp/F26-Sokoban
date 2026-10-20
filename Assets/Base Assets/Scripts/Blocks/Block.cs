using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Block : MonoBehaviour
{
    [Header("Movement Control")]
    #region Movement
    
    [SerializeField]
    protected AnimationCurve moveCurve;
    public float speed;
    [SerializeField]
    private float lerpSnapDist;
    private float lerpTime = 0;
    private Vector3 startPos = Vector3.zero;
    public Vector3 targetPos = Vector3.zero;
    // reference to the currently running move coroutine so it can be stopped safely
    private Coroutine moveCoroutine = null;
    #endregion

    [Header("Block Attributes")]
    #region Block Attributes
    public bool canMove;
    protected GridManager gridManager;
    #endregion

    [HideInInspector]
    public Vector2Int gridPos, moveChange;

    public enum MoveStates
    {
        idle,
        attemptingMove,
        moving
    }

    public MoveStates state;
    public MoveStates State { get => state; set => state = value; }

    

    protected virtual void Start()
    {
        startPos = transform.position;
        targetPos = transform.position;
    }

    #region Movement Methods
    /// <summary>
    /// Checks if it is possible to move in the grid
    /// </summary>
    public virtual bool CheckMove(Vector2 _delta)
    {
        if (State == MoveStates.idle)
        {
            State = MoveStates.attemptingMove;
            if (canMove && InGrid(gridPos + _delta))
            {
                Cell checkCell = gridManager.gridList[gridPos.x + (int)_delta.x][gridPos.y + (int)_delta.y].GetComponent<Cell>();
                if (!checkCell.CheckContainObj() ||
                CheckHit(checkCell.ContainObj.GetComponent<Block>(), _delta))
                {
                    StartMove(checkCell, _delta);
                    BroadcastMessage("BlockMoved", _delta, SendMessageOptions.DontRequireReceiver);
                    return true;
                }
            }
            State = MoveStates.idle;
        }
        return false;
    }

    /// <summary>
    /// Checks how close the target position is to a compared position
    /// </summary>
    public bool CheckLerpDist(Vector3 _comparePos, float _maxVal)
    {
        if (Vector3.Distance(_comparePos, targetPos) < _maxVal) return true;
        return false;
    }

    /// <summary>
    /// Lerps block from one position to another based on animation curve set in the inspector
    /// </summary>
    protected Vector3 Move()
    {
        lerpTime += Time.deltaTime * speed;
        float percent = Mathf.Clamp01(moveCurve.Evaluate(lerpTime));
        Vector3 newPos = Vector3.Lerp(startPos, targetPos, percent);
        return newPos;
    }

    /// <summary>
    /// Sets initial values needed for moving on the grid and changes to the moving state
    /// </summary>
    protected virtual void StartMove(Cell _newParent, Vector2 _delta)
    {
        State = MoveStates.moving;
        moveChange.Set((int)_delta.x, (int)_delta.y);
        RefreshGridData(_newParent.gameObject, gridPos + _delta);
        StartLerp(transform.position, transform.parent.transform.position);
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
        moveCoroutine = StartCoroutine(MoveLoop());
    }

    /// <summary>
    /// Sets the block's position to the target position and change to the idle state
    /// </summary>
    protected virtual void FinishMove()
    {
        transform.position = targetPos;
        State = MoveStates.idle;
    }

    /// <summary>
    /// Sets initial values needed for a lerp
    /// </summary>
    private void StartLerp(Vector3 _start, Vector3 _target)
    {
        lerpTime = 0;
        startPos = _start;
        targetPos = _target;
    }

    /// <summary>
    /// While the block has not reached the target position, move the block
    /// </summary>
    private IEnumerator MoveLoop()
    {
        const float epsilon = 0.0001f;
        while (Vector3.Distance(transform.position, targetPos) > epsilon)
        {
            transform.position = Move();
            if (Vector3.Distance(transform.position, targetPos) < lerpSnapDist)
            {
                FinishMove();
                break;
            }
            else
            {
                yield return null;
            }
        }
        if (Vector3.Distance(transform.position, targetPos) <= lerpSnapDist)
        {
            FinishMove();
        }
        moveCoroutine = null;
    }

    /// <summary>
    /// Determines if a block that we run into can move
    /// </summary>
    private bool CheckHit(Block _hitObj, Vector2 _delta)
    {
        if (GetComponent<Slidey>() == null && _hitObj.CheckMove(_delta)) return true;
        return false;
    }

    #endregion

    #region Grid Information Methods

    /// <summary>
    /// Sets parent grid cell and initial grid position
    /// </summary>
    public void SetNewGridPos(GameObject _parent, Vector2 _newPos)
    {
        _parent.GetComponent<Cell>().ContainObj = gameObject;
        transform.SetParent(_parent.transform);
        gridPos.Set((int)_newPos.x, (int)_newPos.y);
    }

    /// <summary>
    /// Checks if the provided coordinates are within the grid
    /// </summary>
    private bool InGrid(Vector2 _newPos)
    {
        if (_newPos.x >= 0 && _newPos.x < gridManager.gridList.Count &&
            _newPos.y >= 0 && _newPos.y < gridManager.gridList[0].Count) return true;
        return false;
    }

    /// <summary>
    /// Removes previous parent grid cell and sets our new grid data
    /// </summary>
    private void RefreshGridData(GameObject _newParent, Vector2 _newPos)
    {
        if (transform.parent.TryGetComponent<Cell>(out Cell _pCell)) _pCell.RemoveContainObj();
        SetNewGridPos(_newParent.gameObject, _newPos);
        gridManager.UpdateGrid();
    }

    /// <summary>
    /// Removes previous parent grid cell and sets our new grid data
    /// </summary>
    public void SetupNewGridBlock(GridManager _gM, GameObject _parent, Vector2 _newPos)
    {
        gridManager = _gM;
        SetNewGridPos(_parent, _newPos);
    }

    protected virtual void GridChanged(){}

    #endregion 
}
