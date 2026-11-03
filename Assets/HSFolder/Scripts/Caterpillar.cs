using UnityEngine;

public class Caterpillar : Block
{
    [SerializeField] private Caterpillar nextBlock;
    [SerializeField] private Caterpillar previousBlock;

    private bool isHead = false;
    private bool isTail = false;
    private bool pushedByBlock;

    [SerializeField] private Vector2Int direction = Vector2Int.right;

    private void Awake()
    {
        //Sets block identities :)
        if(nextBlock != null) { nextBlock.previousBlock = this; }

        if (nextBlock == null) { isHead = true; }
    }

    protected override void Start()
    {
        if (previousBlock == null) { isTail = true; }

        base.Start();
        Vector2Int facingDir;

        if (isHead)
        {
            facingDir = direction;
        }
        else
        {
            facingDir = nextBlock.gridPos - gridPos;
        }

        FaceDirection(facingDir);

        Debug.Log(name + gridPos + transform.parent.name);
    }

    public override bool CheckMove(Vector2 _delta) //Now checks move based on the position of the block in front of it
    {
        Debug.Log(name + " gridPos: " + gridPos + " isHead: " + isHead + " isTail: " + isTail + " pushedByBlock: " + pushedByBlock + " state: " + State);
        if(!pushedByBlock && !isTail) { return false; }
        if (State != MoveStates.idle) { return false; }

        //If body or tail move into cell in front of it
        if (!isHead)
        {
            Vector2Int nextPos = nextBlock.gridPos - gridPos;
            return base.CheckMove(nextPos);
        }

        //If head, move straight or turn if blocked
        Vector2Int right = new Vector2Int(direction.y, -direction.x);
        Vector2Int left = -right;

        if (base.CheckMove(direction)) { return true; }

        if (base.CheckMove(right))
        {
            direction = right;
            return true;
        }

        if(base.CheckMove(left))
        {
            direction = left;
            return true;
        }

        return false;
    }

    protected override void StartMove(Cell _newParent, Vector2 _delta)
    {
        base.StartMove(_newParent, _delta);
        FaceDirection(_delta);
    }

    private void FaceDirection(Vector2 _dir) //Sets the direction the caterpillar is facing oooooO
    {
        if (_dir == Vector2.zero) { return; }
        transform.rotation = Quaternion.LookRotation(new Vector3(_dir.x, 0f, _dir.y));
    }

    protected override bool CheckHit(Block _hitObj, Vector2 _delta) //Makes the push travel forwards throughout the snake
    {
        //If head
        if(isHead) { return false; }
        //If body or tail
        if (_hitObj != nextBlock ) { return false; }

        //Check that push came from a block and not the player from the side
        nextBlock.pushedByBlock = true;
        bool wasMoved = nextBlock.CheckMove(_delta);
        nextBlock.pushedByBlock = false;

        return wasMoved;
    }
}
