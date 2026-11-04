using UnityEngine;
using System.Collections.Generic;

public class RoomReset : MonoBehaviour
{
    public GridManager gridManager;

    private Block[] blocks;

    private Vector3[] startingPositions;
    private Quaternion[] startingRotations;

    //stores cell and grid orgininal pos each block
   private  Cell[] startingCells;
   private Vector2Int[] startingGridPositions;

   //stores doors, pressure palte,s pizza triggers in room
   private DoorController[] doors;
   private PressurePlate[] plates;
   private PizzaTrigger[] pizzaTriggers;
   
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   private void Start()
   {
       Block[] allBlocks = GetComponentsInChildren<Block>(true);
       //creates list storing what blocks allowed to move
       List<Block> moveableBlocks = new List<Block>();
       foreach (Block block in allBlocks)
       {
           if (block.canMove)
           {
               moveableBlocks.Add(block);
           }
       }

       //converts list of moveable blocks into array
       blocks = moveableBlocks.ToArray();
       //creates arrays to remember start info of each block
       startingPositions = new Vector3[blocks.Length];
       startingRotations = new Quaternion[blocks.Length];
       startingCells = new Cell[blocks.Length];
       startingGridPositions = new Vector2Int[blocks.Length];
       
       //remembers each blocks original position and grid cell
       for(int i =0; i < blocks.Length; i++)
       {
           startingPositions[i] = blocks[i].transform.position;
           startingRotations[i] = blocks[i].transform.rotation;
           startingCells[i] = blocks[i].transform.parent.GetComponent<Cell>();
           //checks if block has starting cell
           if (startingCells[i] == null)
           {
               //testing-to see if any cell is not found
               Debug.LogError("missing starting cell for: " + blocks[i].name);
           }
           startingGridPositions[i] = blocks[i].gridPos;
       }

       //finds and saves doorcontroller/pressureplates/pizzatrigger components in room
       doors = GetComponentsInChildren<DoorController>(true);
       plates = GetComponentsInChildren<PressurePlate>(true);
       pizzaTriggers = GetComponentsInChildren<PizzaTrigger>(true);
   }

   //resets room back to orgininal state
   public void ResetRoom()
   {
       //has ever block finished moving before reset
       foreach (Block block in blocks)
       {
           //if any block still moving, stop the reset
           if (block.State != Block.MoveStates.idle) return;
       }
       
       //clears current and origninal cell for every block
       for (int i = 0; i < blocks.Length; i++)
       {
           Cell currentCell = blocks[i].transform.parent.GetComponent<Cell>();

           //if current cell contains this block
           if (currentCell != null && currentCell.ContainObj == blocks[i].gameObject)
           {
               currentCell.RemoveContainObj();
           }

           //if block had original cell saved
           if (startingCells[i] != null)
           {
               startingCells[i].RemoveContainObj();
           }
       }

       //moves every block back to its original grid position
       for (int i = 0; i < blocks.Length; i++)
       {
           blocks[i].SetupNewGridBlock(gridManager, startingCells[i].gameObject, startingGridPositions[i]);
           blocks[i].transform.position = startingPositions[i];
           blocks[i].transform.rotation = startingRotations[i];
       }

       //reset doors, pressure plates, pizza goals
       foreach (DoorController door in doors)
           door.ResetDoor();
       foreach (PressurePlate plate in plates)
           plate.ResetPlate();
       foreach (PizzaTrigger trigger in pizzaTriggers)
           trigger.ResetTrigger();
   }
}
