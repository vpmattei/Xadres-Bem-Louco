using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece {
    public override void SetName(string letter, int number) {
        this.gameObject.name = "Bishop(" + letter + ", " + (number+1) + ")";
    }

    public override void SetPossibleMoves() {
        List<GameObject> possibleMoves = new List<GameObject>();

        // Down-Left
        possibleMoves.AddRange(GetPossibleMovesAtDirection(-1, -1));

        // Up-Left
        possibleMoves.AddRange(GetPossibleMovesAtDirection(-1, 1));

        // Up-Right
        possibleMoves.AddRange(GetPossibleMovesAtDirection(1, 1));
        
        // Down-Right
        possibleMoves.AddRange(GetPossibleMovesAtDirection(1, -1));
        
        this.possibleMovePositions = possibleMoves;
    }
}
