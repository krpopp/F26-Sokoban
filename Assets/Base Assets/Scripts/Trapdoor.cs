using UnityEngine;

public class Trapdoor : Cell
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject ladder;
    //public GameObject couch;
    public GameObject rug;
    public GameObject player;
    
    bool coveredByRug; //check has rug on it
    bool hasLadderOnIt;
    bool hasWalkedOverIt;
    
    void Start()
    {
        coveredByRug = (ContainObj != null) && (ContainObj == rug);

    }

    // Update is called once per frame
    void Update()
    {
        coveredByRug = (ContainObj != null) && (ContainObj.CompareTag("Rug"));
        //Debug.Log("Is covered by rug");
        if (!coveredByRug)
        {
            //Debug.Log("Is not covered by rug");
            hasWalkedOverIt = (ContainObj != null) && (ContainObj == player);

            if (hasWalkedOverIt) //open trapdoor
            {
                hasLadderOnIt = (ContainObj != null) && (ContainObj == ladder);
                
                if (hasLadderOnIt)
                {
                    Debug.Log("Is not covered by rug, has been opened, and ladder is on it");
                }
            }
        }
        
        hasLadderOnIt = (ContainObj != null) && (ContainObj == ladder);
                
        if (hasLadderOnIt)
        {
            Debug.Log("Ladder on it");
        }
    }
    
    
}
