using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoordinator : MonoBehaviour
{
    [SerializeField] private GameObject chessBoard;
    public enum GameState {
        NotCheck,
        Check,
        CheckMate
    }

    public enum Turn 
    {
        Player1,
        Player2
    }

    public enum GamePhase
    {
        ChosePiece,
        ChoseMove,
        MakeAction,
        SwitchTurn
        
    }

    public GameState currentGameState;
    public Turn currentTurn;
    public GamePhase currentGamePhase;

    void Start() {
        currentGamePhase = GamePhase.ChosePiece;
        currentGameState = GameState.NotCheck;
        chessBoard = GameObject.Find("Chess Board");
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            NextGamePhase();
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            PreviousGamePhase();
        }
    }

    public GameState GetCurrentGameState() {
        return this.currentGameState;
    }

    public Turn GetCurrentTurn() {
        return currentTurn;
    }

    public GamePhase GetCurrentGamePhase() {
        return currentGamePhase;
    }

    private void ChangeTurn() {
        if(currentTurn == Turn.Player1) currentTurn = Turn.Player2;
        else currentTurn = Turn.Player1;
        currentGamePhase = GamePhase.ChosePiece;
        chessBoard.GetComponent<ChessBoard>().UpdatePossibleMovesOfPieces();
        if (chessBoard.GetComponent<ChessBoard>().IsGameInCheck()) {
            currentGameState = GameState.Check;
            chessBoard.GetComponent<ChessBoard>().LimitPlayerMovesWhenChecked();
            if (chessBoard.GetComponent<ChessBoard>().isGameInCheckMate()) {
                currentGameState = GameState.CheckMate;
            }
        }
        else {
            currentGameState = GameState.NotCheck;
        }
        //chessBoard.GetComponent<ChessBoard>().DestroyPiecesFromGraveyard();
    }

    public void NextGamePhase() {
        currentGamePhase += 1;
        if(currentGamePhase == GamePhase.SwitchTurn) {
            ChangeTurn();
        }
    }

    public void PreviousGamePhase() {
        currentGamePhase -= 1;
    }

    public void ResetGamePhase() {
        currentGamePhase = GamePhase.ChosePiece;
    }

}
