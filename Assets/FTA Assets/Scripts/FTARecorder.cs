using UnityEngine;

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

    [SerializeField] float recordingActionInterval;
    [SerializeField] float recordingActionCounter;
    
    
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
        
        if (!IsRecording)
        {
            DoRecordedMovement();
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
            canMove = false;
        }
        Debug.Log(inputDirection);
        Debug.Log(inputCounter); 
    }

    void DoRecordedMovement()
    {
        //make these actually record the deltas. MAKE THEM HAPPEN INDEPENDENTLY
        recordingActionCounter++;
        if(recordingActionCounter == recordingActionInterval * Time.deltaTime)
        {
            CheckMove(recordingOne);
        }
        if(recordingActionCounter == 2 * recordingActionInterval * Time.deltaTime)
        {
            CheckMove(recordingTwo);
        }
        if(recordingActionCounter == 3 * recordingActionInterval * Time.deltaTime)
        {
            CheckMove(recordingThree);
        }
        if(recordingActionCounter == 4 * recordingActionInterval * Time.deltaTime)
        {
            ResetRecorderBlock();
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
        
        Debug.Log("Called CheckMove!!!");
        if (IsRecording)
        {
            MovementRecording(_delta);
            return(false);
        }
        canMove = true;
        base.CheckMove(_delta);
        canMove = false;
        return (true);
    }
    
    void ResetRecorderBlock()
    {
        IsRecording = true;
        inputCounter = 0;
        canMove = true;
    }
    
}
