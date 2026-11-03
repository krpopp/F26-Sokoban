using System;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool color;
    public PieceType pieceType;
    
    public enum PieceType
    {
        QUEEN, KING, PAWN, BISHOP, ROOK, KNIGHT
    }

    public bool IsAttackingSquare(Vector2 from, Vector2 to)
    {
        switch (pieceType)
        {
            case PieceType.QUEEN:
                return (RookAttacks(from, to) || BishopAttacks(from, to));
                break;
            case PieceType.PAWN:
                return PawnAttacks(from, to);
                break;
            case PieceType.BISHOP:
                return BishopAttacks(from, to);
                break;
            case PieceType.KNIGHT:
                return KnightAttacks(from, to);
                break;
            case PieceType.KING:
                return KingAttacks(from, to);
                break;
            case PieceType.ROOK:
                return RookAttacks(from, to);
                break;
            default:
                return false;
                break;
        }
    }

    public bool RookAttacks(Vector2 from, Vector2 to)
    {
        if (from.x == to.x || from.y == to.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool KnightAttacks(Vector2 from, Vector2 to)
    {
        if (from.x + 1 == to.x || from.x - 1 == to.x)
        {
            if (from.y + 2 == to.y || from.y - 2 == to.y)
            {
                return true;
            }
        }

        if (from.y + 1 == to.y || from.y - 1 == to.y)
        {
            if (from.x + 2 == to.x || from.x - 2 == to.x)
            {
                return true;
            }
        }
        return false;
    }

    public bool BishopAttacks(Vector2 from, Vector2 to)
    {
        return Mathf.Abs(from.x - to.x) == Mathf.Abs(from.y - to.y);
    }

    public bool PawnAttacks(Vector2 from, Vector2 to)
    {
        if (color)
        {
            if (from.y + 1 == to.y && (from.x - 1 == to.x || from.x + 1 == to.x))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (from.y - 1 == to.y && (from.x - 1 == to.x || from.x + 1 == to.x))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool KingAttacks(Vector2 from, Vector2 to)
    {
        if (from.y + 1 == to.y || from.y == to.y || from.y - 1 == to.y)
        {
            if(from.x + 1 == to.x || from.x == to.x || from.x - 1 == to.x)
            {
                return true;
            }
        }
        return false;
    }
}
