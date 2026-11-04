using UnityEngine;

public class PizzaTrigger : MonoBehaviour
{
    
    public LevelProgressionManager levelManager;
    public int levelNumber = 1;
    public string requiredPizzaName; //stores name of pizza box needed to activate trigger
    private bool activated = false; //prevents trigger happen more than once

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return; //stops if tigger activated already

        Block block = other.GetComponentInParent<Block>();

        if (block == null) return; //stops code if object not block
        Player player = other.GetComponentInParent<Player>();
        if (player != null) return;
        if (block.gameObject.name != requiredPizzaName) return; //checks if blocks name matches required pizza name, nothing happens if no match
        activated = true;
        levelManager.CompleteLevel(levelNumber);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetTrigger()
    {
        activated = false;
    }
}
