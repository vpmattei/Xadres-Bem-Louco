using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour {

    [SerializeField] protected GameObject gameCoordinator;
    [SerializeField] protected GameObject chessBoard;

    protected char letter;
    protected int letterIndex;
    protected int number;
    protected bool hasMoved = false;

    [SerializeField] protected bool isSelected;

    [SerializeField] protected List<GameObject> possibleMovePositions;
    [SerializeField] protected GameObject currentPosition = null;

    void Start() {
        gameCoordinator = GameObject.Find("Game Coordinator");  // Finds the game coordinator so we can tell it to change the current phase
        chessBoard = GameObject.Find("Chess Board");
        //Debug.Log(gameObject + " can move ? " + CanMoveAt(letterIndex, number + 1));
    }

    void Update() {
        this.possibleMovePositions = GetPossibleMoves();
    }

    public abstract void SetName(string letter, int number);

    public void SetPositions(char letter, int letterIndex, int number) {
        this.letter = letter;
        this.letterIndex = letterIndex;
        this.number = number;
    }

    public void SetSelectionTo(bool isSelected) {
        this.isSelected = isSelected;
    }

    ///<summary>
    /// Method <c>AssignPosition</c> Assigns the current position reference for the piece and the piece reference for the position
    ///</summary>
    public void AssignPosition(GameObject position) {
        position.GetComponent<Position>().AssignPiece(this.gameObject); // Assigns the piece to the new position
        this.currentPosition = position;
    }

    ///<summary>
    /// Method <c>MoveToPosition</c> Assigns and moves the piece to a new position, goes to the next game phase and sets <c>Pawn.hasMoved</c> to true
    ///</summary>
    public void MoveToPostion(GameObject position) {
        GameObject pieceAtPosition = position.GetComponent<Position>().GetPiece();
        if (pieceAtPosition != null) pieceAtPosition.GetComponent<Piece>().Die();   // If there is a piece at the position we kill it

        this.currentPosition.GetComponent<Position>().RemovePiece();        // Remove the piece from the current position
        AssignPosition(position);                                           // Assigns the piece to the new position
        this.gameObject.transform.position = position.transform.position;   // Changes the "physical position" of the piece to the position passed

        // Next Phase
        gameCoordinator.GetComponent<GameCoordinator>().NextGamePhase();

        hasMoved = true;
    }

    public void Die() {
        this.currentPosition.GetComponent<Position>().RemovePiece();
        // Play dying animation
        Destroy(this.gameObject);
    }

    public GameObject GetCurrentPosition() {
        return this.currentPosition;
    }

    public abstract List<GameObject> GetPossibleMoves();

    /// <summary>
    /// Method <c>GetPossibleMovesAtDirection</c> returns the possible move positions at the given direction given <c>horizontalDirection</c>, <c>verticalDirection</c>
    /// </summary>
    protected List<GameObject> GetPossibleMovesAtDirection(int horizontalDirection, int verticalDirection) {
        int l = this.letterIndex + 1*horizontalDirection;
        int n = this.number + 1*verticalDirection;

        List<GameObject> possibleMoves = new List<GameObject>();

        while (CanMoveAt(l, n)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l, n));
            l = l + 1*horizontalDirection;
            n = n + 1*verticalDirection;
        }
        if (IsEnemyAt(l, n)) {
            possibleMoves.Add(chessBoard.GetComponent<ChessBoard>().GetPositionAt(l, n));
        }

        return possibleMoves;
    }

    ///<summary>
    /// Method <c>CanMoveAt</c> Returns true if the position at the index[l,n] is empty or has an enemy piece
    ///</summary>
    public bool CanMoveAt(int l, int n) {
        if (l > 7 || l < 0 || n > 7 || n < 0) {
            return false;
        }
        else {
            GameObject positionAtIndex = chessBoard.GetComponent<ChessBoard>().GetPositionAt(l,n);
            GameObject pieceAtIndex = positionAtIndex.GetComponent<Position>().GetPiece();

            return (pieceAtIndex == null); // Empty positions
        }
    }

    public bool IsEnemyAt(int l, int n) {
        if (l > 7 || l < 0 || n > 7 || n < 0) {
            return false;
        }
        else {
            GameObject positionAtIndex = chessBoard.GetComponent<ChessBoard>().GetPositionAt(l,n);
            GameObject pieceAtIndex = positionAtIndex.GetComponent<Position>().GetPiece();

            return pieceAtIndex != null && !this.gameObject.CompareTag(pieceAtIndex.tag);
        }
    }
}
