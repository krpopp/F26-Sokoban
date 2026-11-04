using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform doorPivot;

    public float openAngle = 90f;

    public float rotationSpeed = 2f;

    //how many puzzles needed to open door
    public int requiredActivations = 1;
    //keeps track how many times door been activated
    private int currentActivations = 0;
    //stores orignal rotation before open
    private Quaternion closedRotation;
    //stores rotation door need reahed when opened
    private Quaternion openRotation;

    //keeps track if door already been opened
    private bool doorOpened = false;

    public bool IsOpen => doorOpened;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closedRotation = doorPivot.localRotation;
        //calculate door open rotation using starting rotation, num of degrees rotated
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
    }

    //pressure plate or pizza indicator when activated
    public void ActivateDoor()
    {
        if (doorOpened) return;
        currentActivations++;

        //check if enough activation happened to open door
        if (currentActivations >= requiredActivations)
        {
            doorOpened = true;
        }
    }

    private void Update()
    {
        //only rotates door if activated
        if (doorOpened)
        {
            //smooth rotate
            doorPivot.localRotation = Quaternion.Slerp(doorPivot.localRotation, openRotation, rotationSpeed * Time.deltaTime);
        }
    }
    
    //for reset method, reset door to orgininal closed roation + lets pressure plate activate again
    public void ResetDoor()
    {
        currentActivations = 0;
        doorOpened = false;
        doorPivot.localRotation = closedRotation;
    }
}
