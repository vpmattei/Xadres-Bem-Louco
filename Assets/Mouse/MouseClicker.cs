using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseClicker : MonoBehaviour
{
    [SerializeField] private GameObject chessBoard;
    private GameObject objectSelected;

    [SerializeField] private GameObject gameCoordinator;

    private string currentTurn;

    private GameObject pieceSelected;
    private GameObject pieceAtPosition;

    void Awake() {
        objectSelected = null;
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
            if (objectSelected != null) {                                         // Unselects the last position/piece selected, unless there was no position/piece selected
                chessBoard.GetComponent<ChessBoard>().UnselectPosition();
                chessBoard.GetComponent<ChessBoard>().UnselectPiece();
            }
            objectSelected = hit.collider.gameObject;   // We either "hit" a piece or a position

            currentTurn = gameCoordinator.GetComponent<GameCoordinator>().GetCurrentTurn().ToString();  // Get the current turn

            #region Chose Piece Phase
            if(gameCoordinator.GetComponent<GameCoordinator>().GetCurrentGamePhase() == GameCoordinator.GamePhase.ChosePiece) {
                // Check if the piece is from the player of the current turn
                // If so, then we update the board reference of the piece selected
                if(objectSelected.CompareTag(currentTurn)) {
                    chessBoard.GetComponent<ChessBoard>().SelectPiece(objectSelected);
                    gameCoordinator.GetComponent<GameCoordinator>().NextGamePhase();

                    pieceSelected = objectSelected;
                }
            }
            #endregion

            #region Chose Move Phase
            if(gameCoordinator.GetComponent<GameCoordinator>().GetCurrentGamePhase() == GameCoordinator.GamePhase.ChoseMove) {
                // If object selected is a position then we update the board reference of the position selected
                if(objectSelected.CompareTag("Position")) {
                    chessBoard.GetComponent<ChessBoard>().SelectPosition(objectSelected);
                    gameCoordinator.GetComponent<GameCoordinator>().NextGamePhase();
                    
                    // Check if the position already has a piece
                    pieceAtPosition = objectSelected.GetComponent<Position>().GetPiece();
                    // Is it empty? Then just move to the position
                    if(pieceAtPosition == null) {
                        // Make piece move to the selected position
                        pieceSelected.GetComponent<Piece>().MoveToPostion(objectSelected);
                    }
                    // Is it enemy? If yes then "kill" the enemy piece and place it in its place
                    else if(!pieceAtPosition.CompareTag(currentTurn)) {
                        pieceAtPosition.GetComponent<Piece>().Die();
                        pieceSelected.GetComponent<Piece>().MoveToPostion(objectSelected);
                    }
                    // Is it friendly? Then do nothing
                }
                // If we select a piece again then we just change the piece selected
                else if(objectSelected.CompareTag(currentTurn)) {
                    chessBoard.GetComponent<ChessBoard>().SelectPiece(objectSelected);

                    pieceSelected = objectSelected;
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
}
