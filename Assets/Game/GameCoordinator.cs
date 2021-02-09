using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoordinator : MonoBehaviour
{

    private enum TURN 
    {
        Player1,
        Player2
    }

    private enum GamePhase
    {
        ChosePiece,
        ChoseMove,
        MakeAction
        
    }

    private GamePhase currentGamePhase;

    // Start is called before the first frame update
    void Start()
    {
        currentGamePhase = GamePhase.ChosePiece;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
