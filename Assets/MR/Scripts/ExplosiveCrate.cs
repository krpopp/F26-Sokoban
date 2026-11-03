using UnityEngine;

public class ExplosiveCrate : Slidey
{
    [Header("---Explosive---")]
    public GameObject explosion;
    public int destroyRange = 1;

    protected override void Slide(Vector2 _delta)
    {
        if (!CheckMove(_delta))
        {
            DestroyNeighbours();

            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void DestroyNeighbours() 
    {
        for (int x = gridPos.x - destroyRange; x <= gridPos.x + destroyRange; x++) 
        {
            for (int y = gridPos.y - destroyRange; y <= gridPos.y + destroyRange; y++)
            {
                if (GameManager.InBounds(x, y)) 
                {
                    Cell cell = GameManager.GridCell(x, y);
                    if (cell.ContainObj != null && cell.ContainObj.GetComponent<Block>() != null && cell.ContainObj != gameObject && !cell.ContainObj.name.Contains("Wall"))
                    {
                        Destroy(cell.ContainObj);
                    }
                }
            }
        }
    }
}
