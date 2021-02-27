using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System;


public class GamePhaseUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI gamePhaseText;
    [SerializeField] private TextMeshProUGUI gameStateText;

    [SerializeField] private GameObject gameCoordinator;   
    
    void Update()
    {
        gamePhaseText?.SetText(gameCoordinator.GetComponent<GameCoordinator>().GetCurrentGamePhase().ToString());
        gameStateText?.SetText(gameCoordinator.GetComponent<GameCoordinator>().GetCurrentGameState().ToString());
    }
}
