using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class LayoutSpawner : MonoBehaviour
{
    // remember to enable read/write and set compression to none in Texture2D settings
    public Texture2D img;
    public Transform lvlHolder;
    public Transform bounds;
    public GameObject floorTile;
    public GameObject wallTile;
    public GameObject[] blockTypes;
    public Color[] associatedBlockColors;
    public float tileSpacing = 1f;

    [Header("  ")]
    public bool generate;

    private void OnValidate()
    {
        if (generate) 
        {
            generate = false;

            List<Transform> tiles = new List<Transform>();
            Vector3 minPos = Vector3.one * 999f;
            Vector3 maxPos = Vector3.one * -999f;
            for (int x = 0; x < img.width; x++) 
            {
                for (int y = 0; y < img.height; y++)
                {
                    Vector3 pos = new Vector3(x, 0, y) * tileSpacing;

                    Color pxl = img.GetPixel(x, y);
                    GameObject tileInst = null;
                    if (pxl == Color.white)
                    {
                        tileInst = Instantiate(floorTile, pos, Quaternion.identity);
                    }
                    else if (pxl == Color.black)
                    {
                        tileInst = Instantiate(wallTile, pos, Quaternion.identity);
                    }
                    else
                    {
                        for (int i = 0; i < blockTypes.Length; i++)
                        {
                            if (pxl == associatedBlockColors[i])
                            {
                                Transform floorTileClone = Instantiate(floorTile, pos, Quaternion.identity).transform;
                                floorTileClone.parent = lvlHolder;
                                tiles.Add(floorTileClone);

                                tileInst = Instantiate(blockTypes[i], pos, Quaternion.identity);
                                break;
                            }
                        }
                    }

                    if (tileInst != null)
                    {
                        tileInst.transform.parent = lvlHolder;
                        tiles.Add(tileInst.transform);

                        maxPos = new Vector3(Mathf.Max(maxPos.x, tileInst.transform.position.x), 0f, Mathf.Max(maxPos.z, tileInst.transform.position.z));
                        minPos = new Vector3(Mathf.Min(minPos.x, tileInst.transform.position.x), 0f, Mathf.Min(minPos.z, tileInst.transform.position.z));
                    }
                }
            }

            Vector3 offset = (maxPos + minPos) / 2;
            foreach (Transform tile in tiles)
                tile.position -= new Vector3((int)offset.x, 0f, (int)offset.z);
            // bounds scaling isnt completely accurate but like who gaf
            bounds.localScale = maxPos - minPos;
            bounds.localScale += Vector3.up * 0.5f;
            bounds.position = Vector3.zero;
        }
    }
}
