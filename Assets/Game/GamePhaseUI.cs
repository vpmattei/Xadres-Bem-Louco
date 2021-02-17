using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System;


public class GamePhaseUI : MonoBehaviour
{

    private TextMeshProUGUI gamePhaseText;

    [SerializeField] private GameObject gameCoordinator;   
    // Start is called before the first frame update
    void Start()
    {
        gamePhaseText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        gamePhaseText.SetText(gameCoordinator.GetComponent<GameCoordinator>().GetCurrentGamePhase().ToString());
    }
}
