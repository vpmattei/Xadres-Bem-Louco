using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoordinator : MonoBehaviour
{

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

    public Turn currentTurn;
    public GamePhase currentGamePhase;

    void Start()
    {
        currentGamePhase = GamePhase.ChosePiece;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            NextGamePhase();
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            PreviousGamePhase();
        }
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
