using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private Transform plate;
    [SerializeField] private float pressDrop = 0.1f;
    private Cell cell;
    private Vector3 startPos;

    private bool wasPressed;
    public bool isPressed => cell != null && cell.CheckContainObj();

    private void Awake()
    {
        if(plate != null) 
        {
            startPos = plate.localPosition;
        }

    }

    public void SetCell(Cell _cell)
    {
        cell = _cell;
    }

    public void PressDown()
    {
        if(plate == null) { return; }

        if(isPressed && !wasPressed)
        {
            SoundManager.Play(SoundManager.Instance.platePress);
        }
        wasPressed = isPressed;

        if (isPressed)
        {
            plate.localPosition = startPos + Vector3.down * pressDrop;
        }
        else {
            plate.localPosition = startPos;
        }
    }

  


}
