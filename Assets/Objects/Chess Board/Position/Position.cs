using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {
    
    private char letter;
    private int number;
    [SerializeField] private bool isSelected;
    [SerializeField] private GameObject piece = null;

    public void SetPositions(char letter, int number) {
        this.letter = letter;
        this.number = number;
    }

    public void AssignPiece(GameObject piece) {
        this.piece = piece;
        piece.transform.parent = this.gameObject.transform;     // Place piece as a child of the position
        piece.GetComponent<Piece>().SetPieceName(this.letter + ", " + this.number);
    }
    
    public void RemovePiece() {
        this.piece = null;
    }

    public GameObject GetPiece() {
        return this.piece;
    }

    public bool IsEmpty() {
        return this.piece == null;
    }

    public void SetSelectionTo(bool isSelected) {
        this.isSelected = isSelected;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = isSelected;
    }

    public char GetLetter() {
        return this.letter;
    }

    public int GetNumber() {
        return this.number;
    }

    public string PositionToString() {
        return ("Position(" + this.letter + ", " + this.number + ")");
    }
}
