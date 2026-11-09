using System.Runtime.CompilerServices;
using UnityEngine;

public class PlateManager : MonoBehaviour
{
    private GridManager gridManager;
    private PressurePlate[] plates;

    private Door[] doors;
    private bool gridChanged;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridManager = GetComponent<GridManager>();
        plates = FindObjectsByType<PressurePlate>(FindObjectsSortMode.None);
        doors = FindObjectsByType<Door>(FindObjectsSortMode.None);

        foreach(PressurePlate plate in plates)
        {
            plate.SetCell(FindCell(plate.transform.position));
        }

        CheckPlates();
    }

    private void GridChanged()
    {
        gridChanged = true;
    }

    private void LateUpdate()
    {
        if(!gridChanged) { return; }
        gridChanged = false;
        CheckPlates();
        
    }

    private void CheckPlates()
    {
        foreach(PressurePlate plate in plates)
        {
            plate.PressDown();
        }

        foreach(Door door in doors)
        {
            bool anyPressed = false;
            foreach(PressurePlate plate in door.plates)
            {
                
                if (plate !=null && plate.isPressed)
                {
                    anyPressed = true;
                    break;
                }
                
            }
            door.OpenDoor(anyPressed);

        }
    }

    private Cell FindCell(Vector3 _worldPos)
    {
        foreach(var column in gridManager.gridList)
        {
            foreach(GameObject cellObj in column)
            {
                Vector3 p = cellObj.transform.position;
                if(Mathf.Abs(p.x - _worldPos.x) < 0.1f && Mathf.Abs(p.z - _worldPos.z) < 0.1f)
                {
                    return cellObj.GetComponent<Cell>();
                }

            }
        }
        return null;
    }



}
