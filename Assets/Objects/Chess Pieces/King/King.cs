using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class King : Piece {

    [SerializeField] private List<GameObject> possibleRoqueMoves;

    public override void SetName(string letter, int number) {
        this.gameObject.name = "King(" + letter + ", " + (number+1) + ")";
    }

    public override void SetPossibleMoves() {
        int l = this.letterIndex;
        int n = this.number;

        List<GameObject> possibleMoves = new List<GameObject>();

        #region Get Every Possible Move
        
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
        
        #endregion
        
        // Get roque moves, if the king hasn't moved (the if statement is to avoid having an out of bounds exception at line 81 and 84)
        if (!HasMoved()) this.possibleRoqueMoves = GetRoqueMoves();
        
        this.possibleMovePositions = possibleMoves;
    }

    public void MakeRoqueMoveAt(GameObject position) {
        int l = position.GetComponent<Position>().GetLetterIndex();
        int n = position.GetComponent<Position>().GetNumber();

        GameObject positionOfRightRook = chessBoard.GetComponent<ChessBoard>().GetPositionAt(l + 1, n);
        GameObject rightRook = positionOfRightRook.GetComponent<Position>().GetPiece();

        GameObject positionOfLeftRook = chessBoard.GetComponent<ChessBoard>().GetPositionAt(l - 2, n);
        GameObject leftRook = positionOfLeftRook.GetComponent<Position>().GetPiece();

        // Is this a roque move to the right?
        if (rightRook != null) {
            // Move king to the position
            MoveToPostion(position);
            // Move rook to the left of the position
            rightRook.GetComponent<Piece>().MoveToPostion(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l - 1, n));
        }
        // If not then this is a roque to the left
        else if (leftRook != null) {
            MoveToPostion(position);
            // Move rook to the right of the position
            leftRook.GetComponent<Piece>().MoveToPostion(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l + 1, n));
        }
    }

    public List<GameObject> GetRoqueMoves() {
        GameObject positionOfRightRook = chessBoard.GetComponent<ChessBoard>().GetPositionAt(this.letterIndex + 3, this.number);
        GameObject rightRook = positionOfRightRook.GetComponent<Position>().GetPiece();

        GameObject positionOfLeftRook = chessBoard.GetComponent<ChessBoard>().GetPositionAt(this.letterIndex - 4, this.number);
        GameObject leftRook = positionOfLeftRook.GetComponent<Position>().GetPiece();

        List<GameObject> possibleRoqueMoves = new List<GameObject>();

        // If king hansn't moved
        // And if the right/left rook has not moved and there are no pieces in between them and the king
        // And is not in check
            // Return the possible positions
        if (!HasMoved() && !InCheck()) {
            if (rightRook != null
                && !rightRook.GetComponent<Piece>().HasMoved()
                && CheckForPieceAtDirection(1, 0, rightRook)) {
                    GameObject rockPosition = chessBoard.GetComponent<ChessBoard>().GetPositionAt(this.letterIndex + 2, this.number);
                    possibleRoqueMoves.Add(rockPosition);  // Adds 2 positions to the right of the King
            }
            if (leftRook != null
                && !leftRook.GetComponent<Piece>().HasMoved()
                && CheckForPieceAtDirection(-1, 0, leftRook)) {
                    GameObject rockPosition = chessBoard.GetComponent<ChessBoard>().GetPositionAt(this.letterIndex - 2, this.number);
                    possibleRoqueMoves.Add(rockPosition);  // Adds 2 positions to the left of the King
            }
        }
        return possibleRoqueMoves;
    }

    public void LimitKingMoves() {
        List<GameObject> enemyMoves = new List<GameObject>();

        if (gameObject.tag == "Player1") {
            enemyMoves = chessBoard.GetComponent<ChessBoard>().GetAllPossibleMovesOfPlayer("Player2");
        }
        else {
            enemyMoves = chessBoard.GetComponent<ChessBoard>().GetAllPossibleMovesOfPlayer("Player1");
        }

        this.possibleMovePositions = this.possibleMovePositions.Except(enemyMoves).ToList();
    }

    public bool InCheck() {
        // Check if the position of the king is inside the list of the possible positions of the enemy player
        string enemyPlayer;
        if (gameObject.CompareTag("Player1")) enemyPlayer = "Player2";
        else                                  enemyPlayer = "Player1";

        List<GameObject> possibleEnemyPositions = chessBoard.GetComponent<ChessBoard>().GetAllPossibleMovesOfPlayer(enemyPlayer);
        return possibleEnemyPositions.Contains(this.GetCurrentPosition());
    }
}
