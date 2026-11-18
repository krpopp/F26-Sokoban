using UnityEngine;

public class Door : Block
{
    public PressurePlate[] plates;

    [SerializeField] private GameObject door;

    private Cell thisCell;
    private bool isOpen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        thisCell = transform.parent.GetComponent<Cell>();
    }

    public void OpenDoor(bool open)
    {
        if(open == isOpen)
        {
            return;
        }

        if (open)
        {
            thisCell.RemoveContainObj();
            isOpen = true;
        }
        else
        {
            if (thisCell.CheckContainObj()) { return; }
            thisCell.ContainObj = gameObject;
            isOpen = false;
        }

        if(door != null)
        {
            door.SetActive(!isOpen);
        }
    }
}
