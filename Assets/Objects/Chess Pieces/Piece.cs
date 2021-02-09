using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    private char letter;
    private int number;

    [SerializeField] private bool isSelected;

    [SerializeField] private List<GameObject> possibleMovePositions;
    [SerializeField] private GameObject currentPosition = null;

    private bool isDead = false;

    public void PlacePieceAt(GameObject position) {
        position.GetComponent<Position>().AssignPiece(this.gameObject); // Assigns the piece to the new position
        this.currentPosition = position;
    }
    public void MoveToPostion(GameObject position) {
        this.currentPosition.GetComponent<Position>().RemovePiece();    // Remove the piece from the current position
        position.GetComponent<Position>().AssignPiece(this.gameObject); // Assigns the piece to the new position
        this.currentPosition = position;                                // Updates current position
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
        this.isDead = true;
        // Play dying animation
        //this.gameObject.SetActive(false);
    }

    public GameObject GetCurrentPosition() {
        return this.currentPosition;
    }
}
