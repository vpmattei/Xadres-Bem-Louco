using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece {

    public override void SetName(string letter, int number) {
        this.gameObject.name = "Rook(" + letter + ", " + (number+1) + ")";
    }

    public override void SetPossibleMoves() {
        List<GameObject> possibleMoves = new List<GameObject>();

        // Left
        possibleMoves.AddRange(GetPossibleMovesAtDirection(-1, 0));

        // Up
        possibleMoves.AddRange(GetPossibleMovesAtDirection(0, 1));

        // Right
        possibleMoves.AddRange(GetPossibleMovesAtDirection(1, 0));
        
        // Down
        possibleMoves.AddRange(GetPossibleMovesAtDirection(0, -1));
        
        this.possibleMovePositions = possibleMoves;
    }
}
