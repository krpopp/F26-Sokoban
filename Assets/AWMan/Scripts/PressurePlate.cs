using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public DoorController doorController;
    public GridManager gridManager;

    public enum ActivationType
    {
        Player, 
        Pizza, 
    }
    
    public ActivationType activationType;
    //stores exact name to activate plate
    public string requiredPizzaName;
    //tracks if pressure plate already been activated
    private bool activated = false;
    private Cell targetCell;
    
    //color settings
    public Renderer plateRenderer;

    public Color originalColor;

    public AudioSource audioSource;

    public AudioClip pressSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
    {
        //saves pressure plates original color before activated
        if (plateRenderer != null)
        {
            originalColor = plateRenderer.material.color;
        }
        FindTargetCell();
    }
   
   //finds closest grid cell to pressure plate
    private void FindTargetCell()
    {
        if (gridManager == null) return;
        float closestDistance = float.MaxValue;
        
        //goes through ever cell again in current column -same stuff like pizza trigger
        foreach(var column in gridManager.gridList)
        {
            foreach (GameObject cellObject in column)
            {
                Vector3 cellPosition = cellObject.transform.position;
                float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
                    new Vector2(cellPosition.x, cellPosition.z));
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetCell = cellObject.GetComponent<Cell>();
                }
            }
        }
    }

    void Update()
    {
        //stops checking if plate already activated or no target cell found
        if (activated || targetCell == null) return;
        GameObject occupant = targetCell.ContainObj;
        if (occupant == null) return;
        Block block = occupant.GetComponent<Block>();
        if (block == null) return;
        //checks if pressure plate is set to activate when player steps
        if (activationType == ActivationType.Player)
        {
            //checks if object in cell is the player
            if (occupant.GetComponent<Player>() != null)
            {
                ActivatePlate();
            }
        }
        //checks if the pressure palte is set to activate when pizza box placed
        else if (activationType == ActivationType.Pizza)
        {
            if (occupant.name.Contains(requiredPizzaName))
            {
                ActivatePlate();
            }
        }
    }

    //handles what happens when pressure plate activated
   private void ActivatePlate()
   {
       //marks it so it cant activate again
            activated = true;
            //changes pressure plate color to green
            if (plateRenderer != null)
            {
                plateRenderer.material.color = Color.green;
            }

            if (audioSource != null && pressSound != null)
            {
                audioSource.PlayOneShot(pressSound);
            }

            if (doorController != null)
            {
                //tells doorcontroller to count the activation
                doorController.ActivateDoor();
            }
    }
   
   //reset pressure plate to its orgininal state
    public void ResetPlate()
    {
        activated = false;
        if(plateRenderer != null)
        {
            plateRenderer.material.color = originalColor;
        }
    }
}
