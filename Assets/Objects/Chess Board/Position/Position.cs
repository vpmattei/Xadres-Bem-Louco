using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {
    
    private char letter;
    private int letterIndex;
    private int number;
    [SerializeField] private bool isSelected;
    [SerializeField] private bool possibleMove;
    [SerializeField] private bool possibleAttackMove;
    [SerializeField] private bool possibleRoqueMove;
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
        this.possibleMove = false;
        this.possibleAttackMove = false;
        this.possibleRoqueMove = false;

        this.isSelected = isSelected;
        if (isSelected) this.gameObject.GetComponent<SpriteRenderer>().color =  Color.green;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = isSelected;
    }
    public void SetPossibleMoveTo(bool possibleMove) {
        this.isSelected = false;
        this.possibleAttackMove = false;
        this.possibleRoqueMove = false;

        this.possibleMove = possibleMove;
        if (possibleMove) this.gameObject.GetComponent<SpriteRenderer>().color =  Color.yellow;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = possibleMove;
    }

    public void SetPossibleAttackMoveTo(bool possibleAttackMove) {
        this.isSelected = false;
        this.possibleMove = false;
        this.possibleRoqueMove = false;

        this.possibleAttackMove = possibleAttackMove;
        if (possibleAttackMove) this.gameObject.GetComponent<SpriteRenderer>().color =  Color.red;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = possibleAttackMove;
    }

    public void SetPossibleRockMoveTo(bool possibleRoqueMove) {
        this.isSelected = false;
        this.possibleMove = false;
        this.possibleAttackMove = false;

        this.possibleRoqueMove = possibleRoqueMove;
        if (possibleRoqueMove) this.gameObject.GetComponent<SpriteRenderer>().color =  new Color(0.617f, 0.567f, 1, 1);    // Purple-ish
        this.gameObject.GetComponent<SpriteRenderer>().enabled = possibleRoqueMove;
    }

    public bool IsMovePossible() {
        return this.possibleMove;
    }

    public bool IsAttackMovePossible() {
        return this.possibleAttackMove;
    }

    public bool IsRoqueMovePossible() {
        return this.possibleRoqueMove;
    }

    public int GetLetterIndex() {
        return this.letterIndex;
    }

    public int GetNumber() {
        return this.number;
    }

    public string PositionToString() {
        return ("Position(" + this.letter + ", " + (this.number+1) + ")");
    }
}
