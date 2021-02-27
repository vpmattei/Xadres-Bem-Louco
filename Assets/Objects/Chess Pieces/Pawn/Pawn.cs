using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {

    public override void SetName(string letter, int number) {
        this.gameObject.name = "Pawn(" + letter + ", " + (number+1) + ")";
    }

    public override void SetPossibleMoves() {
        int l = this.letterIndex;
        int n = this.number;
        int movesForward = 2;
        if (hasMoved) movesForward -= 1;

        List<GameObject> possibleMoves = new List<GameObject>();

        int i = 1;
        // Black Pawn
        if (gameObject.CompareTag("Player2")) {
            while (i <= movesForward && CanMoveAt(l, n - i)) {
                possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l, n - i));
                i++;
            }
            if (IsEnemyAt(l + 1, n - 1)) possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l + 1, n - 1));
            if (IsEnemyAt(l - 1, n - 1)) possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l - 1, n - 1));
        }
        // White Pawn
        else {
            while (i <= movesForward && CanMoveAt(l, n + i)) {
                possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l, n + i));
                i++;
            }
            if (IsEnemyAt(l + 1, n + 1)) possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l + 1, n + 1));
            if (IsEnemyAt(l - 1, n + 1)) possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l - 1, n + 1));
        }

        this.possibleMovePositions = possibleMoves;
    }
}
