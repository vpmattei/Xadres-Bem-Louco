using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {
    
    [SerializeField] private char letter;
    [SerializeField] private int number;
    [SerializeField] private bool selected;
    [SerializeField] private GameObject piece = null;

    public void SetPositions(char letter, int number) {
        this.letter = letter;
        this.number = number;
    }

    public void SpawnPiece(GameObject piece, string positionName) {
        GameObject pieceSpawned = Instantiate(piece, this.gameObject.transform.position, piece.transform.rotation);
        GameObject positionToSpawn = GameObject.Find(positionName);
        pieceSpawned.transform.parent = positionToSpawn.gameObject.transform;
    }

    public void AssignPiece(GameObject piece) {
        this.piece = piece;
    }
    public void RemovePiece() {
        this.piece = null;
    }

    public bool IsEmpty() {
        return this.piece == null;
    }

    public void SetSelectionTo(bool isSelected) {
        this.selected = isSelected;
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
