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

    [SerializeField] private List<GameObject> positions;
    [SerializeField] private GameObject positionSelected;

    [SerializeField] private List<GameObject> pieces;
    [SerializeField] private GameObject pieceSelected;

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
                position.GetComponent<Position>().SetPositions(LETTERS[l],NUMBERS[n]);
                position.name = position.GetComponent<Position>().PositionToString();

                // Add the position to the positions list
                positions.Add(position);
            }
        }
    }

    private void FillPositionsWithPieces() {
        foreach (GameObject pos in positions) {
            #region Fill White Pieces
            if (pos.GetComponent<Position>().GetNumber() == 2) {
                // Fill Pawns
                GameObject pawnSpawned = Instantiate(whitePawnPrefab, pos.transform.position, whitePawnPrefab.transform.rotation);
                pawnSpawned.GetComponent<Piece>().AssignPosition(pos);
                pieces.Add(pawnSpawned);
            }
            if (pos.GetComponent<Position>().GetNumber() == 1) {
                // Fill Rooks
                if (pos.GetComponent<Position>().GetLetter() == 'A' || pos.GetComponent<Position>().GetLetter() == 'H') {
                    GameObject rookSpawned = Instantiate(whiteRookPrefab, pos.transform.position, whiteRookPrefab.transform.rotation);
                    rookSpawned.GetComponent<Piece>().AssignPosition(pos);
                    pieces.Add(rookSpawned);
                }
                // Fill Knights
                if (pos.GetComponent<Position>().GetLetter() == 'B' || pos.GetComponent<Position>().GetLetter() == 'G') {
                    GameObject knightSpawned = Instantiate(whiteKnightPrefab, pos.transform.position, whiteKnightPrefab.transform.rotation);
                    knightSpawned.GetComponent<Piece>().AssignPosition(pos);
                    pieces.Add(knightSpawned);
                }
                // Fill Bishops
                if (pos.GetComponent<Position>().GetLetter() == 'C' || pos.GetComponent<Position>().GetLetter() == 'F') {
                    GameObject bishopSpawned = Instantiate(whiteBishopPrefab, pos.transform.position, whiteBishopPrefab.transform.rotation);
                    bishopSpawned.GetComponent<Piece>().AssignPosition(pos);
                    pieces.Add(bishopSpawned);
                }
                // Fill Queen
                if (pos.GetComponent<Position>().GetLetter() == 'D') {
                    GameObject queenSpawned = Instantiate(whiteQueenPrefab, pos.transform.position, whiteQueenPrefab.transform.rotation);
                    queenSpawned.GetComponent<Piece>().AssignPosition(pos);
                    pieces.Add(queenSpawned);
                }
                // Fill King
                if (pos.GetComponent<Position>().GetLetter() == 'E') {
                    GameObject kingSpawned = Instantiate(whiteKingPrefab, pos.transform.position, whiteKingPrefab.transform.rotation);
                    kingSpawned.GetComponent<Piece>().AssignPosition(pos);
                    pieces.Add(kingSpawned);
                }
            }
            #endregion

            #region Fill Black Pieces
            if (pos.GetComponent<Position>().GetNumber() == 7) {
                // Fill Pawns
                GameObject pawnSpawned = Instantiate(blackPawnPrefab, pos.transform.position, blackPawnPrefab.transform.rotation);
                pawnSpawned.GetComponent<Piece>().AssignPosition(pos);
                pieces.Add(pawnSpawned);
            }
            if (pos.GetComponent<Position>().GetNumber() == 8) {
                // Fill Rooks
                if (pos.GetComponent<Position>().GetLetter() == 'A' || pos.GetComponent<Position>().GetLetter() == 'H') {
                    GameObject rookSpawned = Instantiate(blackRookPrefab, pos.transform.position, blackRookPrefab.transform.rotation);
                    rookSpawned.GetComponent<Piece>().AssignPosition(pos);
                    pieces.Add(rookSpawned);
                }
                // Fill Knights
                if (pos.GetComponent<Position>().GetLetter() == 'B' || pos.GetComponent<Position>().GetLetter() == 'G') {
                    GameObject knightSpawned = Instantiate(blackKnightPrefab, pos.transform.position, blackKnightPrefab.transform.rotation);
                    knightSpawned.GetComponent<Piece>().AssignPosition(pos);
                    pieces.Add(knightSpawned);
                }
                // Fill Bishops
                if (pos.GetComponent<Position>().GetLetter() == 'C' || pos.GetComponent<Position>().GetLetter() == 'F') {
                    GameObject bishopSpawned = Instantiate(blackBishopPrefab, pos.transform.position, blackBishopPrefab.transform.rotation);
                    bishopSpawned.GetComponent<Piece>().AssignPosition(pos);
                    pieces.Add(bishopSpawned);
                }
                // Fill Queen
                if (pos.GetComponent<Position>().GetLetter() == 'D') {
                    GameObject queenSpawned = Instantiate(blackQueenPrefab, pos.transform.position, blackQueenPrefab.transform.rotation);
                    queenSpawned.GetComponent<Piece>().AssignPosition(pos);
                    pieces.Add(queenSpawned);
                }
                // Fill King
                if (pos.GetComponent<Position>().GetLetter() == 'E') {
                    GameObject kingSpawned = Instantiate(blackKingPrefab, pos.transform.position, blackKingPrefab.transform.rotation);
                    kingSpawned.GetComponent<Piece>().AssignPosition(pos);
                    pieces.Add(kingSpawned);
                }
            }
            #endregion
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
    }

    public void UnselectPiece() {
        if(this.pieceSelected != null) this.pieceSelected.GetComponent<Piece>().SetSelectionTo(false);
        this.pieceSelected = null;
        UnselectPosition();                                                                             // Unselects the position where the piece is placed at
    }

    public GameObject GetPieceSelected() {
        return this.pieceSelected;
    }
}
