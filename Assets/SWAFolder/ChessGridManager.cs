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
                if (piece.TryGetComponent<Piece>(out Piece _piece))
                {
                    pieces.Add(piece);
                }
            }
        }
        return pieces;
    }
    
    public bool IsSquareAttacked(Vector2 square, bool color)
    {
        Debug.Log("IsSquareAttacked" + square);
        UpdateGrid();
        foreach (GameObject piece in GetPieces())
        {
            Piece piecePiece = piece.GetComponent<Piece>(); 
            if (piecePiece.color == color) continue;
            if (piecePiece.IsAttackingSquare(WorldToBoardCoords(piece.transform.position), square))
            {
                return true;
            }
        }
        return false;
    }
    
    public Vector2 WorldToBoardCoords(Vector3 _worldPos)
    {
        _worldPos -= transform.position;
        Vector2 boardCoords = new Vector2(_worldPos.x,  _worldPos.z);
        boardCoords = new Vector2(5f,5f)+ boardCoords/cellSize;
        return boardCoords;
    }
}
