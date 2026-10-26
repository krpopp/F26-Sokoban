using UnityEngine;

public class Recorder : Block
{
    // Variables that store directions, I'd say up to 3.
    // Turn these into a list
    Vector2Int[] storedDirections = { Vector2Int.one, Vector2Int.two, Vector2Int.three };
    Vector2Int recordingOne; 
    Vector2Int recordingTwo;
    Vector2Int recordingThree;

    float inputCounter = 0;
    
    
        // It should count total inputs: if<3 then record, if >= 3 then do recorded actions
    // Function that assigns these when tried to get moved (MODIFY THE MOVE-BLOCK FUNCTION)
    // 
    
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        recordingOne = new Vector2Int(0, 0);
        recordingTwo = new Vector2Int(0, 0);
        recordingThree = new Vector2Int(0, 0);
        
    }

    protected override void StartMove(Cell _newParent, Vector2 _delta)
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
