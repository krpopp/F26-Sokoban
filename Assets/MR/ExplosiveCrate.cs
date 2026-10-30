using UnityEngine;

public class ExplosiveCrate : Block
{
    [Header("---Explosive---")]
    public GameObject explosion;
    public int destroyRange = 1;

    protected override void FinishMove()
    {
        base.FinishMove();
        Slide(moveChange);
    }

    private void Slide(Vector2 _delta)
    {
        if (!CheckMove(_delta))
        {
            DestroyNeighbours((int)_delta.x, (int)_delta.y); // needs a delta offset bc gridPos isnt set to the final position??

            GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
            g.transform.position = new Vector3(gridPos.x, 0f, gridPos.y);
            Debug.Log(gridPos);

            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void DestroyNeighbours(int offsetX, int offsetY) 
    {
        Vector2Int offsetPos = gridPos + new Vector2Int(offsetX, offsetY);
        for (int x = offsetPos.x - destroyRange; x < offsetPos.x + destroyRange; x++) 
        {
            for (int y = offsetPos.y - destroyRange; y < offsetPos.y + destroyRange; y++)
            {
                if (GameManager.InBounds(x, y)) 
                {
                    Cell cell = GameManager.GridCell(x, y);
                    if (cell.ContainObj != null)
                        Destroy(cell.ContainObj);
                }
            }
        }
    }
}
