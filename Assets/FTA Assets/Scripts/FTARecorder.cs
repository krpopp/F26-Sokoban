using UnityEngine;

public class FTARecorder : Block
{
    // Variables that store directions, I'd say up to 3.
    // Turn these into a list
    //Vector2Int[] storedDirections = { Vector2Int.one, Vector2Int.two, Vector2Int.three };
    [SerializeField] Vector2 recordingOne; 
    [SerializeField] Vector2 recordingTwo;
    [SerializeField] Vector2 recordingThree;

    bool startMoving = false;
    float inputCounter = 0;
    
    
        // It should count total inputs: if<3 then record, if >= 3 then do recorded actions
    // Function that assigns these when tried to get moved (MODIFY THE MOVE-BLOCK FUNCTION)
    // 
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        recordingOne = new Vector2(0, 0);
        recordingTwo = new Vector2(0, 0);
        recordingThree = new Vector2(0, 0);
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if (startMoving)
        {
            DoRecordedMovement();
        }
    }
    
    
    protected void MovementRecording(Vector2 inputDirection)
    {
        inputCounter++;
        if (inputCounter == 1) recordingOne = inputDirection;
        if (inputCounter == 2) recordingTwo = inputDirection;
        if (inputCounter == 3) recordingThree = inputDirection;
        if (inputCounter >= 4) startMoving = true;
    }

    void DoRecordedMovement()
    {
        //make these actually record the deltas. MAKE THEM HAPPEN INDEPENDENTLY
        CheckMove(recordingOne);
        CheckMove(recordingTwo);
        CheckMove(recordingThree);

        startMoving = false;
        inputCounter = 0;
    }
    
    public override virtual bool CheckMove(Vector2 _delta)
    {
        if (State == MoveStates.idle)
        {
            State = MoveStates.attemptingMove;
            if (canMove && InGrid(gridPos + _delta))
            {
                Cell checkCell = gridManager.gridList[gridPos.x + (int)_delta.x][gridPos.y + (int)_delta.y].GetComponent<Cell>();
                if (!checkCell.CheckContainObj() || CheckHit(checkCell.ContainObj.GetComponent<Block>(), _delta))
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
    
}
