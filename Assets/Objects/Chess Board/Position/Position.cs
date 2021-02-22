using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {
    
    private char letter;
    private int letterIndex;
    private int number;
    [SerializeField] private bool isSelected;
    [SerializeField] private bool isPossibleMove;
    [SerializeField] private bool isPossibleAttackMove;
    [SerializeField] private GameObject piece = null;

    public void SetPositions(char letter, int letterIndex, int number) {
        this.letter = letter;
        this.letterIndex = letterIndex;
        this.number = number;
    }

    public void AssignPiece(GameObject piece) {
        this.piece = piece;
        piece.transform.parent = this.gameObject.transform;     // Place piece as a child of the position
        piece.GetComponent<Piece>().SetName(this.letter.ToString(), this.number);
        piece.GetComponent<Piece>().SetPositions(this.letter, this.letterIndex, this.number);
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
        this.isPossibleMove = false;
        this.isPossibleAttackMove = false;

        this.isSelected = isSelected;
        if (isSelected) this.gameObject.GetComponent<SpriteRenderer>().color =  Color.green;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = isSelected;
    }
    public void SetPossibleMoveTo(bool isPossibleMove) {
        this.isSelected = false;
        this.isPossibleAttackMove = false;

        this.isPossibleMove = isPossibleMove;
        if (isPossibleMove) this.gameObject.GetComponent<SpriteRenderer>().color =  Color.yellow;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = isPossibleMove;
    }

    public void SetPossibleAttackMoveTo(bool isPossibleAttackMove) {
        this.isSelected = false;
        this.isPossibleMove = false;

        this.isPossibleAttackMove = isPossibleAttackMove;
        if (isPossibleAttackMove) this.gameObject.GetComponent<SpriteRenderer>().color =  Color.red;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = isPossibleAttackMove;
    }

    public char GetLetter() {
        return this.letter;
    }

    public int GetNumber() {
        return this.number;
    }

    public string PositionToString() {
        return ("Position(" + this.letter + ", " + (this.number+1) + ")");
    }
}
