using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessTable : MonoBehaviour {

    private static readonly char[] LETTERS = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'};
    private static readonly int[] NUMBERS = {1, 2, 3, 4, 5, 6, 7, 8};

    private static readonly string[] TURN = {"Player1","Player2"};

    [SerializeField] private GameObject positionPrefab;
    [SerializeField] private List<GameObject> positions;
    [SerializeField] private GameObject positionSelected;
    [SerializeField] private float spawnPositionDistance = 0.5f;

    [SerializeField] private GameObject piecePrefab;

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
                position.transform.parent = gameObject.transform;

                // Set their positions i.e(e,3) and give the gameObjects in the scene a name(ex: Position(e,3))
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
                piecePrefab.GetComponent<Piece>().PlacePieceAt(pos);
                pos.GetComponent<Position>().SpawnPiece(piecePrefab, pos.GetComponent<Position>().PositionToString());
            }
            // fill black pieces
            if (pos.GetComponent<Position>().GetNumber() == 7 || pos.GetComponent<Position>().GetNumber() == 8) {
                piecePrefab.GetComponent<Piece>().PlacePieceAt(pos);
                pos.GetComponent<Position>().SpawnPiece(piecePrefab, pos.GetComponent<Position>().PositionToString());
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

}
