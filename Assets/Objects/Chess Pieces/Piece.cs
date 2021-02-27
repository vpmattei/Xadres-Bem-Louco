using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Piece : MonoBehaviour {

    [SerializeField] protected GameObject gameCoordinator;
    [SerializeField] protected GameObject chessBoard;

    protected char letter;
    protected int letterIndex;
    protected int number;
    protected bool hasMoved = false;
    protected bool dead = false;

    [SerializeField] protected bool isSelected;

    [SerializeField] protected List<GameObject> possibleMovePositions;
    [SerializeField] protected GameObject currentPosition = null;

    void Start() {
        gameCoordinator = GameObject.Find("Game Coordinator");  // Finds the game coordinator so we can tell it to change the current phase
        chessBoard = GameObject.Find("Chess Board");
        SetPossibleMoves();
        //Debug.Log(gameObject + " can move ? " + CanMoveAt(letterIndex, number + 1));
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
    /// Method <c>MoveToPosition</c> Assigns and moves the piece to a new position,  and sets <c>Piece.hasMoved</c> to true
    ///</summary>
    public void MoveToPostion(GameObject position) {
        GameObject pieceAtPosition = position.GetComponent<Position>().GetPiece();
        //if (pieceAtPosition != null && pieceAtPosition != this.gameObject) pieceAtPosition.GetComponent<Piece>().Die();   // If there is a piece at the position we kill it

        this.currentPosition.GetComponent<Position>().RemovePiece();        // Remove the piece from the current position
        AssignPosition(position);                                           // Assigns the piece to the new position
        this.gameObject.transform.position = position.transform.position;   // Changes the "physical position" of the piece to the position passed

        // Next Phase
        //gameCoordinator.GetComponent<GameCoordinator>().NextGamePhase();

        hasMoved = true;
    }

    public void Moved(bool hasMoved) {
        this.hasMoved = hasMoved;
    }

    public bool HasMoved() {
        return this.hasMoved;
    }

    public void Die() {
        Debug.Log(gameObject + "Dead");
        Destroy(gameObject);
        /* this.currentPosition.GetComponent<Position>().RemovePiece();
        // Play dying animation
        this.dead = true;
        chessBoard.GetComponent<ChessBoard>().AddPieceToPlayerGraveyard(gameObject.tag, gameObject);
        gameObject.SetActive(false);
        //Destroy(this.gameObject); */
    }

    public void ReviveAt(GameObject pos) {
        AssignPosition(pos);

        this.dead = false;
        chessBoard.GetComponent<ChessBoard>().RevivePieceFromGraveyard(gameObject.tag, gameObject);
        gameObject.SetActive(true);
    }

    public GameObject GetCurrentPosition() {
        return this.currentPosition;
    }

    public abstract void SetPossibleMoves();

    public void SetPossibleMovesAs(List<GameObject> newMoves) {
        this.possibleMovePositions = newMoves;
    }

    public List<GameObject> GetPossibleMoves() {
        return this.possibleMovePositions;
    }

    public bool IsChecking() {
        List<GameObject> enemyPieces;
        GameObject enemyKingPosition = new GameObject();

        if (gameObject.tag == "Player1") {
            enemyPieces = chessBoard.GetComponent<ChessBoard>().GetPiecesFrom("Player2");
        }
        else {
            enemyPieces = chessBoard.GetComponent<ChessBoard>().GetPiecesFrom("Player1");
        } 
        
        foreach (GameObject piece in enemyPieces) {
            if (piece != null && piece.GetComponent<King>() != null) {
                enemyKingPosition = piece.GetComponent<King>().GetCurrentPosition();
            }
        }

        return this.possibleMovePositions.Contains(enemyKingPosition);
    }

    /// <summary>
    /// Method <c>RemoveMovesWhenChecked</c> Only keeps the moves that intersect with the ones of the piece checking and/or the ones that would kill the piece
    /// </summary>
    public void RemoveMovesWhenChecked() {
        List<GameObject> enemyPieces;
        List<GameObject> enemyPiecesChecking = new List<GameObject>();
        List<GameObject> pieceCheckingMoves = new List<GameObject>();
        GameObject enemyKingPosition = new GameObject();
        List<GameObject> movesToKeep = new List<GameObject>();

        if (gameObject.tag == "Player1") {
            enemyPieces = chessBoard.GetComponent<ChessBoard>().GetPiecesFrom("Player2");
        }
        else {
            enemyPieces = chessBoard.GetComponent<ChessBoard>().GetPiecesFrom("Player1");
        }

        foreach (GameObject piece in enemyPieces) {
            if (piece.GetComponent<Piece>().IsChecking()) {
                enemyPiecesChecking.Add(piece);
                pieceCheckingMoves.AddRange(piece.GetComponent<Piece>().GetPossibleMoves());
                movesToKeep.Add(piece.GetComponent<Piece>().GetCurrentPosition());
            }
        }

        // If there are 2 or more pieces checking the king, then only the king can move
        if (enemyPiecesChecking.Count() >= 2) {
            this.possibleMovePositions.Clear();
        }
        // We only keep the moves that match the ones from the pieces cheking and its position
        else {
            movesToKeep.AddRange(pieceCheckingMoves);
            this.possibleMovePositions = this.possibleMovePositions.Intersect(movesToKeep).ToList();
        }
    }

    /// <summary>
    /// Method <c>RemoveSelfCheckMoves</c> removes moves that would otherwise result in a check for the player
    ///</summary>
    public void RemoveSelfCheckMoves() {
        List<GameObject> movesToRemove = new List<GameObject>();                                                    // Moves to remove
        List<GameObject> piecesKilledAtPosition = new List<GameObject>();                                           // Pieces at one of the possible moves
        string player = gameObject.tag;                                                                             // Current player ("Player1" | "Player2")
        string enemyPlayer;
        List<GameObject> playerMoves = chessBoard.GetComponent<ChessBoard>().GetAllPossibleMovesOfPlayer(player);   // Moves of current player
        GameObject originalPosition = this.currentPosition;                                                         // The original position of the piece

        if (gameObject.tag == "Player1") enemyPlayer = "Player2";
        else                             enemyPlayer = "Player1";

        // 1 - We go through every possible position
        foreach(GameObject pos in this.possibleMovePositions) {
            piecesKilledAtPosition.Add(pos.GetComponent<Position>().GetPiece());
            if (pos.GetComponent<Position>().GetPiece() != null) {
                pos.GetComponent<Position>().GetPiece().SetActive(false);
            }
            MoveToPostion(pos);
            chessBoard.GetComponent<ChessBoard>().UpdatePossibleMovesOfPlayer(enemyPlayer);

            // 2 - And we remove the move that would result in a check
            if (chessBoard.GetComponent<ChessBoard>().IsGameInCheck()) {
                // If the game is still in Check
                // Then we add this position to the movesToRemove list
                movesToRemove.Add(pos);
            }
            
        }

        // 3 - We then remove the ones that would result in a check

        // From the piece
        this.possibleMovePositions = this.possibleMovePositions.Except(movesToRemove).ToList();

        // From the chess board
        playerMoves = playerMoves.Except(movesToRemove).ToList();
        chessBoard.GetComponent<ChessBoard>().SetPossibleMovesOfPlayer(player, playerMoves);

        // 4 - Finally, we move the piece back to its original position
        MoveToPostion(originalPosition);
        this.hasMoved = false;
        
        // If there was a piece at the position, then we revive it
        foreach (GameObject pieceKilled in piecesKilledAtPosition) {
            if (pieceKilled != null) {
                Debug.Log(pieceKilled);
                pieceKilled.SetActive(true);
                pieceKilled.GetComponent<Piece>().AssignPosition(pieceKilled.GetComponent<Piece>().GetCurrentPosition());
            }
        }

        // And re-update the possible moves of the enemy player
        chessBoard.GetComponent<ChessBoard>().UpdatePossibleMovesOfPlayer(enemyPlayer);
    }

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

    /// <summary>
    /// Method <c>CheckForPieceAtDirection</c> checks if there are no pieces in between the current piece and the piece passed in parameter
    /// </summary>
    protected bool CheckForPieceAtDirection(int horizontalDirection, int verticalDirection, GameObject piece) {
        int l = this.letterIndex + 1*horizontalDirection;
        int n = this.number + 1*verticalDirection;

        while (CanMoveAt(l, n)) {
            l = l + 1*horizontalDirection;
            n = n + 1*verticalDirection;
        }
        if (IsPieceAt(l, n, piece)) {
            return true;
        }
        else {
            return false;
        }
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

    private bool IsPieceAt(int l, int n, GameObject piece) {
        if (l > 7 || l < 0 || n > 7 || n < 0) {
            return false;
        }
        else {
            GameObject positionAtIndex = chessBoard.GetComponent<ChessBoard>().GetPositionAt(l, n);
            GameObject pieceAtIndex = positionAtIndex.GetComponent<Position>().GetPiece();

            return piece == pieceAtIndex;
        }
    }
}
