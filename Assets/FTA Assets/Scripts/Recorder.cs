using UnityEngine;

public class Recorder : Block
{
    // Variables that store directions, I'd say up to 3.
    // Turn these into a list
    //Vector2Int[] storedDirections = { Vector2Int.one, Vector2Int.two, Vector2Int.three };
    Vector2 recordingOne; 
    Vector2 recordingTwo;
    Vector2 recordingThree;

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
        
    }

    void MovementRecording(Vector2 inputDirection)
    {
        inputCounter++;
        if (inputCounter == 1) recordingOne = inputDirection;
        if (inputCounter == 2) recordingTwo = inputDirection;
        if (inputCounter == 3) recordingThree = inputDirection;
        if (inputCounter >= 4)
        {
            //make these actually record the deltas
            StartMove(_newParent, recordingOne);
            StartMove(_newParent, recordingTwo);
            StartMove(_newParent, recordingThree);
        }

    }
    
}
