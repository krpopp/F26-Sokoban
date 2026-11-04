using UnityEngine;
using System.Collections.Generic;

public class RoomReset : MonoBehaviour
{
    public GridManager gridManager;

    private Block[] blocks;

    private Vector3[] startingPositions;
    private Quaternion[] startingRotations;

   private  Cell[] startingCells;
   private Vector2Int[] startingGridPositions;

  private DoorController[] doors;

   private PressurePlate[] plates;
   private PizzaTrigger[] pizzaTriggers;
   
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   private void Start()
   {
       Block[] allBlocks = GetComponentsInChildren<Block>(true);
       List<Block> moveableBlocks = new List<Block>();
       foreach (Block block in allBlocks)
       {
           if (block.canMove)
           {
               moveableBlocks.Add(block);
           }
       }

       blocks = moveableBlocks.ToArray();
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
           if (startingCells[i] == null)
           {
               Debug.LogError("Missing starting Cell for: " + blocks[i].name);
           }
           startingGridPositions[i] = blocks[i].gridPos;
       }

       doors = GetComponentsInChildren<DoorController>(true);
       plates = GetComponentsInChildren<PressurePlate>(true);
       pizzaTriggers = GetComponentsInChildren<PizzaTrigger>(true);
   }

   public void ResetRoom()
   {
       foreach (Block block in blocks)
       {
           if (block.State != Block.MoveStates.idle) return;
       }
       
       for (int i = 0; i < blocks.Length; i++)
       {
           Cell currentCell = blocks[i].transform.parent.GetComponent<Cell>();

           if (currentCell != null && currentCell.ContainObj == blocks[i].gameObject)
           {
               currentCell.RemoveContainObj();
           }

           if (startingCells[i] != null)
           {
               startingCells[i].RemoveContainObj();
           }
       }

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
