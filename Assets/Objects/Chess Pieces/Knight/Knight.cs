using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece {
    public override void SetName(string letter, int number) {
        this.gameObject.name = "Knight(" + letter + ", " + (number+1) + ")";
    }

    public override void SetPossibleMoves() {
        int l = this.letterIndex;
        int n = this.number;

        List<GameObject> possibleMoves = new List<GameObject>();

        // Go Up 2 positions
            // Go Right 1 position
        if (CanMoveAt(l + 1, n + 2) || IsEnemyAt(l + 1, n + 2)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l + 1, n + 2));
        }

            // Go Left 1 position
        if (CanMoveAt(l - 1, n + 2) || IsEnemyAt(l - 1, n + 2)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l - 1, n + 2));
        }
        
        // Go Down 2 positions
            // Go Right 1 position
        if (CanMoveAt(l + 1, n - 2) || IsEnemyAt(l + 1, n - 2)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l + 1, n - 2));
        }
            // Go Left 1 position
        if (CanMoveAt(l - 1, n - 2) || IsEnemyAt(l - 1, n - 2)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l - 1, n - 2));
        }

        // Go Right 2 positions
            // Go Up 1 position
        if (CanMoveAt(l + 2, n + 1) || IsEnemyAt(l + 2, n + 1)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l + 2, n + 1));
        }
            // Go Down 1 position
        if (CanMoveAt(l + 2, n - 1) || IsEnemyAt(l + 2, n - 1)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l + 2, n - 1));
        }

        // Go Left 2 positions
            // Go Up 1 position
        if (CanMoveAt(l - 2, n + 1) || IsEnemyAt(l - 2, n + 1)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l - 2, n + 1));
        }
            // Go Down 1 position
        if (CanMoveAt(l - 2, n - 1) || IsEnemyAt(l - 2, n - 1)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l - 2, n - 1));
        }

        this.possibleMovePositions = possibleMoves;
    }
}
