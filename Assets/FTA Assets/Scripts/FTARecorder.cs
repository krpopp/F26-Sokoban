using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class FTARecorder : Block
{
    // Variables that store directions, I'd say up to 3.
    // Turn these into a list
    //Vector2Int[] storedDirections = { Vector2Int.one, Vector2Int.two, Vector2Int.three };
    [SerializeField] Vector2 recordingOne; 
    [SerializeField] Vector2 recordingTwo;
    [SerializeField] Vector2 recordingThree;
    
    bool IsRecording = true;
    float inputCounter = 0;

    //[SerializeField] Collider myCollider;

    [SerializeField] float recordedActionInterval;
    [SerializeField] float recordedWaitTimeCounter;
    [SerializeField] float timeSinceRecording;
    [SerializeField] float recordingInputCooldown;

    int recordingCounter = 0;
    int timeSinceFinalPosition = 0;
    
    
        // It should count total inputs: if<3 then record, if >= 3 then do recorded actions
    // Function that assigns these when tried to get moved (MODIFY THE MOVE-BLOCK FUNCTION)
    // 
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        if (transform.position.x == -5 & transform.position.z == -1)
        {
            timeSinceFinalPosition++;
            if(timeSinceFinalPosition >= 120)
            {
                Debug.Log("YOU WON!!!!");
                SceneManager.LoadScene("FTA FIRST");
            }
        }
        canMove = false;
        base.Start();
        recordingOne = new Vector2(0, 0);
        recordingTwo = new Vector2(0, 0);
        recordingThree = new Vector2(0, 0);
        
    }
    
    // Update is called once per frame
    void Update()
    {
        timeSinceRecording++;
        if (!IsRecording)
        {
            if(timeSinceRecording >= recordedActionInterval*1.5)
            {
                DoRecordedMovement();
                timeSinceRecording = 0;
            }
        }
    }
    
    protected void MovementRecording(Vector2 inputDirection)
    {
        inputCounter++;
        if (inputCounter == 1) recordingOne = inputDirection;
        else if (inputCounter == 2) recordingTwo = inputDirection;
        else if (inputCounter == 3)
        {
            recordingThree = inputDirection;
            IsRecording = false;
        }
    
        Debug.Log(inputDirection);
        Debug.Log(inputCounter); 
    }

    void DoRecordedMovement()
    {
        //make these actually record the deltas. MAKE THEM HAPPEN INDEPENDENTLY
        recordedWaitTimeCounter++;
        if(recordedWaitTimeCounter >= recordedActionInterval * Time.deltaTime)
        {
            Debug.Log("doing Recorded movement");
            if(recordingCounter == 0) TryRecordedMove(recordingOne);
            if(recordingCounter == 1) TryRecordedMove(recordingTwo);
            if(recordingCounter == 2) TryRecordedMove(recordingThree);
            if(recordingCounter == 3) ResetRecorderBlock();
            recordingCounter++;
            recordedWaitTimeCounter = 0;
        }
    }

    /*
    protected override void StartMove(Cell _newParent, Vector2 _delta)
    {
        Debug.Log("Called Start Move!!!");
        if (IsRecording)
        {
            MovementRecording(_delta);
            return;
        }
        
        base.StartMove(_newParent, _delta);
    }
    */

    public override bool CheckMove(Vector2 _delta)
    {
        //Debug.Log("Called CheckMove!!!");
        if (IsRecording)
        {
            if (timeSinceRecording >= recordingInputCooldown)
            {
                MovementRecording(_delta);
                timeSinceRecording = 0;
            }
        }
        return (false);
    }
    
    private void TryRecordedMove(Vector2 _delta)
    {
        if (State == MoveStates.idle)
        {
            State = MoveStates.attemptingMove;
            if (InGrid(gridPos + _delta))
            {
                Cell checkCell = gridManager.gridList[gridPos.x + (int)_delta.x][gridPos.y + (int)_delta.y].GetComponent<Cell>();
                if (!checkCell.CheckContainObj() ||
                    CheckHit(checkCell.ContainObj.GetComponent<Block>(), _delta))
                {
                    StartMove(checkCell, _delta);
                }
                

                
                /*
                {
                    Cell finalCell = gridManager.gridList[gridPos.x + (int)_delta.x][gridPos.y + (int)_delta.y]
                        .GetComponent<Cell>();
                    if (!finalCell)
                }
                */
            }
            State = MoveStates.idle;
        }
    }
    
    void ResetRecorderBlock()
    {
        IsRecording = true;
        inputCounter = 0;
        recordingCounter = -1;
        
    }
    
}
