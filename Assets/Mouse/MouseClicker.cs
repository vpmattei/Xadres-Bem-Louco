using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClicker : MonoBehaviour
{
    [SerializeField] private GameObject chessTable;
    private GameObject objectSelected;

    void Awake() {
        objectSelected = null;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            //Debug.Log("Pressed left click, casting ray.");
            CastRay();
        }
    }

    void CastRay() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100)) {
            Debug.DrawLine(ray.origin, hit.point);
            if (objectSelected != null) {                                         // Unselects the last position/piece selected, unless there was no position/piece selected
                chessTable.GetComponent<ChessTable>().UnselectPosition();
                chessTable.GetComponent<ChessTable>().UnselectPiece();
            }
            objectSelected = hit.collider.gameObject;
            // If object selected is a position
            if(objectSelected.CompareTag("Position")) chessTable.GetComponent<ChessTable>().SelectPosition(objectSelected);
            // If object selected is a piece
            if(objectSelected.CompareTag("Piece")) chessTable.GetComponent<ChessTable>().SelectPiece(objectSelected);
            Debug.Log(objectSelected);
            //positionSelected.GetComponent<Position>().setSelectionTo(true);
        }
        else {
            chessTable.GetComponent<ChessTable>().UnselectPosition();
            chessTable.GetComponent<ChessTable>().UnselectPiece();
        }
}
}
