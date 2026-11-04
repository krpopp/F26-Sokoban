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
    
    //color settings
    public Renderer plateRenderer;

    public Color originalColor;

    public AudioSource audioSource;

    public AudioClip pressSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
    {
        if (plateRenderer != null)
        {
            originalColor = plateRenderer.material.color;
        }
    }
    
   //called whenever another collider enters pressure plate trigger
   private void OnTriggerEnter(Collider other)
   {
       if (activated) return;
       //checks if obj entering is player or block component
       Block block = other.GetComponentInParent<Block>();

       if (block == null) return;

       if (activationType == ActivationType.Player)
       {
           if (other.CompareTag("Player") || block.CompareTag("Player"))
           {
               ActivatePlate();
           }
       }
       else if (activationType == ActivationType.Pizza)
       {
               if (block.gameObject.name.Contains(requiredPizzaName))
               {
                   ActivatePlate();
               }
       }
   }

   private void ActivatePlate()
   {
            activated = true;
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
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPlate()
    {
        activated = false;
        if(plateRenderer != null)
        {
            plateRenderer.material.color = originalColor;
        }
    }
}
