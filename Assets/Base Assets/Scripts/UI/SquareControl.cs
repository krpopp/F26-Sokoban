using UnityEngine;

public class SquareControl : MonoBehaviour
{

    public GameObject square;
    public int gridWidth;
    public int gridHeight;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 newPos = new Vector3(transform.position.x + x, transform.position.y + y);
                Instantiate(square, newPos, Quaternion.identity, transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
