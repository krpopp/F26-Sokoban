using UnityEngine;

public class PizzaTrigger : MonoBehaviour
{
    public string requiredPizzaName; //stores name of pizza box needed to activate trigger

    public GameObject objectToActivate; //reference room that appears after activation
    public GameObject objectToDeactivate; //reference room that should dissapear after activation

    private bool activated = false; //prevents trigger happen more than once

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return; //stops if tigger activated already

        Block block = other.GetComponentInParent<Block>();

        if (block == null) return; //stops code if object not block
        if (block.gameObject.name != requiredPizzaName) return; //checks if blocks name matches required pizza name, nothing happens if no match
        activated = true;

        if (objectToDeactivate != null) //checks if an object has been assigned in disappearing
            objectToDeactivate.SetActive(false); 

        if (objectToActivate != null) //checks if an object has been assigned in appearing
            objectToActivate.SetActive(true); 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
