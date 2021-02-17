using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    [SerializeField] private GameObject gameCoordinator;

    private char letter;
    private int number;

    [SerializeField] private bool isSelected;

    [SerializeField] private List<GameObject> possibleMovePositions;
    [SerializeField] private GameObject currentPosition = null;

    void Start() {
        gameCoordinator = GameObject.Find("Game Coordinator");  // Finds the game coordinator so we can tell it to change the current phase
    }

    ///<summary>
    /// Method <c>AssignPosition</c> Assigns the current position reference for the piece and the piece reference for the position
    ///</summary>
    public void AssignPosition(GameObject position) {
        position.GetComponent<Position>().AssignPiece(this.gameObject); // Assigns the piece to the new position
        this.currentPosition = position;
    }

    ///<summary>
    /// Method <c>MoveToPosition</c> Assigns and moves the piece to a new position and goes to the next game phase
    ///</summary>
    public void MoveToPostion(GameObject position) {
        this.currentPosition.GetComponent<Position>().RemovePiece();        // Remove the piece from the current position
        AssignPosition(position);                                           // Assigns the piece to the new position
        this.gameObject.transform.position = position.transform.position;   // Changes the "physical position" of the piece to the position passed

        // Next Phase
        gameCoordinator.GetComponent<GameCoordinator>().NextGamePhase();
    }

    public void SetSelectionTo(bool isSelected) {
        this.isSelected = isSelected;
    }

    public void SetPieceName(string name) {
        this.gameObject.name = "Piece(" + name + ")";
    }

    //public abstract GameObject PossibleMovePositions();

    public void Die() {
        this.currentPosition.GetComponent<Position>().RemovePiece();
        // Play dying animation
        Destroy(this.gameObject);
    }

    public GameObject GetCurrentPosition() {
        return this.currentPosition;
    }

}
