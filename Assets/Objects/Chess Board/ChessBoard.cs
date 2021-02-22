using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour {

    private static readonly char[] LETTERS = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'};
    private static readonly int[] NUMBERS = {1, 2, 3, 4, 5, 6, 7, 8};

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

    [SerializeField] private List<GameObject> pieces;
    [SerializeField] private GameObject pieceSelected;
    [SerializeField] private List<GameObject> possibleMovesOfSelectedPiece;

    // TO-DO List of possible moves of every piece

    [SerializeField] private float spawnPositionDistance = 0.5f;


    void Awake() {
        FillBoardWithPositions();

        FillPositionsWithPieces();
    }

    void Start() {
        UnselectPosition();       // No position selected at start
    }

    private void FillBoardWithPositions() {
        for(int n = 0; n < 8; n++) {            // for 1...8
            for(int l = 0; l < 8; l++) {        // for A...H
                // Spawn positionPrefab instances and set their reference to position
                GameObject position = Instantiate(positionPrefab, this.gameObject.transform.position + new Vector3(l + spawnPositionDistance, 0, n + spawnPositionDistance), positionPrefab.transform.rotation) as GameObject;
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
                }
                if (n == 0) {
                    // Fill Rooks
                    if (l == 0 || l == 7) {
                        GameObject rookSpawned = Instantiate(whiteRookPrefab, positions[l,n].transform.position, whiteRookPrefab.transform.rotation);
                        rookSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(rookSpawned);
                    }
                    // Fill Knights
                    if (l == 1 || l == 6) {
                        GameObject knightSpawned = Instantiate(whiteKnightPrefab, positions[l,n].transform.position, whiteKnightPrefab.transform.rotation);
                        knightSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(knightSpawned);
                    }
                    // Fill Bishops
                    if (l == 2 || l == 5) {
                        GameObject bishopSpawned = Instantiate(whiteBishopPrefab, positions[l,n].transform.position, whiteBishopPrefab.transform.rotation);
                        bishopSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(bishopSpawned);
                    }
                    // Fill Queen
                    if (l == 3) {
                        GameObject queenSpawned = Instantiate(whiteQueenPrefab, positions[l,n].transform.position, whiteQueenPrefab.transform.rotation);
                        queenSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(queenSpawned);
                    }
                    // Fill King
                    if (l == 4) {
                        GameObject kingSpawned = Instantiate(whiteKingPrefab, positions[l,n].transform.position, whiteKingPrefab.transform.rotation);
                        kingSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(kingSpawned);
                    }
                }
                #endregion

                #region Fill Black Pieces
                if (n == 6) {
                    // Fill Pawns
                    GameObject pawnSpawned = Instantiate(blackPawnPrefab, positions[l,n].transform.position, blackPawnPrefab.transform.rotation);
                    pawnSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                    pieces.Add(pawnSpawned);
                }
                if (n == 7) {
                    // Fill Rooks
                    if (l == 0 || l == 7) {
                        GameObject rookSpawned = Instantiate(blackRookPrefab, positions[l,n].transform.position, blackRookPrefab.transform.rotation);
                        rookSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(rookSpawned);
                    }
                    // Fill Knights
                    if (l == 1 || l == 6) {
                        GameObject knightSpawned = Instantiate(blackKnightPrefab, positions[l,n].transform.position, blackKnightPrefab.transform.rotation);
                        knightSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(knightSpawned);
                    }
                    // Fill Bishops
                    if (l == 2 || l == 5) {
                        GameObject bishopSpawned = Instantiate(blackBishopPrefab, positions[l,n].transform.position, blackBishopPrefab.transform.rotation);
                        bishopSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(bishopSpawned);
                    }
                    // Fill Queen
                    if (l == 3) {
                        GameObject queenSpawned = Instantiate(blackQueenPrefab, positions[l,n].transform.position, blackQueenPrefab.transform.rotation);
                        queenSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(queenSpawned);
                    }
                    // Fill King
                    if (l == 4) {
                        GameObject kingSpawned = Instantiate(blackKingPrefab, positions[l,n].transform.position, blackKingPrefab.transform.rotation);
                        kingSpawned.GetComponent<Piece>().AssignPosition(positions[l,n]);
                        pieces.Add(kingSpawned);
                    }
                }
                #endregion
            }
        }
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
        possibleMovesOfSelectedPiece = ShowPossibleMovesOfPiece(piece);                                 // Shows the possible moves
    }

    public void UnselectPiece() {
        if(this.pieceSelected != null) this.pieceSelected.GetComponent<Piece>().SetSelectionTo(false);
        this.pieceSelected = null;
        UnselectPosition();                                                                             // Unselects the position where the piece is placed at
        HidePossibleMovesOfSelectedPiece();
    }

    private List<GameObject> ShowPossibleMovesOfPiece(GameObject piece) {
        List<GameObject> possibleMoves = new List<GameObject>();
        possibleMoves = piece.GetComponent<Piece>().GetPossibleMoves();
        
        foreach (GameObject pos in possibleMoves) {
            if(pos.GetComponent<Position>().GetPiece() == null) {
                pos.GetComponent<Position>().SetPossibleMoveTo(true);
            }
            else {
                pos.GetComponent<Position>().SetPossibleAttackMoveTo(true);
            }
        }
        return possibleMoves;
    }

    private void HidePossibleMovesOfSelectedPiece() {
        foreach (GameObject pos in possibleMovesOfSelectedPiece) {
            if(pos.GetComponent<Position>().GetPiece() == null) {
                pos.GetComponent<Position>().SetPossibleMoveTo(false);
            }
            else {
                pos.GetComponent<Position>().SetPossibleAttackMoveTo(false);
            }
        }
    }

    public List<GameObject> GetPossibleMovesOfSelectedPiece() {
        return this.possibleMovesOfSelectedPiece;
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
}
