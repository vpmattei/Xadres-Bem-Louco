using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {
    
    public char letter;
    public int number;
    public bool isEmpty;
    private bool selected;

    void OnMouseDown() {
        //Debug.Log("Position Clicked : \n" + positionToString());
        //this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    void Update() {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = this.selected;
    }

    public void setPositions(char letter, int number) {
        this.letter = letter;
        this.number = number;
    }

    public void setEmpty() {
        this.isEmpty = true;
    }

    public void setSelectionTo(bool isSelected) {
        this.selected = isSelected;
    }

    public string positionToString() {
        return ("Letter : " + this.letter + "\n"+
                  "Number : " + this.number + "\n"+
                  "Is Empty ? : " + this.isEmpty);
    }
}
