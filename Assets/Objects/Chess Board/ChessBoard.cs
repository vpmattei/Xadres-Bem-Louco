using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChessBoard : MonoBehaviour {

    private static readonly char[] LETTERS = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'};
    private static readonly int[] NUMBERS = {1, 2, 3, 4, 5, 6, 7, 8};

    private GameObject gameCoordinator;

    [SerializeField] private GameObject positionPrefab;

    #region White Pieces Prefabs
        [SerializeField] private GameObject whitePawnPrefab;
        [SerializeField] private GameObject whiteRookPrefab;
        [SerializeField] private GameObject whiteKnightPrefab;
        [SerializeField] private GameObject whiteBishopPrefab;
        [SerializeField] private GameObject whiteQueenPrefab;
        [SerializeField] private GameObject whiteKingPrefab;
    #endregion

    #region Black Pieces Prefabs
        [SerializeField] private GameObject blackPawnPrefab;
        [SerializeField] private GameObject blackRookPrefab;
        [SerializeField] private GameObject blackKnightPrefab;
        [SerializeField] private GameObject blackBishopPrefab;
        [SerializeField] private GameObject blackQueenPrefab;
        [SerializeField] private GameObject blackKingPrefab;
    #endregion

    [SerializeField] private GameObject[,] positions = new GameObject[8,8];
    [SerializeField] private GameObject positionSelected;

    [SerializeField] private GameObject pieceSelected;
    [SerializeField] private List<GameObject> pieces;
    [SerializeField] private List<GameObject> player1Pieces;
    [SerializeField] private List<GameObject> player2Pieces;
    [SerializeField] private List<GameObject> player1DeadPieces = new List<GameObject>();
    [SerializeField] private List<GameObject> player2DeadPieces = new List<GameObject>();
    [SerializeField] private List<GameObject> player1Moves;
    [SerializeField] private List<GameObject> player2Moves;
    [SerializeField] private List<GameObject> possibleMovesOfSelectedPiece;
    [SerializeField] private List<GameObject> possibleRoqueMovesOfSelectedPiece;

    [SerializeField] private float spawnPositionDistance = 0.5f;


    void Awake() {
        FillBoardWithPositions();

        FillPositionsWithPieces();
    }

    void Start() {
        UnselectPosition();       // No position selected at start

        gameCoordinator = GameObject.Find("Game Coordinator");
    }

    private void FillBoardWithPositions() {
        for(int n = 0; n < 8; n++) {            // for 1...8
            for(int l = 0; l < 8; l++) {        // for A...H
                // Spawn positionPrefab instances and set their reference to position
                GameObject position = Instantiate(positionPrefab, this.gameObject.transform.position + new Vector3(l + spawnPositionDistance, 0.01f, n + spawnPositionDistance), positionPrefab.transform.rotation) as GameObject;
                position.transform.parent = gameObject.transform;   // Making each position a child of ChessTable

                // Set their positions i.e(e,3) and give the gameObjects in the scene a name(ex: Position(E,3))
                position.GetComponent<Position>().SetPositions(LETTERS[l], l, n);
                position.name = position.GetComponent<Position>().PositionToString();

                // Add the position to the positions list
                positions[l,n] = position;
            }
        }
    }

    private void FillPositionsWithPieces() {
        for(int n = 0; n < 8; n++) {            // for 1...8
            for(int l = 0; l < 8; l++) {        // for A...H
                #region Fill White Pieces
                if (n == 1) {
                    // Fill Pawns
                    GameObject pawnSpawned = Instantiate(whitePawnPrefab, positions[l,n].transform.position, whitePawnPrefab.transform.rotation);
                    pawnSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                    pieces.Add(pawnSpawned);
                    player1Pieces.Add(pawnSpawned);
                }
                if (n == 0) {
                    // Fill Rooks
                    if (l == 0 || l == 7) {
                        GameObject rookSpawned = Instantiate(whiteRookPrefab, positions[l,n].transform.position, whiteRookPrefab.transform.rotation);
                        rookSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(rookSpawned);
                        player1Pieces.Add(rookSpawned);
                    }
                    // Fill Knights
                    if (l == 1 || l == 6) {
                        GameObject knightSpawned = Instantiate(whiteKnightPrefab, positions[l,n].transform.position, whiteKnightPrefab.transform.rotation);
                        knightSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(knightSpawned);
                        player1Pieces.Add(knightSpawned);
                    }
                    // Fill Bishops
                    if (l == 2 || l == 5) {
                        GameObject bishopSpawned = Instantiate(whiteBishopPrefab, positions[l,n].transform.position, whiteBishopPrefab.transform.rotation);
                        bishopSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(bishopSpawned);
                        player1Pieces.Add(bishopSpawned);
                    }
                    // Fill Queen
                    if (l == 3) {
                        GameObject queenSpawned = Instantiate(whiteQueenPrefab, positions[l,n].transform.position, whiteQueenPrefab.transform.rotation);
                        queenSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(queenSpawned);
                        player1Pieces.Add(queenSpawned);
                    }
                    // Fill King
                    if (l == 4) {
                        GameObject kingSpawned = Instantiate(whiteKingPrefab, positions[l,n].transform.position, whiteKingPrefab.transform.rotation);
                        kingSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(kingSpawned);
                        player1Pieces.Add(kingSpawned);
                    }
                }
                #endregion

                #region Fill Black Pieces
                if (n == 6) {
                    // Fill Pawns
                    GameObject pawnSpawned = Instantiate(blackPawnPrefab, positions[l,n].transform.position, blackPawnPrefab.transform.rotation);
                    pawnSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                    pieces.Add(pawnSpawned);
                    player2Pieces.Add(pawnSpawned);
                }
                if (n == 7) {
                    // Fill Rooks
                    if (l == 0 || l == 7) {
                        GameObject rookSpawned = Instantiate(blackRookPrefab, positions[l,n].transform.position, blackRookPrefab.transform.rotation);
                        rookSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(rookSpawned);
                        player2Pieces.Add(rookSpawned);
                    }
                    // Fill Knights
                    if (l == 1 || l == 6) {
                        GameObject knightSpawned = Instantiate(blackKnightPrefab, positions[l,n].transform.position, blackKnightPrefab.transform.rotation);
                        knightSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(knightSpawned);
                        player2Pieces.Add(knightSpawned);
                    }
                    // Fill Bishops
                    if (l == 2 || l == 5) {
                        GameObject bishopSpawned = Instantiate(blackBishopPrefab, positions[l,n].transform.position, blackBishopPrefab.transform.rotation);
                        bishopSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(bishopSpawned);
                        player2Pieces.Add(bishopSpawned);
                    }
                    // Fill Queen
                    if (l == 3) {
                        GameObject queenSpawned = Instantiate(blackQueenPrefab, positions[l,n].transform.position, blackQueenPrefab.transform.rotation);
                        queenSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(queenSpawned);
                        player2Pieces.Add(queenSpawned);
                    }
                    // Fill King
                    if (l == 4) {
                        GameObject kingSpawned = Instantiate(blackKingPrefab, positions[l,n].transform.position, blackKingPrefab.transform.rotation);
                        kingSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(kingSpawned);
                        player2Pieces.Add(kingSpawned);
                    }
                }
                #endregion
            }
        }
    }

    #region Methods for Check State
    public bool IsGameInCheck() {
        List<GameObject> playerCheckedPieces;
        King king;

        if (gameCoordinator.GetComponent<GameCoordinator>().GetCurrentTurn().ToString() == "Player1") {
            playerCheckedPieces = player1Pieces;
        }
        else {
            playerCheckedPieces = player2Pieces;
        }

        foreach (GameObject piece in playerCheckedPieces) {
            if (piece != null) {
                king = piece.GetComponent<King>();
                if (king != null && king.InCheck()) {
                    return true;
                }
            }
        }
        return false;
    }

    public bool isGameInCheckMate() {
        return (player1Moves.Count() == 0 || player2Moves.Count() == 0 );
    }

    public void LimitPlayerMovesWhenChecked() {
        /* List<GameObject> playerCheckedPieces;

        if (gameCoordinator.GetComponent<GameCoordinator>().GetCurrentTurn().ToString() == "Player1") {
            playerCheckedPieces = player1Pieces;
        }
        else {
            playerCheckedPieces = player2Pieces;
        }

        foreach (GameObject piece in playerCheckedPieces) {
            if (piece!= null) {
                if (piece.GetComponent<King>() != null) {
                    piece.GetComponent<King>().LimitKingMoves();
                }
                else {
                    piece.GetComponent<Piece>().RemoveMovesWhenChecked();
                }
                piece.GetComponent<Piece>().RemoveSelfCheckMoves();
            }
        } */

        List<GameObject> playerCheckedPieces;
        List<GameObject> playerCheckedMoves;
        List<GameObject> possibleMovesOfEachPiece;
        List<GameObject> movesToRemoveOfEachPiece = new List<GameObject>();
        GameObject originalPositionOfPiece;
        string enemyPlayer;

        if (gameCoordinator.GetComponent<GameCoordinator>().GetCurrentTurn().ToString() == "Player1") {
            playerCheckedPieces = player1Pieces;
            playerCheckedMoves = player1Moves;
            enemyPlayer = "Player2";
        }
        else {
            playerCheckedPieces = player2Pieces;
            playerCheckedMoves = player2Moves;
            enemyPlayer = "Player1";
        }

        foreach (GameObject piece in playerCheckedPieces) {
            possibleMovesOfEachPiece = piece.GetComponent<Piece>().GetPossibleMoves();
            originalPositionOfPiece = piece.GetComponent<Piece>().GetCurrentPosition();

            foreach(GameObject pos in piece.GetComponent<Piece>().GetPossibleMoves()) {
                piece.GetComponent<Piece>().MoveToPostion(pos);
                UpdatePossibleMovesOfPlayer(enemyPlayer);
                if (IsGameInCheck()) {
                    Debug.Log("The Game is still in check if we move the piece : " + piece
                                + "\n to the position : " + pos);
                    // If the game is still in Check
                    // Then we add this position to the movesToRemove list
                    movesToRemoveOfEachPiece.Add(pos);
                }
            }
            // We remove all the positions of the movesToRemove list from the possibleMovesOfEachPiece list
            possibleMovesOfEachPiece = possibleMovesOfEachPiece.Except(movesToRemoveOfEachPiece).ToList();
            piece.GetComponent<Piece>().SetPossibleMovesAs(possibleMovesOfEachPiece);
            // And the playerCheckedMoves list
            playerCheckedMoves.AddRange(possibleMovesOfEachPiece);
            // And we update the player moves attribute
            if (gameCoordinator.GetComponent<GameCoordinator>().GetCurrentTurn().ToString() == "Player1") {
                player1Moves = playerCheckedMoves;
            }
            else {
                player2Moves = playerCheckedMoves;
            }
            movesToRemoveOfEachPiece.Clear();
            // We move the piece back to its original position
            piece.GetComponent<Piece>().MoveToPostion(originalPositionOfPiece);
            piece.GetComponent<Piece>().Moved(false);
            // And re-update the possible moves of the enemy player
            UpdatePossibleMovesOfPlayer(enemyPlayer);
        }
    }

    public void UpdatePossibleMovesOfPlayer(string enemyPlayer) {
        List<GameObject> enemyMoves = new List<GameObject>();
        List<GameObject> enemyPieces = new List<GameObject>();
        if (enemyPlayer == "Player1")  {
            enemyPieces = player1Pieces;
            enemyPieces = enemyPieces.Except(player1DeadPieces).ToList();
        }
        else {
            enemyPieces = player2Pieces;
            enemyPieces = enemyPieces.Except(player2DeadPieces).ToList();
        }

        foreach (GameObject piece in enemyPieces ) {
            if (piece!=null && piece.activeSelf != false) {
                piece.GetComponent<Piece>().SetPossibleMoves();
                enemyMoves.AddRange(piece.GetComponent<Piece>().GetPossibleMoves());
            }
        }

        if (enemyPlayer == "Player1") {
            this.player1Moves = enemyMoves;
        }
        else {
            this.player2Moves = enemyMoves;
        }
    }
    #endregion

    public void UpdatePossibleMovesOfPieces() {
        List<GameObject> player1Moves = new List<GameObject>();
        List<GameObject> player2Moves = new List<GameObject>();

        foreach (GameObject piece in pieces) {
            if (piece != null) {
                piece.GetComponent<Piece>().SetPossibleMoves();

                if (piece.CompareTag("Player1") && piece.activeSelf != false) {
                    player1Moves.AddRange(piece.GetComponent<Piece>().GetPossibleMoves());
                }
                else if (piece.CompareTag("Player2") && piece.activeSelf != false) {
                    player2Moves.AddRange(piece.GetComponent<Piece>().GetPossibleMoves());
                }
            }
        }

        this.player1Moves = player1Moves;
        this.player2Moves = player2Moves;
    }

    private List<GameObject> ShowPossibleMovesOfPiece(GameObject piece) {
        List<GameObject> possibleMoves = new List<GameObject>();
        possibleMoves = piece.GetComponent<Piece>().GetPossibleMoves();
        
        foreach (GameObject pos in possibleMoves) {
            if (pos.GetComponent<Position>().GetPiece() == null) {
                pos.GetComponent<Position>().SetPossibleMoveTo(true);
            }
            else if (pos.GetComponent<Position>().GetPiece() != null) {
                pos.GetComponent<Position>().SetPossibleAttackMoveTo(true);
            }
        }
        // Shows the possible roque moves (if there are any)
        this.possibleRoqueMovesOfSelectedPiece = ShowPossibleRoqueMovesOf(piece);
        return possibleMoves;
    }

    private void HidePossibleMovesOfSelectedPiece() {
        if (this.possibleMovesOfSelectedPiece != null) {
            // Normal moves/attack moves
            foreach (GameObject pos in possibleMovesOfSelectedPiece) {
                pos.GetComponent<Position>().SetPossibleMoveTo(false);
                pos.GetComponent<Position>().SetPossibleAttackMoveTo(false);
            }
            // Roque moves
            foreach (GameObject pos in possibleRoqueMovesOfSelectedPiece) {
                pos.GetComponent<Position>().SetPossibleRockMoveTo(false);
            }
            //this.possibleMovesOfSelectedPiece = null;
        }
    }

    private List<GameObject> ShowPossibleRoqueMovesOf(GameObject piece) {
        King kingPiece = piece.GetComponent<King>();

        List<GameObject> possibleRoqueMoves = new List<GameObject>();

        // If the piece is a king
            // possibleRoqueMoves <- kingPiece.GetRoqueMoves()
        if (kingPiece != null && !kingPiece.HasMoved()) {
            possibleRoqueMoves = kingPiece.GetRoqueMoves();
        }

        foreach (GameObject pos in possibleRoqueMoves) {
            pos.GetComponent<Position>().SetPossibleRockMoveTo(true);
        }
        return possibleRoqueMoves;
    }

    public void SetPossibleMovesOfPlayer(string player, List<GameObject> moves) {
        if (player == "Player1") {
            player1Moves = moves;
        }
        else if (player == "Player2") {
            player2Moves = moves;
        }
    }

    public List<GameObject> GetAllPossibleMovesOfPlayer(string player) {
        List<GameObject> possibleMoves = new List<GameObject>();
        if (player == "Player1") {
            possibleMoves = player1Moves;
        }
        else if (player == "Player2") {
            possibleMoves = player2Moves;
        }
        return possibleMoves;
    }

    public List<GameObject> GetPossibleMovesOfSelectedPiece() {
        return this.possibleMovesOfSelectedPiece;
    }

    public List<GameObject> GetPossibleRoqueMovesOfSelectedPiece() {
        return this.possibleRoqueMovesOfSelectedPiece;
    }
    public void SelectPosition(GameObject position) {
        this.positionSelected = position;
        this.positionSelected.GetComponent<Position>().SetSelectionTo(true);
    }

    public void UnselectPosition() {
        if(this.positionSelected != null) this.positionSelected.GetComponent<Position>().SetSelectionTo(false);
        this.positionSelected = null;
    }
    public void SelectPiece(GameObject piece) {
        this.pieceSelected = piece;
        this.pieceSelected.GetComponent<Piece>().SetSelectionTo(true);
        SelectPosition(this.pieceSelected.GetComponent<Piece>().GetCurrentPosition());                  // Selects the position where the piece is placed at
        //piece.GetComponent<Piece>().RemoveSelfCheckMoves();
        possibleMovesOfSelectedPiece = ShowPossibleMovesOfPiece(piece);                                 // Shows the possible moves
    }

    public void UnselectPiece() {
        if(this.pieceSelected != null) this.pieceSelected.GetComponent<Piece>().SetSelectionTo(false);
        this.pieceSelected = null;
        UnselectPosition();                                                                             // Unselects the position where the piece is placed at
        HidePossibleMovesOfSelectedPiece();
    }

    public List<GameObject> GetPiecesFrom(string player) {
        if (player == "Player1") return player1Pieces;
        else                     return player2Pieces;
    }

    public GameObject GetPieceSelected() {
        return this.pieceSelected;
    }

    public GameObject[,] GetPositions() {
        return this.positions;
    }

    public GameObject GetPositionAt(int l, int n) {
        return this.positions[l,n];
    }

    public void AddPieceToPlayerGraveyard(string player, GameObject piece) {
        if (player == "Player1") {
            player1DeadPieces.Add(piece);
        }
        else {
            player2DeadPieces.Add(piece);
        }
    }

    public void DestroyPiecesFromGraveyard() {
        foreach (GameObject piece in player1DeadPieces) {
            Destroy(piece);
        }
        foreach (GameObject piece in player2DeadPieces) {
            Destroy(piece);
        }
    }

    public void RevivePieceFromGraveyard(string player, GameObject piece) {
        if (player == "Player1") {
            player1DeadPieces.Remove(piece);
        }
        else {
            player2DeadPieces.Remove(piece);
        }
    }
}
