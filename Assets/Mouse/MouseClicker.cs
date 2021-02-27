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
        if (Input.GetMouseButtonDown(0) && gameCoordinator.GetComponent<GameCoordinator>().GetCurrentGameState().ToString() != "CheckMate") {
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

            if(currentGamePhase == GameCoordinator.GamePhase.ChosePiece.ToString()) {
            #region Chose Piece Phase
                // If the object clicked is friendly
                    // -> Select the piece
                    // -> Go to the next game phase
                if(objectClicked.CompareTag(currentTurn)) {
                    SelectPieceFromObjectClicked(objectClicked);
                    gameCoordinator.GetComponent<GameCoordinator>().NextGamePhase();
                }
            #endregion
            }

            if(currentGamePhase == GameCoordinator.GamePhase.ChoseMove.ToString()) {
            #region Chose Move Phase
                // Get the possible moves of the current selected piece
                List<GameObject> possibleMoves = chessBoard.GetComponent<ChessBoard>().GetPossibleMovesOfSelectedPiece();
                List<GameObject> possibleRoqueMoves = chessBoard.GetComponent<ChessBoard>().GetPossibleRoqueMovesOfSelectedPiece();

                // If we click on a position then we check if :
                    // Is it a possible move ?
                        // Is it empty or has an enemy piece ?
                            // -> Move to position
                        // Does it have a friendly piece ?
                            // -> Select the friendly piece instead
                if(objectClicked.CompareTag("Position")) {
                    GameObject positionClicked = objectClicked;
                    GameObject pieceAtPosition = positionClicked.GetComponent<Position>().GetPiece();

                    // We check first if it is a ROQUE MOVE
                    if (possibleRoqueMoves.Contains(positionClicked)) {
                        pieceSelected.GetComponent<King>().MakeRoqueMoveAt(positionClicked);
                        gameCoordinator.GetComponent<GameCoordinator>().NextGamePhase();
                        pieceSelected = null;
                    }
                    // If it is not a ROQUE MOVE then we check if the position clicked is on the list of possible moves
                    else if (possibleMoves.Contains(positionClicked)) {
                        // Is it empty ? Then just move to the position
                        if(pieceAtPosition == null) {
                            pieceSelected.GetComponent<Piece>().MoveToPostion(positionClicked);

                            gameCoordinator.GetComponent<GameCoordinator>().NextGamePhase();
                            pieceSelected = null;
                        }
                        // Does it have an enemy piece ? Then move to the position and kill the enemy
                        else if (pieceAtPosition != null && !pieceAtPosition.CompareTag(currentTurn)) {
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
                }
                // If we click on an enemy piece then we check if
                    // Is it a possible move ?
                        // -> Move to position
                else if(!objectClicked.CompareTag(currentTurn)) {
                    GameObject pieceClicked = objectClicked;
                    GameObject positionOfPiece = objectClicked.GetComponent<Piece>().GetCurrentPosition();

                    // We check if the enemy clicked's position is on the list of possible moves
                    if (possibleMoves.Contains(positionOfPiece)) {
                        pieceClicked.GetComponent<Piece>().Die();
                        pieceSelected.GetComponent<Piece>().MoveToPostion(positionOfPiece);

                        gameCoordinator.GetComponent<GameCoordinator>().NextGamePhase();
                        pieceSelected = null;
                    }
                }
                // If we click on a piece again then we just change the piece selected
                else if(objectClicked.CompareTag(currentTurn)) {
                    SelectPieceFromObjectClicked(objectClicked);
                }

                // If we have done our action then we go to the next game phase(switch turn)
                if (gameCoordinator.GetComponent<GameCoordinator>().GetCurrentGamePhase().ToString() == "MakeAction") {
                    gameCoordinator.GetComponent<GameCoordinator>().NextGamePhase();
                }
            #endregion
            }
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
