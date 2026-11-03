using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public DoorController doorController;

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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
    {
    }

   //called whenever another collider enters pressure plate trigger
    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;
        //checks if obj entering is player or block component
        Player player = other.GetComponentInParent<Player>();
        Block block = other.GetComponentInParent<Block>();
        
        if (block == null) return;

        if (activationType == ActivationType.Player)
        {
            if (player == null) return;
        }

        if (activationType == ActivationType.Pizza)
        {
            //prevents player from activating pizza only plate
            if (player != null) return;
            //only allow pizza with name to activate
            if (block.gameObject.name != requiredPizzaName) return;
        }
        
        activated = true;
        //tells doorcontroller to count the activation
        doorController.ActivateDoor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
