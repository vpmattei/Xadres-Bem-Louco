using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessTable : MonoBehaviour {

    private static readonly char[] LETTERS = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'};
    private static readonly int[] NUMBERS = {1, 2, 3, 4, 5, 6, 7, 8};

    [SerializeField] private GameObject positionPrefab;
    public List<GameObject> positions;

    void Awake() {
        // Preencher todas as posicoes com as letras e numeros
        for(int l = 0; l < LETTERS.Length; l++) {
            for(int n = 0; n < NUMBERS.Length; n++) {
                GameObject position = Instantiate(positionPrefab, this.gameObject.transform.position + new Vector3(l + 0.5f, 0, n + 0.5f), positionPrefab.transform.rotation) as GameObject;
                position.transform.parent = gameObject.transform;
                position.GetComponent<Position>().setPositions(LETTERS[l],NUMBERS[n]);
                position.GetComponent<Position>().setEmpty();
                positions.Add(position);
            }
        }
    }

    void Start() {
        
    }

}
