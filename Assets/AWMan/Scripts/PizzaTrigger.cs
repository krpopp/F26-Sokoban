using UnityEngine;

public class PizzaTrigger : MonoBehaviour
{
    
    public LevelProgressionManager levelManager;
    public GridManager gridManager;
    public int levelNumber = 1;
    public string requiredPizzaName; //stores name of pizza box needed to activate trigger
    private bool activated = false; //prevents trigger happen more than once
    private Cell targetCell;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //finds grid cell where pizza loc
        FindTargetCell();
    }

    private void FindTargetCell()
    {
        //stops if no gridmanager assigned
        if (gridManager == null) return;
        //keeps track of closest distance 
        float closestDistance = float.MaxValue;
        //goes through every column in grid then cell in current column
        foreach (var column in gridManager.gridList)
        {
            foreach (GameObject cellObject in column)
            {
                Vector3 cellPosition = cellObject.transform.position;
                //calculates the distance between goal and the cell 
                float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
                    new Vector2(cellPosition.x, cellPosition.z));

                //checks if cell is closer than previous closest cell
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetCell = cellObject.GetComponent<Cell>();
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //stops checking if goal already activated or no target cell found
        if (activated || targetCell == null) return;
        //checks which object is currently occupying target cell
        GameObject occupant = targetCell.ContainObj;
        if (occupant == null) return;
        //checks if obj in cell has block component
        Block block = occupant.GetComponent<Block>();
        if (block == null) return;

        //so player cannot activate the goal
        if (occupant.GetComponent<Player>() != null) return;
        //checks if obj is the required pizza box
        if (occupant.name != requiredPizzaName) return;
        activated = true;
        //checks if levelmanager assigned and if level completed
        if (levelManager != null)
        {
            levelManager.CompleteLevel(levelNumber);
        }
    }

    public void ResetTrigger()
    {
        activated = false;
    }
}
