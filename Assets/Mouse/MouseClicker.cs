using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseClicker : MonoBehaviour
{
    [SerializeField] private GameObject chessBoard;
    [SerializeField] private GameObject gameCoordinator;

    private GameObject objectClicked;
    private GameObject pieceSelected;

    private string currentTurn;
    private string currentGamePhase;

    void Awake() {
        objectClicked = null;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            //Debug.Log("Pressed left click, casting ray.");
            CastRay();
        }
    }

    void CastRay() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100)) {
            Debug.DrawLine(ray.origin, hit.point);
            if (objectClicked != null) {                                         // Unselects the last position/piece selected, unless there was no position/piece selected
                chessBoard.GetComponent<ChessBoard>().UnselectPosition();
                chessBoard.GetComponent<ChessBoard>().UnselectPiece();
            }
            objectClicked = hit.collider.gameObject;   // We either "hit" a piece or a position

            currentTurn = gameCoordinator.GetComponent<GameCoordinator>().GetCurrentTurn().ToString();  // Get the current turn
            currentGamePhase = gameCoordinator.GetComponent<GameCoordinator>().GetCurrentGamePhase().ToString();

            #region Chose Piece Phase
            if(currentGamePhase == GameCoordinator.GamePhase.ChosePiece.ToString()) {
                SelectPieceFromObjectClicked(objectClicked);

                if(objectClicked.CompareTag(currentTurn)) {  // Is the object clicked friendly? Then go to the next game phase
                    gameCoordinator.GetComponent<GameCoordinator>().NextGamePhase();
                }
            }
            #endregion

            #region Chose Move Phase
            if(currentGamePhase == GameCoordinator.GamePhase.ChoseMove.ToString()) {
                // If we click on a position then we check if
                    // Is it empty?
                    // Does it have an enemy piece?
                    // Does it have a friendly piece?
                if(objectClicked.CompareTag("Position")) {
                    GameObject positionClicked = objectClicked;
                    GameObject pieceAtPosition = positionClicked.GetComponent<Position>().GetPiece();

                    // Is it empty? Then just move to the position
                    if(pieceAtPosition == null) {
                        pieceSelected.GetComponent<Piece>().MoveToPostion(positionClicked);

                        gameCoordinator.GetComponent<GameCoordinator>().NextGamePhase();
                        pieceSelected = null;
                    }
                    // Is it enemy? If yes then "kill" the enemy piece and place it in its place
                    else if(!pieceAtPosition.CompareTag(currentTurn)) {
                        pieceAtPosition.GetComponent<Piece>().Die();
                        pieceSelected.GetComponent<Piece>().MoveToPostion(positionClicked);

                        gameCoordinator.GetComponent<GameCoordinator>().NextGamePhase();
                        pieceSelected = null;
                    }
                    // Is it friendly? Then we select the piece
                    else if(pieceAtPosition.CompareTag(currentTurn)) {
                        SelectPieceFromObjectClicked(objectClicked);
                    }
                }
                // If we click on an enemy piece then we kill that piece and move the piece selected to its position
                else if(!objectClicked.CompareTag(currentTurn)) {
                    GameObject pieceClicked = objectClicked;
                    GameObject positionOfPiece = objectClicked.GetComponent<Piece>().GetCurrentPosition();

                    pieceClicked.GetComponent<Piece>().Die();
                    pieceSelected.GetComponent<Piece>().MoveToPostion(positionOfPiece);

                    gameCoordinator.GetComponent<GameCoordinator>().NextGamePhase();
                    pieceSelected = null;
                }
                // If we click on a piece again then we just change the piece selected
                else if(objectClicked.CompareTag(currentTurn)) {
                    SelectPieceFromObjectClicked(objectClicked);
                }

            }
            #endregion
        }
        else {  // If we click on a empty space (not the board)
            // Unselects every selection
            chessBoard.GetComponent<ChessBoard>().UnselectPosition();
            chessBoard.GetComponent<ChessBoard>().UnselectPiece();

            // Reset Game Phase
            gameCoordinator.GetComponent<GameCoordinator>().ResetGamePhase();
        }
    }

    private void SelectPieceFromObjectClicked(GameObject objectClicked) {
        if (objectClicked.CompareTag("Position")) {     // Is the object clicked a position?
            GameObject positionClicked = objectClicked;
            GameObject pieceAtPosition = positionClicked.GetComponent<Position>().GetPiece();
            
            if (pieceAtPosition != null && pieceAtPosition.CompareTag(currentTurn)) {  // Is the pieceAtPosition not null and friendly piece?
                chessBoard.GetComponent<ChessBoard>().SelectPiece(pieceAtPosition);
                pieceSelected = chessBoard.GetComponent<ChessBoard>().GetPieceSelected();
            }
        }
        else if (objectClicked.CompareTag(currentTurn)) {   // Is the object clicked a friendly piece?
            chessBoard.GetComponent<ChessBoard>().SelectPiece(objectClicked);
            pieceSelected = chessBoard.GetComponent<ChessBoard>().GetPieceSelected();
        }
    }
}
