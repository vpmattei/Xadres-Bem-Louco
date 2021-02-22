using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece {
    public override void SetName(string letter, int number) {
        this.gameObject.name = "Queen(" + letter + ", " + (number+1) + ")";
    }

    public override List<GameObject> GetPossibleMoves() {
        List<GameObject> possibleMoves = new List<GameObject>();

        // Down-Left
        possibleMoves.AddRange(GetPossibleMovesAtDirection(-1, -1));

        // Left
        possibleMoves.AddRange(GetPossibleMovesAtDirection(-1, 0));

        // Up-Left
        possibleMoves.AddRange(GetPossibleMovesAtDirection(-1, 1));

        // Up
        possibleMoves.AddRange(GetPossibleMovesAtDirection(0, 1));

        // Up-Right
        possibleMoves.AddRange(GetPossibleMovesAtDirection(1, 1));

        // Right
        possibleMoves.AddRange(GetPossibleMovesAtDirection(1, 0));
        
        // Down-Right
        possibleMoves.AddRange(GetPossibleMovesAtDirection(1, -1));
        
        // Down
        possibleMoves.AddRange(GetPossibleMovesAtDirection(0, -1));
        
        return possibleMoves;
    }
}
