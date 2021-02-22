using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece {

    public override void SetName(string letter, int number) {
        this.gameObject.name = "King(" + letter + ", " + (number+1) + ")";
    }

    public override List<GameObject> GetPossibleMoves() {
        int l = this.letterIndex;
        int n = this.number;

        List<GameObject> possibleMoves = new List<GameObject>();

        #region up-down-left-right Movement
        // Go Right
        if (CanMoveAt(l + 1, n) || IsEnemyAt(l + 1, n)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l + 1, n));
        }
        // Go Up
        if (CanMoveAt(l, n + 1) || IsEnemyAt(l, n + 1)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l, n + 1));
        }
        // Go Left
        if (CanMoveAt(l - 1, n) || IsEnemyAt(l - 1, n)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l - 1, n));
        }
        // Go Down
        if (CanMoveAt(l, n - 1) || IsEnemyAt(l, n - 1)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l, n - 1));
        }
        #endregion

        #region Diagonal Movement
        // Go up-right
        if (CanMoveAt(l + 1, n + 1) || IsEnemyAt(l + 1, n + 1)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l + 1, n + 1));
        }
        // Go down-right
        if (CanMoveAt(l + 1, n - 1) || IsEnemyAt(l + 1, n - 1)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l + 1, n - 1));
        }
        // Go down-left
        if (CanMoveAt(l - 1, n - 1) || IsEnemyAt(l - 1, n - 1)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l - 1, n - 1));
        }
    
        // Go up-left
        if (CanMoveAt(l - 1, n + 1) || IsEnemyAt(l - 1, n + 1)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l - 1, n + 1));
        }
        #endregion
        return possibleMoves;
    }
}
