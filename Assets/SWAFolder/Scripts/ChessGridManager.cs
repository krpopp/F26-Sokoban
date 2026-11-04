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
            if (!p.IsAttackingSquare(WorldToBoardCoords(piece.transform.position), square)) continue;
            if (IsPathBlocked(WorldToBoardCoords(piece.transform.position),square)) continue;
            return true;
        }
        return false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Restart();
    }
    
    private bool IsPathBlocked(Vector2 from, Vector2 to)
    {
        Vector2 delta = to - from;
        if (!(delta.x == 0 || delta.y == 0) && !(Mathf.Abs(delta.x) == Mathf.Abs(delta.y))) return false;

        Vector2 step = new Vector2(System.Math.Sign(delta.x), System.Math.Sign(delta.y));
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

    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
