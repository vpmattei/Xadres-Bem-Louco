using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
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

    //public abstract GameObject PossibleMovePositions();

    public void Die() {
        this.currentPosition.GetComponent<Position>().RemovePiece();
        this.isDead = true;
        // Play dying animation
        //this.gameObject.SetActive(false);
    }
}
