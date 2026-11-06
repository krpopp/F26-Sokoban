using UnityEngine;

public class Trapdoor : Block
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
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
