using System.Collections;
using UnityEngine;

public class CouchBlocks : Block
{
    public int numBlocks;
    //public GameObject 
    public GameObject[] followingBlocks;
    public GameObject blockPushed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        followingBlocks = new GameObject[numBlocks];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numBlocks; i++)
        {
            if (followingBlocks[i].GetComponent<CouchBlocks>().State == MoveStates.attemptingMove)
            {
                
            }
        }
    }
}
