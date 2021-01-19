using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClicker : MonoBehaviour
{
    private GameObject positionSelected;

    void Awake() {
        positionSelected = null;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Pressed left click, casting ray.");
            CastRay();
        }
    }

    void CastRay() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100)) {
            Debug.DrawLine(ray.origin, hit.point);
            if (positionSelected != null) {                                         // Deselects the last position selected, unless there was no position selected
                positionSelected.GetComponent<Position>().setSelectionTo(false);
            }
            positionSelected = hit.collider.gameObject;
            Debug.Log("Position: " + positionSelected.GetComponent<Position>().positionToString());
            positionSelected.GetComponent<Position>().setSelectionTo(true);
        }
}
}
