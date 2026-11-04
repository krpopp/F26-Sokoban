using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChessGridManager :GridManager
{
    public String nextSceneName;
    public List<GameObject> pieces;
    public GameObject winPrompt;
    public void Start()
    {
        GameObject.FindGameObjectsWithTag("block", pieces);
    }
    
    public bool IsSquareAttacked(Vector2 square, bool color)
    {
        foreach (GameObject piece in pieces)
        {
            Piece p = piece.GetComponent<Piece>();
            if (p == null || p.color == color) continue;
            if (!p.IsAttackingSquare(WorldToBoardCoords(piece.transform.position), square)) return false;
            if (IsPathBlocked(WorldToBoardCoords(piece.transform.position),square)) return false;
            return true;
        }
        return false;
    }
    
    private bool IsPathBlocked(Vector2 from, Vector2 to)
    {
        float deltaX = to.x - from.x;
        float deltaY = to.y - from.y;
        
        bool straight = deltaX == 0 || deltaY == 0;
        bool diagonal = Mathf.Abs(deltaX) == Mathf.Abs(deltaY);
        if (!straight && !diagonal) return false;

        Vector2 step = new Vector2(System.Math.Sign(deltaX), System.Math.Sign(deltaY));

        
        for (Vector2 i = from + step; i != to; i += step)
        {
            if (IsOccupied(i)) return true;
        }
        return false;
    }
    
    private bool IsOccupied(Vector2 boardPos)
    {
        foreach (GameObject piece in pieces)
        {
            if (piece == null) continue;
            if (Vector2Int.RoundToInt(WorldToBoardCoords(piece.transform.position)) == boardPos)
                return true;
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

    public void Win()
    {
        winPrompt.SetActive(true);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
