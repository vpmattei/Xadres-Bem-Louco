using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessTable : MonoBehaviour {

    private static readonly char[] LETTERS = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'};
    private static readonly int[] NUMBERS = {1, 2, 3, 4, 5, 6, 7, 8};

    [SerializeField] private GameObject positionPrefab;
    [SerializeField] private GameObject piecePrefab;

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
            // fill white pieces
            if (pos.GetComponent<Position>().GetNumber() == 1 || pos.GetComponent<Position>().GetNumber() == 2) {
                GameObject pieceSpawned = Instantiate(piecePrefab, pos.transform.position, piecePrefab.transform.rotation);
                pieceSpawned.GetComponent<Piece>().PlacePieceAt(pos);
                pieces.Add(pieceSpawned);
                //pos.GetComponent<Position>().SpawnPiece(piecePrefab);
            }
            // fill black pieces
            if (pos.GetComponent<Position>().GetNumber() == 7 || pos.GetComponent<Position>().GetNumber() == 8) {
                GameObject pieceSpawned = Instantiate(piecePrefab, pos.transform.position, piecePrefab.transform.rotation);
                pieceSpawned.GetComponent<Piece>().PlacePieceAt(pos);
                pieces.Add(pieceSpawned);
                //pos.GetComponent<Position>().SpawnPiece(piecePrefab);
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
    }

    public void UnselectPiece() {
        if(this.pieceSelected != null) this.pieceSelected.GetComponent<Piece>().SetSelectionTo(false);
        this.pieceSelected = null;
        UnselectPosition();                                                                             // Deselects the position where the piece is placed at
    }

}
