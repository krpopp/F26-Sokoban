using System.Collections.Generic;
using UnityEngine;

public class ChessGridManager :GridManager
{
    public List<GameObject> GetPieces()
    {
        List<GameObject> pieces = new List<GameObject>();
        foreach (List<GameObject> list in gridList)
        {
            foreach (GameObject piece in list)
            {
                if (piece.TryGetComponent())
                {
                    pieces.Add(piece);
                }
            }
        }
        return pieces;
    }
    
    public bool IsSquareAttacked(Vector2 square, bool color)
    {
        UpdateGrid();
        foreach (GameObject piece in GetPieces())
        {
            
        }
        return false;
    }
}
